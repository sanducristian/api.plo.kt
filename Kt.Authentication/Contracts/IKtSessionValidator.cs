namespace Kt.Authentication;

/// <summary>Implemented by the host's identity/session adapter.</summary>
public interface IKtSessionValidator {


    /// <summary>
    /// Returns null for unknown, expired or revoked sessions, inactive accounts,
    /// or a security-version mismatch. Validate against authoritative backend
    /// data and enforce this API's intended recipient. Do not merely decode a
    /// token or trust client-supplied claims. Never log the bearer secret.
    /// Backend outages must throw, not return a successful identity.
    /// </summary>
    /// <param name="bearerToken">The bearer token to validate.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    ValueTask<KtValidatedSession?> ValidateAsync(string bearerToken, CancellationToken cancellationToken = default);
}
