using System.Globalization;
using System.Text;
using System.Text.Json;
using Kt.Data.Queries.Finance;
using Kt.Data.Configuration.Finance;
using Kt.Data.Context.Finance;
using Kt.Data.Commands.Finance;
using Kt.Data.Exceptions;
using MySqlConnector;

namespace Kt.Data.Repositories;

/// <summary>Scoped invoice reads and purchase-draft writes. No posting, payment or arbitrary status changes.</summary>
public sealed class KtInvoiceRepository {
    private readonly KtDb _db;
    private readonly KtWorkspaceRepository _workspaces;
    private readonly KtInvoicePermissions _permissions;
    public KtInvoiceRepository(KtDb db, KtWorkspaceRepository workspaces, KtInvoicePermissions permissions) {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _workspaces = workspaces ?? throw new ArgumentNullException(nameof(workspaces));
        _permissions = permissions ?? throw new ArgumentNullException(nameof(permissions));
        foreach (var code in new[] { permissions.ApplicationEntry, permissions.Read, permissions.CreateDraft, permissions.EditDraft })
            if (string.IsNullOrWhiteSpace(code) || code.Length > 160 || code.Any(c => c > 127))
                throw new ArgumentException("Configure actual ASCII permission codes from the database.");
    }

    // Scope uses the verified primary-legal-entity relationship only, not a guessed company ID join.
    private async Task<ulong> RequireScopeAsync(KtDbSession session, KtInvoiceScope scope, string permission,
        CancellationToken ct) {
        if (scope.WorkspaceId == 0 || scope.LegalEntityId == 0) throw new ArgumentOutOfRangeException(nameof(scope));
        if (!await _workspaces.HasPermissionAsync(session, scope.WorkspaceId, _permissions.ApplicationEntry, ct) ||
            !await _workspaces.HasPermissionAsync(session, scope.WorkspaceId, permission, ct))
            throw new UnauthorizedAccessException("Invoice permission denied.");
        using var command = session.CreateCommand("""
            SELECT le.party_id FROM company_workspace cw
            JOIN core_legal_entity le ON le.id = cw.primary_legal_entity_id AND le.party_id = cw.core_party_id
            WHERE cw.workspace_id = @workspace AND cw.status = 'ACTIVE' AND le.id = @entity;
            """);
        command.Parameters.Add("@workspace", MySqlDbType.UInt64).Value = scope.WorkspaceId;
        command.Parameters.Add("@entity", MySqlDbType.UInt64).Value = scope.LegalEntityId;
        var party = await command.ExecuteScalarAsync(ct);
        if (party is null) throw new UnauthorizedAccessException("Legal entity is not the active workspace's primary entity.");
        return Convert.ToUInt64(party, CultureInfo.InvariantCulture);
    }

    private const string HeaderColumns = """
        i.id, HEX(i.public_id), i.accounting_legal_entity_id, i.invoice_type, i.document_number,
        i.issue_date, i.due_date, i.currency_id,
        i.issuer_party_id, i.issuer_name_snapshot, i.issuer_tax_id_snapshot, i.issuer_address_snapshot_json,
        i.recipient_party_id, i.recipient_name_snapshot, i.recipient_tax_id_snapshot, i.recipient_address_snapshot_json,
        i.invoice_recipient_party_id, i.invoice_recipient_name_snapshot, i.invoice_recipient_tax_id_snapshot, i.invoice_recipient_address_snapshot_json,
        i.subtotal_amount, i.discount_amount, i.charge_amount, i.tax_amount, i.rounding_amount,
        i.total_amount, i.prepaid_amount, i.payable_amount,
        i.workflow_status, i.posting_status, i.settlement_status, i.matching_status, i.notes, i.row_version
        """;

