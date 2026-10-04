
using Kt.Data.Queries.Finance;

namespace Kt.Data.Commands.Finance;


/// <summary>
/// Represents a draft purchase document with its associated details, including document number, issue date, due date, currency, issuer, recipient, invoice recipient, line items, and optional notes.
/// </summary>
/// <param name="DocumentNumber">The document number of the draft purchase.</param>
/// <param name="DocumentNumberNormalized">The normalized document number of the draft purchase.</param>
/// <param name="IssueDate">The issue date of the draft purchase.</param>
/// <param name="DueDate">The due date of the draft purchase.</param>
/// <param name="CurrencyId">The currency ID of the draft purchase.</param>
/// <param name="Issuer">The issuer of the draft purchase.</param>
/// <param name="Recipient">The recipient of the draft purchase.</param>
/// <param name="InvoiceRecipient">The invoice recipient of the draft purchase.</param>
/// <param name="Lines">The line items of the draft purchase.</param>
/// <param name="Notes">Optional notes for the draft purchase.</param>
public sealed record KtPurchaseDraft(string DocumentNumber, string DocumentNumberNormalized,
    DateOnly IssueDate, DateOnly? DueDate, ulong CurrencyId,
    KtPartySnapshot Issuer, KtPartySnapshot Recipient, KtPartySnapshot InvoiceRecipient,
    IReadOnlyList<KtDraftLine> Lines, string? Notes = null);


