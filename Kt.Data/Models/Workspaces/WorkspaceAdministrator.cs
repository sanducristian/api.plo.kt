// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Workspaces;

/// <summary>Maps one row of the workspace_administrator table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("workspace_administrator")]
public sealed record WorkspaceAdministrator
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: company_workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("company_workspace_id", TypeName = "bigint unsigned")]
    public required ulong CompanyWorkspaceId { get; init; }

    /// <summary>Column: workspace_membership_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_membership_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceMembershipId { get; init; }

    /// <summary>Column: administrator_slot; SQL: tinyint unsigned; not null.</summary>
    [Column("administrator_slot", TypeName = "tinyint unsigned")]
    public required byte AdministratorSlot { get; init; }

    /// <summary>Column: status; SQL: varchar(32); not null. ACTIVE or REVOKED.</summary>
    [Column("status", TypeName = "varchar(32)")]
    public required string Status { get; init; }

    /// <summary>Column: assigned_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("assigned_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong AssignedByPrincipalId { get; init; }

    /// <summary>Column: assigned_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("assigned_utc", TypeName = "datetime(6)")]
    public required DateTime AssignedUtc { get; init; }

    /// <summary>Column: revoked_by_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("revoked_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? RevokedByPrincipalId { get; init; }

    /// <summary>Column: revoked_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("revoked_utc", TypeName = "datetime(6)")]
    public DateTime? RevokedUtc { get; init; }

    /// <summary>Column: revocation_reason; SQL: varchar(255); nullable.</summary>
    [Column("revocation_reason", TypeName = "varchar(255)")]
    public string? RevocationReason { get; init; }

    /// <summary>Column: active_company_workspace_id; SQL: bigint unsigned; nullable. Generated workspace key present only for active administrator assignments.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_company_workspace_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveCompanyWorkspaceId { get; init; }

    /// <summary>Column: active_administrator_slot; SQL: tinyint unsigned; nullable. Generated slot present only for active administrator assignments.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_administrator_slot", TypeName = "tinyint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public byte? ActiveAdministratorSlot { get; init; }

    /// <summary>Column: active_workspace_membership_id; SQL: bigint unsigned; nullable. Generated membership key present only for active administrator assignments.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_workspace_membership_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveWorkspaceMembershipId { get; init; }
}