    public async Task<KtInvoiceHeader?> GetAsync(KtDbSession session, KtInvoiceScope scope, ulong invoiceId,
        CancellationToken cancellationToken = default) {
        await RequireScopeAsync(session, scope, _permissions.Read, cancellationToken);
        using var command = session.CreateCommand($"SELECT {HeaderColumns} FROM finance_invoice i WHERE i.id = @id AND i.accounting_legal_entity_id = @entity;");
        command.Parameters.Add("@id", MySqlDbType.UInt64).Value = invoiceId;
        command.Parameters.Add("@entity", MySqlDbType.UInt64).Value = scope.LegalEntityId;
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? ReadHeader(reader) : null;
    }

    public async Task<IReadOnlyList<KtInvoiceHeader>> ListAsync(KtDbSession session, KtInvoiceScope scope,
        ulong afterId = 0, int limit = 100, CancellationToken cancellationToken = default) {
        PageLimit(limit);
        await RequireScopeAsync(session, scope, _permissions.Read, cancellationToken);
        using var command = session.CreateCommand($"SELECT {HeaderColumns} FROM finance_invoice i WHERE i.accounting_legal_entity_id = @entity AND i.id > @after ORDER BY i.id LIMIT @limit;");
        command.Parameters.Add("@entity", MySqlDbType.UInt64).Value = scope.LegalEntityId;
        command.Parameters.Add("@after", MySqlDbType.UInt64).Value = afterId;
        command.Parameters.Add("@limit", MySqlDbType.Int32).Value = limit;
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var result = new List<KtInvoiceHeader>();
        while (await reader.ReadAsync(cancellationToken)) result.Add(ReadHeader(reader));
        return result.AsReadOnly();
    }

    public async Task<IReadOnlyList<KtInvoiceLine>> GetLinesAsync(KtDbSession session, KtInvoiceScope scope,
        ulong invoiceId, uint afterLineNumber = 0, int limit = 100, CancellationToken cancellationToken = default) {
        PageLimit(limit);
        await RequireScopeAsync(session, scope, _permissions.Read, cancellationToken);
        using var command = session.CreateCommand("""
            SELECT l.id, l.line_number, l.line_type, l.description, l.quantity, l.unit_of_measure_code,
                   l.unit_price, l.discount_amount, l.charge_amount, l.net_amount, l.tax_amount, l.gross_amount, l.row_version
            FROM finance_invoice_line l JOIN finance_invoice i ON i.id = l.invoice_id
            WHERE i.id = @id AND i.accounting_legal_entity_id = @entity AND l.line_number > @after
            ORDER BY l.line_number LIMIT @limit;
            """);
        command.Parameters.Add("@id", MySqlDbType.UInt64).Value = invoiceId;
        command.Parameters.Add("@entity", MySqlDbType.UInt64).Value = scope.LegalEntityId;
        command.Parameters.Add("@after", MySqlDbType.UInt32).Value = afterLineNumber;
        command.Parameters.Add("@limit", MySqlDbType.Int32).Value = limit;
        await using var r = await command.ExecuteReaderAsync(cancellationToken);
        var result = new List<KtInvoiceLine>();
        while (await r.ReadAsync(cancellationToken))
            result.Add(new(r.GetUInt64(0), r.GetUInt32(1), r.GetString(2), r.GetString(3), r.GetDecimal(4),
                NullableText(r, 5), r.GetDecimal(6), r.GetDecimal(7), r.GetDecimal(8), r.GetDecimal(9),
                r.GetDecimal(10), r.GetDecimal(11), r.GetUInt64(12)));
        return result.AsReadOnly();
    }

