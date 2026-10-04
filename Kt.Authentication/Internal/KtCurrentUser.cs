using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Kt.Authentication.Internal;


/// <summary>
/// Provides access to the currently authenticated user, if any, based on the HTTP context.
/// </summary>
/// <param name="accessor">The HTTP context accessor.</param>
internal sealed class KtCurrentUser(IHttpContextAccessor accessor) : IKtCurrentUser {

    public bool IsAuthenticated => User is not null;


    /// <summary>
    /// Gets the currently authenticated user, or null if no user is authenticated.
    /// </summary>
    public KtAuthenticatedUser? User {
        get {
            var identity = accessor.HttpContext?.User.Identities.FirstOrDefault(
                item => item.IsAuthenticated &&
                        item.AuthenticationType == KtAuthenticationDefaults.Scheme);

            var userId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return string.IsNullOrWhiteSpace(userId)
                ? null
                : new KtAuthenticatedUser(userId,
                    identity?.FindFirst(ClaimTypes.Name)?.Value);
        }
    }

}
