// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Workspaces;

/// <summary>Maps one row of the workspace table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("workspace")]
public sealed record Workspace
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: workspace_type; SQL: varchar(24); not null. COMPANY, PERSONAL or HOUSEHOLD.</summary>
    [Column("workspace_type", TypeName = "varchar(24)")]
    public required string WorkspaceType { get; init; }

    /// <summary>Column: name; SQL: varchar(255); not null. Workspace name shown to members.</summary>
    [Column("name", TypeName = "varchar(255)")]
    public required string Name { get; init; }

    /// <summary>Column: status; SQL: varchar(16); not null. ACTIVE, SUSPENDED or CLOSED.</summary>
    [Column("status", TypeName = "varchar(16)")]
    public required string Status { get; init; }

    /// <summary>Column: legacy_company_id; SQL: bigint unsigned; nullable. Optional link to the existing company table during migration.</summary>
    [Column("legacy_company_id", TypeName = "bigint unsigned")]
    public ulong? LegacyCompanyId { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null. Human or system principal that created the workspace.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }

    /// <summary>Column: closed_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("closed_utc", TypeName = "datetime(6)")]
    public DateTime? ClosedUtc { get; init; }
}