    /// <summary>Creates a PURCHASE_INVOICE draft, its lines, initial status event and audit event atomically.</summary>
    public Task<KtInvoiceCreated> CreatePurchaseDraftAsync(ulong authenticatedPrincipalId, KtInvoiceScope scope,
        KtPurchaseDraft draft, Guid correlationId, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(draft);
        // Freeze the mutable list before validation or any await.
        draft = draft with { Lines = draft.Lines?.ToArray() ?? throw new ArgumentException("Lines are required.") };
        var totals = ValidateDraft(draft);
        var frozen = draft;
        return _db.InTransactionAsync(authenticatedPrincipalId, async (session, ct) => {
            var legalPartyId = await RequireScopeAsync(session, scope, _permissions.CreateDraft, ct);
            if (frozen.Recipient.PartyId != legalPartyId || frozen.InvoiceRecipient.PartyId != legalPartyId)
                throw new ArgumentException("This initial purchase workflow requires recipient and bill-to to be the accounting party.");
            using (var validate = session.CreateCommand("""
                SELECT EXISTS (
                    SELECT 1 FROM core_party issuer
                    JOIN core_business_partner bp ON bp.party_id = issuer.id AND bp.is_supplier = 1 AND bp.is_active = 1
                    JOIN core_party recipient ON recipient.id = @recipient AND recipient.is_active = 1
                    JOIN sys_currency currency ON currency.id = @currency AND currency.is_active = 1
                    WHERE issuer.id = @issuer AND issuer.is_active = 1
                );
                """)) {
                validate.Parameters.Add("@recipient", MySqlDbType.UInt64).Value = legalPartyId;
                validate.Parameters.Add("@currency", MySqlDbType.UInt64).Value = frozen.CurrencyId;
                validate.Parameters.Add("@issuer", MySqlDbType.UInt64).Value = frozen.Issuer.PartyId;
                if (Convert.ToInt32(await validate.ExecuteScalarAsync(ct)) != 1)
                    throw new ArgumentException("Active supplier, recipient and currency are required.");
            }
            // The DDL declares local AUTO_INCREMENT IDs; refuse if deployment metadata says otherwise.
            using (var strategy = session.CreateCommand("""
                SELECT COUNT(*) FROM obj_table
                WHERE table_name IN ('finance_invoice', 'finance_invoice_line') AND uses_global_object_id = 1;
                """)) {
                if (Convert.ToInt32(await strategy.ExecuteScalarAsync(ct)) != 0)
                    throw new InvalidOperationException("Invoice tables are configured for global IDs; review allocation before writes.");
            }
            var publicId = Guid.NewGuid().ToString("N");
            using var command = session.CreateCommand("""
                INSERT INTO finance_invoice (
                    public_id, accounting_legal_entity_id, issuer_party_id, recipient_party_id, invoice_recipient_party_id,
                    invoice_type, document_number, document_number_normalized, issue_date, due_date, currency_id,
                    issuer_name_snapshot, issuer_tax_id_snapshot, issuer_address_snapshot_json,
                    recipient_name_snapshot, recipient_tax_id_snapshot, recipient_address_snapshot_json,
                    invoice_recipient_name_snapshot, invoice_recipient_tax_id_snapshot, invoice_recipient_address_snapshot_json,
                    subtotal_amount, tax_amount, total_amount, payable_amount, notes,
                    workflow_status, posting_status, matching_status, created_by_user_id, updated_by_user_id)
                VALUES (UNHEX(@public), @entity, @issuer, @recipient, @billto, 'PURCHASE_INVOICE',
                    @number, @normalized, @issue, @due, @currency,
                    @issuerName, @issuerTax, @issuerAddress, @recipientName, @recipientTax, @recipientAddress,
                    @billtoName, @billtoTax, @billtoAddress, @net, @tax, @gross, @gross, @notes,
                    'DRAFT', 'NOT_POSTED', 'UNMATCHED', @actor, @actor);
                """);
            command.Parameters.Add("@public", MySqlDbType.VarChar).Value = publicId;
            command.Parameters.Add("@entity", MySqlDbType.UInt64).Value = scope.LegalEntityId;
            AddParty(command, "issuer", frozen.Issuer);
            AddParty(command, "recipient", frozen.Recipient);
            AddParty(command, "billto", frozen.InvoiceRecipient);
            command.Parameters.Add("@number", MySqlDbType.VarChar).Value = frozen.DocumentNumber;
            command.Parameters.Add("@normalized", MySqlDbType.VarChar).Value = frozen.DocumentNumberNormalized;
            command.Parameters.Add("@issue", MySqlDbType.Date).Value = frozen.IssueDate;
            command.Parameters.Add("@due", MySqlDbType.Date).Value = (object?)frozen.DueDate ?? DBNull.Value;
            command.Parameters.Add("@currency", MySqlDbType.UInt64).Value = frozen.CurrencyId;
            AddDecimal(command, "@net", totals.Net);
            AddDecimal(command, "@tax", totals.Tax);
            AddDecimal(command, "@gross", totals.Gross);
            command.Parameters.Add("@notes", MySqlDbType.Text).Value = (object?)frozen.Notes ?? DBNull.Value;
            command.Parameters.Add("@actor", MySqlDbType.UInt64).Value = session.PrincipalId;
            await command.ExecuteNonQueryAsync(ct);
            using var last = session.CreateCommand("SELECT LAST_INSERT_ID();");
            var id = Convert.ToUInt64(await last.ExecuteScalarAsync(ct), CultureInfo.InvariantCulture);
            uint lineNumber = 0;
            foreach (var line in frozen.Lines)
                await InsertLineAsync(session, id, ++lineNumber, line, ct);
            using var status = session.CreateCommand("""
                INSERT INTO finance_invoice_status_event
                    (invoice_id, status_dimension, from_status_code, to_status_code, actor_user_id, correlation_id)
                VALUES (@id, 'WORKFLOW', NULL, 'DRAFT', @actor, UNHEX(@correlation));
                """);
            status.Parameters.Add("@id", MySqlDbType.UInt64).Value = id;
            status.Parameters.Add("@actor", MySqlDbType.UInt64).Value = session.PrincipalId;
            status.Parameters.Add("@correlation", MySqlDbType.VarChar).Value = correlationId.ToString("N");
            await status.ExecuteNonQueryAsync(ct);
            await AuditAsync(session, scope, id, "finance.invoice.draft_created", correlationId, ct);
            return new KtInvoiceCreated(id, publicId, 1);
        }, cancellationToken);
    }

