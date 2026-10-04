namespace Kt.Authentication;


/// <summary>Identity only. Does not grant application or company permissions.</summary>
/// <param name="DisplayName">The display name of the user, if any.</param>
/// <param name="UserId">The unique identifier of the user.</param>
public sealed record KtAuthenticatedUser(string UserId, string? DisplayName);



/// <summary>Server-validated session metadata; never contains the bearer secret.</summary>
/// <param name="ExpiresAt">The UTC time at which the session expires.</param>
/// <param name="User">The authenticated user associated with the session.</param>
public sealed record KtValidatedSession(KtAuthenticatedUser User,DateTimeOffset ExpiresAt);
