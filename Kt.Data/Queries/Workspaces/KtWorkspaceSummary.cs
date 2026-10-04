// Move the corresponding previous declaration to this namespace; do not leave duplicate old types.
#nullable enable
using System;
using System.Collections.Generic;

namespace Kt.Data.Queries.Workspaces;

/// <summary>Selected or combined repository result; not a complete table mapping or public API contract.</summary>
/// <remarks>Existing positional constructor retained for repository compatibility. Map public API identifiers deliberately.</remarks>
public sealed record KtWorkspaceSummary(ulong Id, string Name, string WorkspaceType);
