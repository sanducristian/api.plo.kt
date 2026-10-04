using System.Security.Cryptography;
using System.Text;
using Kt.Api.Authentication;


namespace Kt.Api.Session;

public static class KtSessionStoreExtensions {
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

