# Kt.Data implementation summary

This summary documents the implementation present in `Kt.Data` and directly related contracts/call sites. It does not describe proposed code as if it already exists. No connection-string values or credentials are included.

## 1. Project and dependencies

[`Kt.Data/Kt.Data.csproj`](Kt.Data/Kt.Data.csproj) targets **`net10.0`**, enables implicit usings and nullable reference types, and sets C# language version 12. Direct dependencies:

- NuGet: `MySqlConnector` **2.6.2**.
- Project: [`Kt.Models/Kt.Data.Models.csproj`](Kt.Models/Kt.Data.Models.csproj), referenced as `Kt.Data.Models.csproj`.
- No ORM package is referenced; repository implementations use `MySqlConnector` commands and explicit row mapping.

## 2. Connection, sessions, and dependency injection

### Implemented connection/session setup

[`Kt.Data/KtDb.cs`](Kt.Data/KtDb.cs), namespace `Kt.Data`:

```csharp
public sealed class KtDb : IAsyncDisposable
public KtDb(string connectionString)
public Task<KtDbSession> OpenAsync(ulong principalId, CancellationToken cancellationToken = default)
public Task<T> InTransactionAsync<T>(ulong principalId, Func<KtDbSession, CancellationToken, Task<T>> work, CancellationToken cancellationToken = default)
public ValueTask DisposeAsync()
```

`KtDb` builds and owns a `MySqlDataSource` for its lifetime. It enables pooling, connection reset, and MySQL user variables. `OpenAsync` rejects principal ID 0, opens a pooled connection, sets `@plo_user_id` and the session time zone to `+00:00`, then calls `GetUserId()` and rejects a mismatch with `UnauthorizedAccessException`. If initialization fails, it disposes the connection and rethrows. The principal must be a trusted, validated authenticated-user ID; it is not a request-body value.

[`Kt.Data/KtDbSession.cs`](Kt.Data/KtDbSession.cs), namespace `Kt.Data`:

```csharp
public sealed class KtDbSession : IAsyncDisposable
public ulong PrincipalId { get; }
public bool HasTransaction { get; }
public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
public Task CommitAsync(CancellationToken cancellationToken = default)
public Task RollbackAsync(CancellationToken cancellationToken = default)
public ValueTask DisposeAsync()
```

The session constructor and `CreateCommand(string sql)` are internal. Commands created internally are bound to the session’s connection and current transaction. A session represents one actor/connection, is explicitly documented as not thread-safe, and must not execute concurrent commands.

### DI status

`Kt.Data` does **not** define a service-collection registration extension, nor does it register `KtDb` or repositories. The working integration-check setup in [`Kt.Data.IntegrationChecks/Program.cs`](Kt.Data.IntegrationChecks/Program.cs) manually creates `KtDb`, `KtIdentityRepository`, and `KtWorkspaceRepository`; it passes the same `KtDb` to repositories that require it.

The API’s distinct registration method, [`Kt.API/Authentication/KtAuthenticationApi.cs`](Kt.API/Authentication/KtAuthenticationApi.cs), is:

```csharp
public static IServiceCollection AddKtAuthentication<TBackend>(this IServiceCollection services)
	where TBackend : class, IKtIdentityBackend
```

It registers the authentication backend and API session/authentication services. It does **not** configure `KtDb` or make a database-backed `IKtIdentityBackend`. [`Kt.API/Program.cs`](Kt.API/Program.cs) calls `AddKtAuthentication<MyIdentityBackend>()`; the implementation is still a placeholder (see §7). A host integrating Kt.Data must supply its own `KtDb` lifetime/registration and repository registrations, if it wants DI construction.

## 3. Contexts, repositories, and query signatures

Signatures below are public declarations in namespace `Kt.Data.Repositories` unless noted. Optional values are the implementation defaults.

### User profile

[`Kt.Data/Repositories/KtIdentityRepository.cs`](Kt.Data/Repositories/KtIdentityRepository.cs):

```csharp
public sealed class KtIdentityRepository
public Task<KtUserProfile?> GetCurrentAsync(KtDbSession session, CancellationToken cancellationToken = default)
```

