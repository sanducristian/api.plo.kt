using Kt.Api.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace Kt.Api.Modules;

public sealed class KtUsersApi : IKtApiModule {
    public string Segment => "users";

    public void MapEndpoints(RouteGroupBuilder group) {
        // Add once connected to the existing identity backend:
        //
        // GET   /me
        // GET   /{id}
        // PATCH /{id}
        //
        // Listing or modifying other users requires explicit
        // permissions and validated company/personal scope.
    }
}