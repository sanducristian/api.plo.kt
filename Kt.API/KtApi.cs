using Kt.Api.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kt.Api;

public static class KtApi {
    public const string BasePath = "/api/v1";


    public static IServiceCollection AddKtApiModule<TModule>(this IServiceCollection services)
        where TModule : class, IKtApiModule {
        services.AddSingleton<IKtApiModule, TModule>();

        return services;
    }


    /// <summary>
    /// Validates that the module segment is a non-empty string starting with a lowercase letter
    /// </summary>
    /// <param name="segment">The module segment to validate.</param>
    /// <returns>True if the segment is valid; otherwise, false.</returns>
    private static bool IsValidSegment(string? segment) {
        // Null or empty segments are invalid
        if (string.IsNullOrEmpty(segment))
            return false;

        // Must start with a lowercase letter
        if (segment[0] < 'a' || segment[0] > 'z')
            return false;

        // Must only contain lowercase letters, digits, or hyphens
        foreach (var character in segment) {
            if ((character >= 'a' && character <= 'z') ||
                (character >= '0' && character <= '9') ||
                character == '-') {
                continue;
            }

            return false;
        }

        // Valid segment
        return true;
    }


    /// <summary>
    /// Registers public API endpoints and returns the authenticated
    /// route group used to register application modules.
    ///
    /// Call once during application startup.
    /// </summary>
    public static RouteGroupBuilder MapKtApi(this IEndpointRouteBuilder endpoints, string authenticationPolicy) {
        ArgumentNullException.ThrowIfNull(endpoints);

        if (string.IsNullOrWhiteSpace(authenticationPolicy)) {
            throw new ArgumentException("An authentication policy is required.",nameof(authenticationPolicy));
        }

        var modules = endpoints.ServiceProvider
            .GetServices<IKtApiModule>()
            .ToArray();

        var segments = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Validate everything before registering any module routes.
        foreach (var module in modules) {
            if (!IsValidSegment(module.Segment)) {
                throw new InvalidOperationException($"Invalid API segment '{module.Segment}' on module '{module.GetType().FullName}'.");
            }

            if (!segments.Add(module.Segment)) {
                throw new InvalidOperationException($"Duplicate API segment '{module.Segment}'.");
            }
        }

        var api = endpoints.MapGroup("/api/v1").RequireAuthorization(authenticationPolicy);

        foreach (var module in modules) {
            var group = api.MapGroup($"/{module.Segment}")
                .WithTags(module.Segment);

            module.MapEndpoints(group);
        }

        return api;



        //var api = endpoints.MapGroup(BasePath);

        //// Liveness only: this does not check database connectivity
        //// or confirm that startup preload has completed.
        //api.MapGet("/health", () => TypedResults.Text("KT API: OK"))
        //    .AllowAnonymous()
        //    .WithName("KtApi.Health")
        //    .WithTags("System");

        //var authenticated = api.MapGroup("").RequireAuthorization(authenticationPolicy);

        //// Confirms that the existing authentication policy accepts
        //// the current request. Does not grant application access.
        //authenticated.MapGet("/session/check", () => TypedResults.NoContent())
        //    .WithName("KtApi.SessionCheck")
        //    .WithTags("Session");

        //return authenticated;
    }
}