This returns a profile only when both `platform_user` and `security_principal` are ACTIVE and the principal type is USER; otherwise it returns `null`. “Current” means the actor already bound to the session (`session.PrincipalId`), not an arbitrary supplied user ID.

Result type, namespace `Kt.Data.Queries.Identity`, [`Kt.Data/Queries/Identity/KtUserProfile.cs`](Kt.Data/Queries/Identity/KtUserProfile.cs):

```csharp
public sealed record KtUserProfile(ulong Id, string PrincipalCode, string DisplayName,
	string? PreferredLocale, string? TimeZone)
```

### Workspaces and explicit permissions

[`Kt.Data/Repositories/KtWorkspaceRepository.cs`](Kt.Data/Repositories/KtWorkspaceRepository.cs):

```csharp
public sealed class KtWorkspaceRepository
public Task<IReadOnlyList<KtWorkspaceSummary>> ListCurrentAsync(KtDbSession session,
	ulong afterId = 0, int limit = 100, CancellationToken cancellationToken = default)
public Task<bool> HasPermissionAsync(KtDbSession session, ulong workspaceId, string permissionCode,
	CancellationToken cancellationToken = default)
```

`ListCurrentAsync` returns active memberships in active workspaces for the session actor, ordered/paged by workspace ID (maximum limit 200). `HasPermissionAsync` checks one exact permission grant in one workspace. It requires active membership, workspace, user and USER principal, active role and permission, and a role assignment effective at the database UTC time. It returns false when no explicit valid grant exists; there is no administrator bypass. It does not return a list of grants or apply application-specific filtering.

Result type, namespace `Kt.Data.Queries.Workspaces`, [`Kt.Data/Queries/Workspaces/KtWorkspaceSummary.cs`](Kt.Data/Queries/Workspaces/KtWorkspaceSummary.cs):

```csharp
public sealed record KtWorkspaceSummary(ulong Id, string Name, string WorkspaceType)
```

### Objects

[`Kt.Data/Repositories/KtObjectRepository.cs`](Kt.Data/Repositories/KtObjectRepository.cs):

```csharp
public sealed class KtObjectRepository
public KtObjectRepository(KtDb db, IKtObjectAccessPolicy access, IEnumerable<KtObjectTable> tables)
public Task<KtObjectRecord?> GetAsync(KtDbSession session, ulong globalId,
	CancellationToken cancellationToken = default)
public Task<KtObjectRecord?> GetStaticAsync(KtDbSession session, string tableName, ulong id,
	CancellationToken cancellationToken = default)
public Task<KtObjectPage> ListDynamicAsync(KtDbSession session, ulong masterId,
	ulong afterId = 0, int limit = 100, CancellationToken cancellationToken = default)
public Task<KtObjectPage> ListReferencesAsync(KtDbSession session, ulong masterId,
	ulong afterId = 0, int limit = 100, CancellationToken cancellationToken = default)
public Task<ulong> CreateValueAsync(ulong authenticatedPrincipalId, ulong masterId, ulong roleId,
	KtObjectDynamicValue value, CancellationToken cancellationToken = default)
```

The read methods authorize through `IKtObjectAccessPolicy`; list methods page by scanned ID and authorize children individually. `CreateValueAsync` owns a transaction and only creates a new scalar property. The interface is namespace `Kt.Data.Authorization.Objects`:

```csharp
public interface IKtObjectAccessPolicy
Task<bool> CanReadAsync(KtDbSession session, string tableName, ulong id, CancellationToken cancellationToken)
Task<bool> CanCreatePropertyAsync(KtDbSession session, ulong masterId, ulong roleId,
	string valueTable, CancellationToken cancellationToken)
```

The corresponding records are in `Kt.Data.Queries.Objects`: `KtObjectRecord(string TableName, ulong Id, KtObjectIdentity? Identity, IReadOnlyDictionary<string, object?> Fields)` and `KtObjectPage(IReadOnlyList<KtObjectRecord> Items, ulong LastScannedId)`.

### Invoices

[`Kt.Data/Repositories/KtInvoiceRepository.cs`](Kt.Data/Repositories/KtInvoiceRepository.cs):

