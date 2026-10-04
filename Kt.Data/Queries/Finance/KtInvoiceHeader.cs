// Move the corresponding previous declaration to this namespace; do not leave duplicate old types.
#nullable enable
using System;
using System.Collections.Generic;

namespace Kt.Data.Queries.Finance;

/// <summary>Selected or combined repository result; not a complete table mapping or public API contract.</summary>
/// <remarks>Existing positional constructor retained for repository compatibility. Map public API identifiers deliberately.</remarks>
public sealed record KtInvoiceHeader(ulong Id, string PublicId, ulong LegalEntityId,
    string InvoiceType, string DocumentNumber, DateOnly IssueDate, DateOnly? DueDate,
    ulong CurrencyId, KtPartySnapshot Issuer, KtPartySnapshot Recipient, KtPartySnapshot InvoiceRecipient,
    decimal SubtotalAmount, decimal DiscountAmount, decimal ChargeAmount, decimal TaxAmount,
    decimal RoundingAmount, decimal TotalAmount, decimal PrepaidAmount, decimal PayableAmount,
    string WorkflowStatus, string PostingStatus, string SettlementStatus, string MatchingStatus,
    string? Notes, ulong RowVersion);