    /// <summary>Optimistic update restricted to unposted DRAFT rows. Returns the incremented version.</summary>
    public Task<ulong> UpdateDraftNotesAsync(ulong authenticatedPrincipalId, KtInvoiceScope scope, ulong invoiceId,
        ulong expectedRowVersion, string? notes, Guid correlationId, CancellationToken cancellationToken = default) {
        ValidateNotes(notes);
        if (expectedRowVersion is 0 or ulong.MaxValue) throw new ArgumentOutOfRangeException(nameof(expectedRowVersion));
        return _db.InTransactionAsync(authenticatedPrincipalId, async (session, ct) => {
            await RequireScopeAsync(session, scope, _permissions.EditDraft, ct);
            using var command = session.CreateCommand("""
                UPDATE finance_invoice SET notes = @notes, updated_by_user_id = @actor,
                    updated_at_utc = UTC_TIMESTAMP(6), row_version = row_version + 1
                WHERE id = @id AND accounting_legal_entity_id = @entity AND row_version = @version
                  AND workflow_status = 'DRAFT' AND posting_status = 'NOT_POSTED'
                  AND posted_at_utc IS NULL AND posting_journal_entry_id IS NULL AND voided_at_utc IS NULL;
                """);
            command.Parameters.Add("@notes", MySqlDbType.Text).Value = (object?)notes ?? DBNull.Value;
            command.Parameters.Add("@actor", MySqlDbType.UInt64).Value = session.PrincipalId;
            command.Parameters.Add("@id", MySqlDbType.UInt64).Value = invoiceId;
            command.Parameters.Add("@entity", MySqlDbType.UInt64).Value = scope.LegalEntityId;
            command.Parameters.Add("@version", MySqlDbType.UInt64).Value = expectedRowVersion;
            if (await command.ExecuteNonQueryAsync(ct) != 1) throw new KtInvoiceConflictException();
            await AuditAsync(session, scope, invoiceId, "finance.invoice.draft_notes_updated", correlationId, ct);
            return expectedRowVersion + 1;
        }, cancellationToken);
    }

