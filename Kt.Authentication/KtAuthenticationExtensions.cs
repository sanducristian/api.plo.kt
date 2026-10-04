using Kt.Authentication.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kt.Authentication;

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

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = KtAuthenticationDefaults.Scheme;
            options.DefaultChallengeScheme = KtAuthenticationDefaults.Scheme;
            options.DefaultForbidScheme = KtAuthenticationDefaults.Scheme;
        }).AddScheme<AuthenticationSchemeOptions, KtSessionAuthenticationHandler>(
            KtAuthenticationDefaults.Scheme, _ => { });

        services.AddAuthorization(options => {
            options.AddPolicy(KtAuthenticationDefaults.Policy, policy => {
                policy.AddAuthenticationSchemes(KtAuthenticationDefaults.Scheme);
                policy.RequireAuthenticatedUser();
            });
        });

        return services;
    }
}
