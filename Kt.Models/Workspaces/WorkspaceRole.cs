// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Workspaces;

/// <summary>Maps one row of the workspace_role table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("workspace_role")]
public sealed record WorkspaceRole
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: role_code; SQL: varchar(64); not null. Stable role code within the workspace, such as OWNER, FAMILY_MEMBER or CAREGIVER.</summary>
    [Column("role_code", TypeName = "varchar(64)")]
    public required string RoleCode { get; init; }

    /// <summary>Column: display_name; SQL: varchar(255); not null. Default English display name; localized UI text uses resources.</summary>
    [Column("display_name", TypeName = "varchar(255)")]
    public required string DisplayName { get; init; }

    /// <summary>Column: description; SQL: varchar(1000); nullable.</summary>
    [Column("description", TypeName = "varchar(1000)")]
    public string? Description { get; init; }

    /// <summary>Column: is_system_role; SQL: tinyint(1); not null. True when application policy protects the role from ordinary deletion.</summary>
    [Column("is_system_role", TypeName = "tinyint(1)")]
    public required bool IsSystemRole { get; init; }

    /// <summary>Column: active; SQL: tinyint(1); not null.</summary>
    [Column("active", TypeName = "tinyint(1)")]
    public required bool Active { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }
}