```csharp
public sealed class KtInvoiceRepository
public KtInvoiceRepository(KtDb db, KtWorkspaceRepository workspaces, KtInvoicePermissions permissions)
public Task<KtInvoiceHeader?> GetAsync(KtDbSession session, KtInvoiceScope scope, ulong invoiceId,
	CancellationToken cancellationToken = default)
public Task<IReadOnlyList<KtInvoiceHeader>> ListAsync(KtDbSession session, KtInvoiceScope scope,
	ulong afterId = 0, int limit = 100, CancellationToken cancellationToken = default)
public Task<IReadOnlyList<KtInvoiceLine>> GetLinesAsync(KtDbSession session, KtInvoiceScope scope,
	ulong invoiceId, uint afterLineNumber = 0, int limit = 100,
	CancellationToken cancellationToken = default)
public Task<KtInvoiceCreated> CreatePurchaseDraftAsync(ulong authenticatedPrincipalId,
	KtInvoiceScope scope, KtPurchaseDraft draft, Guid correlationId,
	CancellationToken cancellationToken = default)
public Task<ulong> UpdateDraftNotesAsync(ulong authenticatedPrincipalId, KtInvoiceScope scope,
	ulong invoiceId, ulong expectedRowVersion, string? notes, Guid correlationId,
	CancellationToken cancellationToken = default)
```

`KtInvoiceScope` is `Kt.Data.Context.Finance.KtInvoiceScope(ulong WorkspaceId, ulong LegalEntityId)`. `KtInvoicePermissions` is `Kt.Data.Configuration.Finance.KtInvoicePermissions(string ApplicationEntry, string Read, string CreateDraft, string EditDraft)`. The repository checks application-entry plus operation permissions against the **workspace ID**, then verifies the active company-workspace’s primary legal entity. Draft creation writes the invoice, lines, initial status event, and audit event atomically. Notes updates use row-version and draft/unposted conditions; conflicts throw `Kt.Data.Exceptions.KtInvoiceConflictException`. It does not implement posting or payment workflows.

Invoice result records are in `Kt.Data.Queries.Finance`:

```csharp
public sealed record KtInvoiceCreated(ulong Id, string PublicId, ulong RowVersion)
public sealed record KtInvoiceHeader(ulong Id, string PublicId, ulong LegalEntityId,
	string InvoiceType, string DocumentNumber, DateOnly IssueDate, DateOnly? DueDate,
	ulong CurrencyId, KtPartySnapshot Issuer, KtPartySnapshot Recipient, KtPartySnapshot InvoiceRecipient,
	decimal SubtotalAmount, decimal DiscountAmount, decimal ChargeAmount, decimal TaxAmount,
	decimal RoundingAmount, decimal TotalAmount, decimal PrepaidAmount, decimal PayableAmount,
	string WorkflowStatus, string PostingStatus, string SettlementStatus, string MatchingStatus,
	string? Notes, ulong RowVersion)
public sealed record KtInvoiceLine(ulong Id, uint LineNumber, string LineType, string Description,
	decimal Quantity, string? UnitOfMeasureCode, decimal UnitPrice, decimal DiscountAmount,
	decimal ChargeAmount, decimal NetAmount, decimal TaxAmount, decimal GrossAmount, ulong RowVersion)
public sealed record KtPartySnapshot(ulong PartyId, string Name, string? TaxId = null,
	string? AddressJson = null)
```

Draft input records are in `Kt.Data.Commands.Finance`: `KtPurchaseDraft(string DocumentNumber, string DocumentNumberNormalized, DateOnly IssueDate, DateOnly? DueDate, ulong CurrencyId, KtPartySnapshot Issuer, KtPartySnapshot Recipient, KtPartySnapshot InvoiceRecipient, IReadOnlyList<KtDraftLine> Lines, string? Notes = null)` and `KtDraftLine(string Description, decimal Quantity, decimal UnitPrice, decimal DiscountAmount = 0, decimal ChargeAmount = 0, decimal TaxAmount = 0, string LineType = "ITEM", string? UnitOfMeasureCode = null)`.

### Other catalog loading

