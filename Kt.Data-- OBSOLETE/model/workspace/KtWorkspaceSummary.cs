namespace Kt.Data;

// Persistence results; map IDs to strings in browser-facing API contracts.
public sealed record KtWorkspaceSummary(ulong Id, string Name, string WorkspaceType);
