namespace Kt.Data.Context.Finance;

/// It identifies the workspace and legal entity for an invoice operation.

public sealed record KtInvoiceScope(
    ulong WorkspaceId,
    ulong LegalEntityId);