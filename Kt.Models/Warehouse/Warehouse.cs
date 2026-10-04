// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Warehouse;

/// <summary>Maps one row of the wh_warehouse table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("wh_warehouse")]
public sealed record Warehouse
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: location_id; SQL: bigint unsigned; not null.</summary>
    [Column("location_id", TypeName = "bigint unsigned")]
    public required ulong LocationId { get; init; }

    /// <summary>Column: code; SQL: varchar(64); not null.</summary>
    [Column("code", TypeName = "varchar(64)")]
    public required string Code { get; init; }

    /// <summary>Column: name; SQL: varchar(255); not null.</summary>
    [Column("name", TypeName = "varchar(255)")]
    public required string Name { get; init; }

    /// <summary>Column: is_active; SQL: tinyint unsigned; not null.</summary>
    [Column("is_active", TypeName = "tinyint unsigned")]
    public required byte IsActive { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }
}
