// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Workspaces;

/// <summary>Maps one row of the workspace_role_permission table. Includes all 3 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("workspace_role_permission")]
public sealed record WorkspaceRolePermission
{
    /// <summary>Column: workspace_role_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_role_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceRoleId { get; init; }

    /// <summary>Column: workspace_permission_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_permission_id", TypeName = "bigint unsigned")]
    public required ulong WorkspacePermissionId { get; init; }

    /// <summary>Column: granted_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("granted_utc", TypeName = "datetime(6)")]
    public required DateTime GrantedUtc { get; init; }
}
