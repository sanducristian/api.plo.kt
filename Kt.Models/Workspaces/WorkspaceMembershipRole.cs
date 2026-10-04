// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Workspaces;

/// <summary>Maps one row of the workspace_membership_role table. Includes all 6 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("workspace_membership_role")]
public sealed record WorkspaceMembershipRole
{
    /// <summary>Column: workspace_membership_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_membership_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceMembershipId { get; init; }

    /// <summary>Column: workspace_role_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_role_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceRoleId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null. Repeated workspace key used to prevent assigning a role from another workspace.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: assigned_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("assigned_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong AssignedByPrincipalId { get; init; }

    /// <summary>Column: assigned_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("assigned_utc", TypeName = "datetime(6)")]
    public required DateTime AssignedUtc { get; init; }

    /// <summary>Column: expires_utc; SQL: datetime(6); nullable. Optional UTC expiration for temporary membership roles.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("expires_utc", TypeName = "datetime(6)")]
    public DateTime? ExpiresUtc { get; init; }
}
