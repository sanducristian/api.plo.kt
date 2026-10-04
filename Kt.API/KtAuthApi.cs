using Kt.Api.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Kt.Api.Modules;

public sealed class KtAuthApi : IKtApiModule {
    public string Segment => "auth";

    public void MapEndpoints(RouteGroupBuilder group) {
        group.MapGet("/check", Check)
            .WithName("Kt.Auth.Check");

        // Register your existing handlers here:
        //
        // group.MapPost("/login", LoginAsync)
        //     .AllowAnonymous();
        //
        // group.MapPost("/logout", LogoutAsync);
        //
        // Retain login rate limiting and existing cookie/CSRF
        // protections when moving those handlers.
    }

    private static IResult Check() {
        return TypedResults.NoContent();
    }
}
