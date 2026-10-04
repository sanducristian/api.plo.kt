using System.Security.Cryptography;
using System.Text;
using Kt.Api.Authentication;


namespace Kt.Api.Session;



/// <summary>
/// Provides extension methods for creating and managing sessions in an <see cref="IKtSessionStore"/>.
/// </summary>
public static class KtSessionStoreExtensions {
    /// <summary>
    /// Creates a new session for the specified user and stores it in the session store.
    /// </summary>
    /// <param name="sessions">The session store to add the new session to.</param>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="securityVersion">The security version of the session.</param>
    /// <param name="application">The application associated with the session.</param>
    /// <param name="companyId">The optional company identifier associated with the session.</param>
    /// <param name="client">The client associated with the session.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="KtCreatedSession"/> representing the newly created session.</returns>
    public static async Task<KtCreatedSession> CreateSessionAsync(
        this IKtSessionStore sessions,
        string userId,
        string securityVersion,
        string application,
        string? companyId,
        string client,
        CancellationToken cancellationToken) {

        // Default session lifetime: 8 hours.
        var expiresAt = DateTimeOffset.UtcNow.AddHours(8);

        // Return the secret token to the client; store only its hash.
        var token = Convert.ToHexString(
            RandomNumberGenerator.GetBytes(32)
        );

        var tokenHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(token))
        );

        var storedSession = new KtSession(
            UserId: userId,
            SecurityVersion: securityVersion,
            Application: application,
            CompanyId: companyId,
            Client: client,
            ExpiresAt: expiresAt
        );

        await sessions.PutAsync(
            tokenHash,
            storedSession,
            cancellationToken
        );

        return new KtCreatedSession(
            SessionId: token,
            ExpiresUtc: expiresAt
        );
    }
}