    private static async Task InsertLineAsync(KtDbSession session, ulong invoiceId, uint number, KtDraftLine line, CancellationToken ct) {
        var net = checked(line.Quantity * line.UnitPrice - line.DiscountAmount + line.ChargeAmount);
        using var command = session.CreateCommand("""
            INSERT INTO finance_invoice_line (invoice_id, line_number, line_type, description, quantity,
                unit_of_measure_code, unit_price, discount_amount, charge_amount, net_amount, tax_amount, gross_amount)
            VALUES (@invoice, @number, @type, @description, @quantity, @unit, @price, @discount, @charge, @net, @tax, @gross);
            """);
        command.Parameters.Add("@invoice", MySqlDbType.UInt64).Value = invoiceId;
        command.Parameters.Add("@number", MySqlDbType.UInt32).Value = number;
        command.Parameters.Add("@type", MySqlDbType.VarChar).Value = line.LineType;
        command.Parameters.Add("@description", MySqlDbType.VarChar).Value = line.Description;
        command.Parameters.Add("@unit", MySqlDbType.VarChar).Value = (object?)line.UnitOfMeasureCode ?? DBNull.Value;
        AddDecimal(command, "@quantity", line.Quantity);
        AddDecimal(command, "@price", line.UnitPrice);
        AddDecimal(command, "@discount", line.DiscountAmount);
        AddDecimal(command, "@charge", line.ChargeAmount);
        AddDecimal(command, "@net", net);
        AddDecimal(command, "@tax", line.TaxAmount);
        AddDecimal(command, "@gross", checked(net + line.TaxAmount));
        await command.ExecuteNonQueryAsync(ct);
    }

    private static async Task AuditAsync(KtDbSession session, KtInvoiceScope scope, ulong id, string eventCode,
        Guid correlation, CancellationToken ct) {
        using var command = session.CreateCommand("""
            INSERT INTO audit_event (workspace_id, actor_principal_id, event_type, entity_type, entity_id, correlation_id, outcome)
            VALUES (@workspace, @actor, @event, 'finance_invoice', @id, @correlation, 'SUCCESS');
            """);
        command.Parameters.Add("@workspace", MySqlDbType.UInt64).Value = scope.WorkspaceId;
        command.Parameters.Add("@actor", MySqlDbType.UInt64).Value = session.PrincipalId;
        command.Parameters.Add("@event", MySqlDbType.VarChar).Value = eventCode;
        command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id.ToString(CultureInfo.InvariantCulture);
        command.Parameters.Add("@correlation", MySqlDbType.VarChar).Value = correlation.ToString("D");
        await command.ExecuteNonQueryAsync(ct);
    }

