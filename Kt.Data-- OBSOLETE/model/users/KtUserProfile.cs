namespace Kt.Data;

// Persistence results; map IDs to strings in browser-facing API contracts.
public sealed record KtUserProfile(ulong Id, string PrincipalCode, string DisplayName, string? PreferredLocale, string? TimeZone);