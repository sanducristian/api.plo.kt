using Kt.Authentication.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kt.Authentication;


/// <summary>
/// Provides extension methods for registering and configuring KT session authentication in an ASP.NET Core application.
/// </summary>
public static class KtAuthenticationExtensions {


    /// <summary>
    /// Registers shared request authentication. Call once in the API host.
    /// TValidator adapts the existing authoritative identity/session backend.
    /// </summary>
    /// <param name="services">The service collection to add authentication services to.</param>
    public static IServiceCollection AddKtSessionAuthentication<TValidator>(this IServiceCollection services)
        where TValidator : class, IKtSessionValidator {
        services.AddScoped<IKtSessionValidator, TValidator>();
        services.AddHttpContextAccessor();
        services.TryAddSingleton<TimeProvider>(TimeProvider.System);
        services.AddScoped<IKtCurrentUser, KtCurrentUser>();


        // Configure authentication to use the KT session scheme. This sets up the default authentication, challenge, and forbid schemes to use the KT session handler.
        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = KtAuthenticationDefaults.Scheme;
            options.DefaultChallengeScheme = KtAuthenticationDefaults.Scheme;
            options.DefaultForbidScheme = KtAuthenticationDefaults.Scheme;
        }).AddScheme<AuthenticationSchemeOptions, KtSessionAuthenticationHandler>(
            KtAuthenticationDefaults.Scheme, _ => { });


        // Set up a policy that requires authentication using the KT session scheme. This policy can be applied to controllers or actions to enforce authentication.
        services.AddAuthorization(options => {
            options.AddPolicy(KtAuthenticationDefaults.Policy, policy => {
                policy.AddAuthenticationSchemes(KtAuthenticationDefaults.Scheme);
                policy.RequireAuthenticatedUser();
            });
        });

        return services;
    }
}
