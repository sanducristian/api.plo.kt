// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Workspaces;

/// <summary>Maps one row of the workspace_membership table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("workspace_membership")]
public sealed record WorkspaceMembership
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: platform_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("platform_user_id", TypeName = "bigint unsigned")]
    public required ulong PlatformUserId { get; init; }

    /// <summary>Column: status; SQL: varchar(16); not null. INVITED, ACTIVE, SUSPENDED or ENDED.</summary>
    [Column("status", TypeName = "varchar(16)")]
    public required string Status { get; init; }

    /// <summary>Column: invited_by_principal_id; SQL: bigint unsigned; nullable. Inviting principal; NULL for approved self-registration.</summary>
    [Column("invited_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? InvitedByPrincipalId { get; init; }

    /// <summary>Column: invited_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("invited_utc", TypeName = "datetime(6)")]
    public DateTime? InvitedUtc { get; init; }

    /// <summary>Column: joined_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("joined_utc", TypeName = "datetime(6)")]
    public DateTime? JoinedUtc { get; init; }

    /// <summary>Column: ended_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("ended_utc", TypeName = "datetime(6)")]
    public DateTime? EndedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
