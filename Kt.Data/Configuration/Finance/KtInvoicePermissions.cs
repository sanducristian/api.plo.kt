 namespace Kt.Data.Configuration.Finance;

public sealed record KtInvoicePermissions(
    string ApplicationEntry,
    string Read,
    string CreateDraft,
    string EditDraft);