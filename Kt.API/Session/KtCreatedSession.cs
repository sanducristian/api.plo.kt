namespace Kt.Api.Session;


/// <summary>
/// Represents a created session with its unique identifier and expiration time in UTC.
/// </summary>
/// <param name="SessionId">The unique identifier of the session.</param>
/// <param name="ExpiresUtc">The expiration time of the session in UTC.</param>
public sealed record KtCreatedSession(
    string SessionId,
    DateTimeOffset ExpiresUtc
);
