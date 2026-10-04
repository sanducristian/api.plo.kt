using Microsoft.AspNetCore.Routing;

namespace Kt.Api.Abstractions;

/// <summary>
/// Defines one API module mounted below /api/v1/{Segment}.
/// </summary>
public interface IKtApiModule {
    /// <summary>
    /// Unique, lowercase route segment, for example "auth" or "users".
    /// </summary>
    string Segment { get; }

    /// <summary>
    /// Registers endpoints relative to the module's route group.
    /// </summary>
    void MapEndpoints(RouteGroupBuilder group);
}