[`Kt.Data/Caching/KtDbPreload.cs`](Kt.Data/Caching/KtDbPreload.cs), namespace `Kt.Data.Caching`, is a separate metadata/reference-data snapshot loader, not an actor session/repository. Its public surface includes `KtDbPreload(string connectionString)`, `bool IsLoaded`, `KtDbSnapshot Current`, `Task LoadAsync(CancellationToken cancellationToken = default)`, and `Task ReloadAsync(CancellationToken cancellationToken = default)`. It directly opens a MySQL connection and reads catalogues into immutable frozen dictionaries; it is not registered by Kt.Data DI.

## 4. Relevant models and namespaces

The model project uses namespace `Kt.Data.Models.*`. These classes are database-row snapshots with explicit mappings, not an ORM context; annotations do not execute queries. The repository maps its own profile/workspace result records rather than loading these models automatically.

- **Users and principals** — `Kt.Data.Models.Identity.PlatformUser` (`platform_user`) and `SecurityPrincipal` (`security_principal`). The platform user ID is also the principal ID. `PlatformUser` includes status and profile fields, plus email/phone/MFA requirement timestamps.
- **External identities** — `Kt.Data.Models.Identity.PlatformUserIdentity` (`platform_user_identity`); fields identify provider, issuer, and stable subject and include link/disable/last-use times.
- **Email/phone contacts** — `PlatformUserEmail`, `PlatformUserPhone`, and `PlatformPhoneNumber`, all in `Kt.Data.Models.Identity`. Contact records contain opaque Identity Service references; `PlatformUserEmail.WorkspaceId` is optional scope, not a company-table foreign key.
- **MFA** — `Kt.Data.Models.Identity.PlatformUserMfaMethod`; stores method type/status and an opaque `IdentityMethodReference`, not the method secret. The listed types include TOTP, SMS, recovery codes, and passkey.
- **Recovery/onboarding** — `PlatformUserAccountRecoveryCase`, `PlatformUserAccountRecoveryEvent`, onboarding, guardian, and age-profile records are also in `Kt.Data.Models.Identity`; they are row models, not credential-verification behavior.
- **Company/workspace boundary** — `Kt.Data.Models.Companies.CompanyWorkspace` (`company_workspace`) represents the operating boundary using `WorkspaceId`, organization `CorePartyId`, and optional `PrimaryLegalEntityId`. `Kt.Data.Models.Workspaces.Workspace` has `WorkspaceType` values including COMPANY and an optional `LegacyCompanyId`. A compatibility `Kt.Data.Models.Legacy.LegacyCompany` also exists.
- **Memberships and access grants** — `Kt.Data.Models.Workspaces.WorkspaceMembership`, `WorkspaceMembershipRole`, `WorkspaceRole`, `WorkspacePermission`, `WorkspaceRolePermission`, plus `WorkspaceAdministrator` and `WorkspaceInvitation`.

No `Kt.Data.Models.Identity` row model in this project represents a stored password hash/credential, and no Kt.Data repository authenticates credentials. The MFA/contact identifiers deliberately point to an external Identity Service.

## 5. Cancellation, transactions, and database errors

- Database async operations accept/pass `CancellationToken`; readers and commands pass it to MySqlConnector APIs. `KtDbSession` transaction begin/commit/rollback also accept cancellation.
- `KtDb.InTransactionAsync<T>(...)` opens the actor session, begins a transaction, awaits the delegate, and commits only after success. If work throws or is canceled, it does not commit; `await using` disposal disposes the uncommitted transaction and then returns the connection to the pool. The implementation documents no automatic retries.
- Sessions are single-threaded/non-concurrent. Explicit rollback is available with `RollbackAsync`; the session must have an active transaction for commit/rollback, and cannot begin a nested transaction.
- Queries use parameterized command values. The session’s SQL command factory is internal, so it is not a public caller-supplied-SQL API.
- There is no general database-exception translation layer or retry policy in the repositories: underlying MySqlConnector/database exceptions propagate. Application-level checks throw `ArgumentException`/`ArgumentOutOfRangeException`, `InvalidOperationException`, or `UnauthorizedAccessException` as applicable. Invoice optimistic-update conflicts have the dedicated `KtInvoiceConflictException`.

## 6. Working example: read the current user and check permissions by workspace

