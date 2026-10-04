namespace Kt.Api.Session;

public sealed record KtCreatedSession(
    string SessionId,
    DateTimeOffset ExpiresUtc
);
