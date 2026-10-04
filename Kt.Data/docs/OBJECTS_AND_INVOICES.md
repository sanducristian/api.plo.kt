# Objects and invoices — implementation guide

Version 0.2, 1 October 2026. Based on the supplied September 26 database reference. These are repository implementations, not deployed API endpoints. No migrations are applied.

## Capability matrix

| Area | Implemented |
| --- | --- |
| Global objects | Resolve active obj_id through obj_table; return registry metadata and the selected concrete projection |
| Static records | Read registered static rows by table and ID, respecting uses_global_object_id |
| Dynamic properties | Paginate active direct children, independently authorise each child |
| Typed references | Paginate outgoing obj_dyn.masterId relations separately from metadata ownership |
| Scalar writes | Atomically create integer, double, generic/localised text, attribute, blob, date, datetime and time properties |
| Rich dynamic records | Read files, geographic pose, locations, prices, quantities and numbering metadata |
| Invoices | Get header, paginate headers, paginate lines within a verified legal-entity scope |
| Purchase drafts | Create header and up to 500 lines, status event and audit event in one transaction |
| Draft updates | Update notes using expected row_version and unposted DRAFT guards, with an audit event |

Static generic writes, rich dynamic writes, property replacement/invalidation/version lineage, reference creation, invoice line editing, posting, tax determination, settlements and payments are intentionally not exposed. Their domain invariants must be implemented in dedicated operations; an unrestricted table writer would bypass them.

## Register the repositories

Keep the KtDb and workspace registrations in README.md. Add:

```csharp
using Kt.Data.Objects;
using Kt.Data.Invoices;
using Kt.Data.Repositories;

// Your implementation must check authoritative ownership and permissions.
// Implement IKtObjectAccessPolicy in the API's authorisation layer.
builder.Services.AddScoped<IKtObjectAccessPolicy, YourObjectAccessPolicy>();
builder.Services.AddScoped(sp => new KtObjectRepository(
    sp.GetRequiredService<KtDb>(),
    sp.GetRequiredService<IKtObjectAccessPolicy>(),
    KtObjectTable.KnownTables));

// Read these four values from your trusted configuration. They must be real DB permission codes.
builder.Services.AddSingleton(new KtInvoicePermissions(
    configuredEntryPermission,
    configuredReadPermission,
    configuredCreateDraftPermission,
    configuredEditDraftPermission));
builder.Services.AddScoped<KtInvoiceRepository>();
```

`YourObjectAccessPolicy` and the configured code variables are integration placeholders, not supplied classes. There is deliberately no allow-all default policy. Identity validation is not object authorisation.

### Object policy contract

`CanReadAsync(session, tableName, id, cancellationToken)` must resolve the actual scope of that table/record and check the actor. A plain static ID is table-local unless its registry declares global identity. Generic metadata has no universal workspace column. Follow verified owner relationships; do not blindly treat an obj_id master or semantic role as an access grant. Reject unresolved ownership. Shared reference catalogues can have a distinct, explicit read policy.

`CanCreatePropertyAsync(session, masterId, roleId, valueTable, cancellationToken)` must check permission to modify that owner's metadata and ensure this semantic role may hold that value-table type. Being allowed to read the owner is insufficient.

The policy executes inside the session passed to it. If it needs internal SQL access, place the persistence helper within Kt.Data; do not weaken the interface to always return true. Return false for unknown ownership or roles. It must not commit, change actor context or execute concurrently on the same connection.

The supplied schema alone does not resolve all ownership paths. An API-specific policy is required before exposing these object methods to clients. Dynamic links expose IDs, but never automatically authorise reading their target payloads.

### Table projections

KnownTables contains all 17 obj_dyn tables and reviewed static projections for units, currency, language, core locations, device units and legacy components. Restrict the collection to tables the API needs. Add another static table with an explicit `KtObjectTable` projection after checking its current DDL. Table/column names are server configuration, never HTTP input. The DB registry alone cannot add a queryable table.

Object fields are database-level values in a read-only dictionary. This is intentional for heterogeneous records. Map them into public DTOs before HTTP serialization: preserve bigint precision, choose date semantics, filter internal storage locators/configuration and register required JSON types in a Native AOT host. Do not serialize the entire dictionary as a public API contract by default.

