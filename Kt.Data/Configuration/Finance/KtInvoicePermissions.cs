 namespace Kt.Data.Configuration.Finance;

/// <summary>
/// Represents the permissions for an invoice.
/// </summary>
/// <param name="ApplicationEntry">The application entry permission.</param>
/// <param name="Read">The read permission.</param>
/// <param name="CreateDraft">The create draft permission.</param>
/// <param name="EditDraft">The edit draft permission.</param>
public sealed record KtInvoicePermissions(
    string ApplicationEntry,
    string Read,
    string CreateDraft,
    string EditDraft);