namespace Kt.Data.Invoices;

public sealed record KtInvoicePermissions(string ApplicationEntry, string Read, string CreateDraft, string EditDraft);
public sealed record KtInvoiceScope(ulong WorkspaceId, ulong LegalEntityId);
public sealed record KtPartySnapshot(ulong PartyId, string Name, string? TaxId = null, string? AddressJson = null);
public sealed record KtDraftLine(string Description, decimal Quantity, decimal UnitPrice,
    decimal DiscountAmount = 0, decimal ChargeAmount = 0, decimal TaxAmount = 0,
    string LineType = "ITEM", string? UnitOfMeasureCode = null);
public sealed record KtPurchaseDraft(string DocumentNumber, string DocumentNumberNormalized,
    DateOnly IssueDate, DateOnly? DueDate, ulong CurrencyId,
    KtPartySnapshot Issuer, KtPartySnapshot Recipient, KtPartySnapshot InvoiceRecipient,
    IReadOnlyList<KtDraftLine> Lines, string? Notes = null);


public sealed record KtInvoiceCreated(ulong Id, string PublicId, ulong RowVersion);


public sealed record KtInvoiceHeader(ulong Id, string PublicId, ulong LegalEntityId,
    string InvoiceType, string DocumentNumber, DateOnly IssueDate, DateOnly? DueDate,
    ulong CurrencyId, KtPartySnapshot Issuer, KtPartySnapshot Recipient, KtPartySnapshot InvoiceRecipient,
    decimal SubtotalAmount, decimal DiscountAmount, decimal ChargeAmount, decimal TaxAmount,
    decimal RoundingAmount, decimal TotalAmount, decimal PrepaidAmount, decimal PayableAmount,
    string WorkflowStatus, string PostingStatus, string SettlementStatus, string MatchingStatus,
    string? Notes, ulong RowVersion);


public sealed record KtInvoiceLine(ulong Id, uint LineNumber, string LineType, string Description,
    decimal Quantity, string? UnitOfMeasureCode, decimal UnitPrice, decimal DiscountAmount,
    decimal ChargeAmount, decimal NetAmount, decimal TaxAmount, decimal GrossAmount, ulong RowVersion);


public sealed class KtInvoiceConflictException : Exception {
    public KtInvoiceConflictException() : base("Invoice changed, is not an editable draft, or is unavailable in this scope.") { }
}