```csharp
await using var session = await db.OpenAsync(authenticatedPrincipalId, ct);
var currency = await objects.GetStaticAsync(session, "sys_currency", currencyId, ct);
var item = await objects.GetAsync(session, globalObjectId, ct);
var properties = await objects.ListDynamicAsync(session, globalObjectId, limit: 100,
    cancellationToken: ct);
var links = await objects.ListReferencesAsync(session, globalObjectId,
    cancellationToken: ct);
```

Use LastScannedId for the next object page, even when Items is empty: inaccessible children are filtered, so an empty page is not necessarily the end. Advance until LastScannedId stops changing. The root needs a registered projection and a positive read decision.

### Dynamic creation and allocation

```csharp
ulong propertyId = await objects.CreateValueAsync(
    authenticatedPrincipalId, ownerGlobalId, existingSemanticRoleId,
    new KtLocalizedTextValue(existingLanguageId, "Display name"), ct);
```

The method owns its transaction. It locks the active master, validates the policy and semantic role, checks singleton occupancy, resolves an exact registered table with uses_global_object_id=1, inserts the obj_id and concrete value, then commits. It does not call the broken AllocateIdMaster routine or create registry entries/role names on demand.

Direct obj_id insertion uses the same registry allocation mechanism as AllocateId and explicitly supplies actor attribution; the existing insert trigger also resolves the actor. This is a new application insert path requiring an integration check before production. The SQL session's LAST_INSERT_ID is read as unsigned. A failure rolls back registry and payload together.

Singleton enforcement is deliberately conservative: one active child per master/role, including localised strings. No language exception is invented. All writers must lock the master with this protocol; legacy writers can otherwise violate the invariant. Existing invalid/version records are not automatically overwritten or revived.

Scalar validation respects current physical limits: utf8mb3 rejects supplementary characters, short strings are bounded, BLOB is limited to 65,535 bytes, doubles must be finite, dates start at year 1000. Legacy DATETIME has unspecified timezone and whole seconds; convert intentionally before constructing KtDateTimeValue. TIME writes are restricted to whole-second time of day, despite MySQL also supporting durations. Rich metadata writes require their own rules and are not silently coerced into scalar tables.

## Invoice scope and permissions

Each operation takes both WorkspaceId and LegalEntityId. The repository checks application-entry plus action permission and verifies:

- Active membership, user/principal and workspace through KtWorkspaceRepository.
- Active company_workspace.
- company_workspace.primary_legal_entity_id equals the requested core_legal_entity.
- The composite legal-entity/party relationship matches core_party_id.

Every invoice read/update additionally constrains accounting_legal_entity_id. Line queries join their parent invoice. Additional legal entities per workspace are not inferred from party IDs: add a verified mapping before extending scope.

Read permission is not sufficient for writes. Configure real existing permission codes; this package does not insert permissions or assume example codes exist. The API remains responsible for valid sessions/MFA, application enablement and any finer resource restrictions or revocation serialization requirements.

## Creating a supplier invoice draft

```csharp
var scope = new KtInvoiceScope(workspaceId, accountingLegalEntityId);
var draft = new KtPurchaseDraft(
    DocumentNumber: "SUP-2026-001",
    DocumentNumberNormalized: normalizedByYourNumberPolicy,
    IssueDate: new DateOnly(2026, 10, 1),
    DueDate: new DateOnly(2026, 10, 31),
    CurrencyId: currencyId,
    Issuer: supplierSnapshot,
    Recipient: legalEntitySnapshot,
    InvoiceRecipient: legalEntitySnapshot,
    Lines: new[] { new KtDraftLine("Engineering services", 2m, 100m, TaxAmount: 40m) });

var created = await invoices.CreatePurchaseDraftAsync(
    authenticatedPrincipalId, scope, draft, correlationId, ct);
```

This initial write operation creates PURCHASE_INVOICE only. Issuer must be an active party with an active supplier record; currency must be active. Recipient and invoice recipient must both equal the accounting legal entity's party. Different delivery/bill-to arrangements and credit/sales documents remain readable but require separate write workflows.

Pass snapshots from the actual invoice: names/tax identifiers/address JSON are preserved, not silently replaced with today's master data. Normalization must be performed by a single trusted API policy, not independently chosen by each client; the repository preserves document_number and validates the supplied normalized string's size. The database unique key rejects duplicate occurrence 1; the repository never overrides duplicates.

