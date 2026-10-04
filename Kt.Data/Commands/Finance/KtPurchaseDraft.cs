
using Kt.Data.Queries.Finance;

namespace Kt.Data.Commands.Finance;


public sealed record KtPurchaseDraft(string DocumentNumber, string DocumentNumberNormalized,
    DateOnly IssueDate, DateOnly? DueDate, ulong CurrencyId,
    KtPartySnapshot Issuer, KtPartySnapshot Recipient, KtPartySnapshot InvoiceRecipient,
    IReadOnlyList<KtDraftLine> Lines, string? Notes = null);


