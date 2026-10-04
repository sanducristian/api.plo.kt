// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Workspaces;

/// <summary>Maps one row of the workspace_permission table. Includes all 5 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("workspace_permission")]
public sealed record WorkspacePermission
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: permission_code; SQL: varchar(160); not null. Stable module.action permission code; never translate this value.</summary>
    [Column("permission_code", TypeName = "varchar(160)")]
    public required string PermissionCode { get; init; }

    /// <summary>Column: description; SQL: varchar(1000); not null. English technical explanation of the permission.</summary>
    [Column("description", TypeName = "varchar(1000)")]
    public required string Description { get; init; }

    /// <summary>Column: module_code; SQL: varchar(64); not null. Owning PLO module code.</summary>
    [Column("module_code", TypeName = "varchar(64)")]
    public required string ModuleCode { get; init; }

    /// <summary>Column: active; SQL: tinyint(1); not null.</summary>
    [Column("active", TypeName = "tinyint(1)")]
    public required bool Active { get; init; }
}