Quantities and amounts must fit DECIMAL(20,6) exactly. There is no implicit rounding. Net per line is quantity × unit price − line discount + line charge. Header subtotal is the sum of line net amounts; header-level discount/charge/rounding/prepayment remain zero. Tax is supplied per line, totals are recomputed, and payable equals total. This is draft arithmetic, not country tax validation. Tax breakdown tables are not populated and the draft must undergo the full tax/matching/posting workflow before posting. A zero-line draft is allowed.

Default creation states are DRAFT / NOT_POSTED / OPEN / UNMATCHED. Public IDs use UUID hexadecimal bytes (UNHEX of the N format, without UUID_TO_BIN byte swapping). Reads return the stored bytes as lower-case hex; confirm existing writers use a compatible convention before treating every historical binary ID as an RFC UUID.

Invoice header and line IDs use their own AUTO_INCREMENT paths. Creation refuses if registry metadata marks either table as requiring global IDs. No unrelated obj_id row is created. Audit/status event creation is in the same transaction. New audit codes are `finance.invoice.draft_created` and `finance.invoice.draft_notes_updated`; these are application conventions introduced here, not pre-existing registry claims.

## Reading and updating

```csharp
await using var session = await db.OpenAsync(authenticatedPrincipalId, ct);
var header = await invoices.GetAsync(session, scope, invoiceId, ct);
var lines = await invoices.GetLinesAsync(session, scope, invoiceId,
    afterLineNumber: 0, limit: 100, cancellationToken: ct);
```

Use an explicit transaction for a consistent header/lines snapshot. List methods use ID/line-number keyset pagination and a maximum page size of 200. Header/line DTOs are selected views, not a full export of every invoice column or related tax/matching/document table.

```csharp
var nextVersion = await invoices.UpdateDraftNotesAsync(
    authenticatedPrincipalId, scope, invoiceId, expectedRowVersion,
    "Supplier clarification received", correlationId, ct);
```

Concurrent edits, wrong entity, missing records and non-editable states raise KtInvoiceConflictException. Map that to an appropriate API conflict response without leaking another entity's data. Other MySQL errors, including duplicate numbers, are intentionally propagated for controlled translation by the API; never return raw database diagnostics to clients.

Methods that create values or invoices and update notes own their transaction; do not nest them inside another InTransactionAsync callback. They have no automatic retries. Duplicate detection is not general request idempotency: after a connection failure during commit, reconcile the outcome before retrying.

## Validation status

No .NET SDK or MySQL server connection is available in the delivery environment. Compilation and live integration are unverified. The Python source-schema checks validate known projections and insert column names; they do not replace compilation or execute SQL.

Run from the directory containing both projects:

```sh
dotnet build Kt.Data/Kt.Data.csproj
dotnet build Kt.Data.IntegrationChecks/Kt.Data.IntegrationChecks.csproj
python Kt.Data/tools/check_schema_mappings.py
```

For the read-only integration runner, set KT_TEST_CONNECTION, KT_TEST_ACTOR_A, KT_TEST_ACTOR_B, KT_TEST_WORKSPACE, KT_TEST_DENIED_WORKSPACE and KT_TEST_PERMISSION. Use two distinct active human principals. Actor A needs the selected permission in the allowed workspace and must lack it in the denied workspace. Then run:

```sh
dotnet run --project Kt.Data.IntegrationChecks
```

Before enabling writes, test on a disposable database:

1. Denied object policy creates neither registry nor payload.
2. Force a concrete-value insert failure and confirm allocation rolls back.
3. Concurrent singleton inserts produce only one committed child when all writers use this repository.
4. Invalid language, unknown role, invalid master and non-global table registration reject creation.
5. Static local IDs do not resolve unrelated obj_id rows with the same number.
6. Purchase draft totals, party snapshots, binary UUID, line rows, status and audit events match expected values.
7. Duplicate invoice numbers and a forced line/audit failure roll back the entire operation.
8. Wrong workspace/legal entity cannot read or update an invoice or its lines.
9. Two notes updates with the same version permit one update; POSTED, VOID and non-DRAFT rows remain unchanged.
10. Use a fractional quantity × price requiring over six decimals and verify it is rejected rather than rounded.

Verify target schema/routines, current writer conventions, permission codes, ownership policy and invoice-number normalization before production deployment.