This uses existing methods only. Supply `principalId` from validated authentication and `permissionCodes` from application configuration/database values; do not treat these inputs as client-trusted authorization claims.

```csharp
using Kt.Data;
using Kt.Data.Repositories;

await using var db = new KtDb(connectionString);
var identities = new KtIdentityRepository();
var workspaces = new KtWorkspaceRepository();

await using var session = await db.OpenAsync(principalId, cancellationToken);
var user = await identities.GetCurrentAsync(session, cancellationToken);
if (user is null)
	return;

var workspacesForUser = await workspaces.ListCurrentAsync(session, cancellationToken: cancellationToken);
foreach (var workspace in workspacesForUser)
{
	var granted = new List<string>();
	foreach (var permissionCode in permissionCodes)
	{
		if (await workspaces.HasPermissionAsync(
				session, workspace.Id, permissionCode, cancellationToken))
			granted.Add(permissionCode);
	}

	// `user` and `workspace.Id` are the identity/scope for these explicit checks.
	// `granted` contains only the permission codes checked above for this workspace.
}
```

This deliberately performs one explicit permission check per code; KtWorkspaceRepository has no method that returns all scoped permissions.

## 7. Missing functionality and placeholder behavior

The requested operation names are part of the API authentication contract in [`Kt.API/Authentication/IKtIdentityBackend.cs`](Kt.API/Authentication/IKtIdentityBackend.cs), **not** Kt.Data. Their current public signatures are:

```csharp
Task<KtVerifiedIdentity?> VerifyCredentialsAsync(string login, string password, string? otp,
	string? remoteIp, CancellationToken cancellationToken)
Task<KtAccess?> GetCurrentAccessAsync(string userId, string application, string? companyId,
	CancellationToken cancellationToken)
```

`KtVerifiedIdentity` contains `(string UserId, string SecurityVersion)`; `KtAccess` contains `(string UserId, string DisplayName, string? Email, string SecurityVersion, string[] Permissions)`. These are API types in `Kt.Api.Authentication`, not data-layer types.

**Implemented in Kt.Data:** current authenticated-user profile retrieval; current workspaces; one workspace permission check; company-workspace/primary-legal-entity scope validation inside invoice operations; external-identity and MFA row models. These pieces do not together provide either requested auth operation.

**Not implemented in Kt.Data; work still needed for `VerifyCredentialsAsync`:** login normalization/lookup and mapping to `PlatformUser.Id`; secure verification via the actual external/local identity mechanism (no password hash model or verifier is present); active/verified/locked account rules; required MFA challenge verification and replay prevention; persistent throttling/lockout; generic failure behavior/dummy hash for unknown logins; audit outcomes; and generation/lookup of an authoritative `SecurityVersion`. The API interface documents these as requirements. The current `KtIdentityRepository.GetCurrentAsync` is not a credential check.

**Not implemented in Kt.Data; work still needed for `GetCurrentAccessAsync`:** parse/map the API’s string user identifier to the Kt.Data `ulong` user/principal ID; validate the application together with the requested scope; establish exact company/workspace membership and application access; retrieve *all* current permissions for that application/scope (the current repository checks one permission code at a time and does not filter by application); produce `KtAccess` with current profile, permissions, and security version; and reject deleted/disabled/locked users, revoked access, and invalid membership. The API contract requires this method to run on login and every authenticated request.

[`Kt.API/Authentication/AuthBackend.cs`](Kt.API/Authentication/AuthBackend.cs) is an explicit placeholder, not a database implementation. `VerifyCredentialsAsync` returns `null`. `GetCurrentAccessAsync` currently returns fabricated `Read`, `Write`, and `Delete` permissions and assigns input values to placeholder profile fields. Do not use it as real authorization behavior.

### Meaning of `CompanyId`

The API contract/request/session names the value `CompanyId` and types it as `string?`, but Kt.Data does not define a mapping from that value to a company ID or workspace ID. The Kt.Data permission API explicitly takes `ulong workspaceId`; `company_workspace` is keyed by `workspace_id`, while `Workspace` separately has an optional legacy company ID. Thus **the code does not establish that API `CompanyId` means either identifier**. A compatible backend must define and validate that mapping (and should not pass the string directly as if it were a workspace ID).