    private static void AddParty(MySqlCommand command, string prefix, KtPartySnapshot party) {
        command.Parameters.Add("@" + prefix, MySqlDbType.UInt64).Value = party.PartyId;
        command.Parameters.Add("@" + prefix + "Name", MySqlDbType.VarChar).Value = party.Name;
        command.Parameters.Add("@" + prefix + "Tax", MySqlDbType.VarChar).Value = (object?)party.TaxId ?? DBNull.Value;
        command.Parameters.Add("@" + prefix + "Address", MySqlDbType.JSON).Value = (object?)party.AddressJson ?? DBNull.Value;
    }
    private static void AddDecimal(MySqlCommand command, string name, decimal value) {
        var parameter = command.Parameters.Add(name, MySqlDbType.Decimal);
        parameter.Precision = 20;
        parameter.Scale = 6;
        parameter.Value = value;
    }
    private static string? NullableText(MySqlDataReader r, int index) => r.IsDBNull(index) ? null : r.GetString(index);
    private static KtPartySnapshot ReadParty(MySqlDataReader r, int i) => new(r.GetUInt64(i), r.GetString(i + 1), NullableText(r, i + 2), NullableText(r, i + 3));
    private static KtInvoiceHeader ReadHeader(MySqlDataReader r) => new(r.GetUInt64(0), r.GetString(1).ToLowerInvariant(),
        r.GetUInt64(2), r.GetString(3), r.GetString(4), r.GetDateOnly(5), r.IsDBNull(6) ? null : r.GetDateOnly(6),
        r.GetUInt64(7), ReadParty(r, 8), ReadParty(r, 12), ReadParty(r, 16),
        r.GetDecimal(20), r.GetDecimal(21), r.GetDecimal(22), r.GetDecimal(23), r.GetDecimal(24),
        r.GetDecimal(25), r.GetDecimal(26), r.GetDecimal(27), r.GetString(28), r.GetString(29),
        r.GetString(30), r.GetString(31), NullableText(r, 32), r.GetUInt64(33));
    private static void PageLimit(int limit) {
        if (limit is < 1 or > 200) throw new ArgumentOutOfRangeException(nameof(limit));
    }
    private static void Text(string value, int max) {
        if (string.IsNullOrWhiteSpace(value) || value.Length > max) throw new ArgumentException("Missing or oversized text.");
    }
    private static void ValidateNotes(string? notes) {
        if (notes is not null && Encoding.UTF8.GetByteCount(notes) > 65535) throw new ArgumentException("Notes exceed TEXT capacity.");
    }
    private static void Amount(decimal value) {
        if (value < 0 || value > 99999999999999.999999m || decimal.Round(value, 6) != value)
            throw new ArgumentException("Amount must be nonnegative and fit DECIMAL(20,6) without rounding.");
    }
    private static (decimal Net, decimal Tax, decimal Gross) ValidateDraft(KtPurchaseDraft draft) {
        Text(draft.DocumentNumber, 128);
        Text(draft.DocumentNumberNormalized, 128);
        ValidateNotes(draft.Notes);
        if (draft.IssueDate.Year < 1000 || draft.DueDate < draft.IssueDate || draft.CurrencyId == 0)
            throw new ArgumentException("Invalid invoice date, due date or currency.");
        foreach (var party in new[] { draft.Issuer, draft.Recipient, draft.InvoiceRecipient }) {
            ArgumentNullException.ThrowIfNull(party);
            if (party.PartyId == 0) throw new ArgumentException("Party ID required.");
            Text(party.Name, 256);
            if (party.TaxId?.Length > 64) throw new ArgumentException("Tax identifier too long.");
            if (party.AddressJson is not null) {
                if (Encoding.UTF8.GetByteCount(party.AddressJson) > 65535) throw new ArgumentException("Address JSON too large.");
                using var json = JsonDocument.Parse(party.AddressJson);
                if (json.RootElement.ValueKind != JsonValueKind.Object) throw new ArgumentException("Address must be a JSON object.");
            }
        }
        if (draft.Lines.Count > 500) throw new ArgumentException("At most 500 lines per draft creation.");
        decimal net = 0, tax = 0;
        foreach (var line in draft.Lines) {
            ArgumentNullException.ThrowIfNull(line);
            Text(line.Description, 1024);
            if (line.LineType is not ("ITEM" or "SERVICE" or "SHIPPING" or "CHARGE"))
                throw new ArgumentException("This draft workflow supports ITEM, SERVICE, SHIPPING and CHARGE lines.");
            if (line.UnitOfMeasureCode?.Length > 32 || line.Quantity <= 0) throw new ArgumentException("Invalid unit or quantity.");
            Amount(line.Quantity); Amount(line.UnitPrice); Amount(line.DiscountAmount); Amount(line.ChargeAmount); Amount(line.TaxAmount);
            var lineNet = checked(line.Quantity * line.UnitPrice - line.DiscountAmount + line.ChargeAmount);
            Amount(lineNet); Amount(checked(lineNet + line.TaxAmount));
            net = checked(net + lineNet); tax = checked(tax + line.TaxAmount);
        }
        Amount(net); Amount(tax); Amount(checked(net + tax));
        return (net, tax, net + tax);
    }
}
