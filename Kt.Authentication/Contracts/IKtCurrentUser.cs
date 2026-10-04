namespace Kt.Authentication;



/// <summary>Request-scoped access to the identity validated by KtSession.</summary>
public interface IKtCurrentUser {
    /// <summary>
    /// Gets the currently authenticated user, or null if no user is authenticated.
    /// </summary>
    KtAuthenticatedUser? User { get; }



    /// <summary>
    /// Gets a value indicating whether the user is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }
}
