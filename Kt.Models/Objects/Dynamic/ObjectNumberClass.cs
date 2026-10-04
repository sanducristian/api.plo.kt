// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects.Dynamic;

/// <summary>Maps one row of the obj_dyn_number_class table. Includes all 7 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_dyn_number_class")]
public sealed record ObjectNumberClass
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: typeId; SQL: bigint unsigned; not null.</summary>
    [Column("typeId", TypeName = "bigint unsigned")]
    public required ulong TypeId { get; init; }

    /// <summary>Column: prefix; SQL: varchar(64); nullable.</summary>
    [Column("prefix", TypeName = "varchar(64)")]
    public string? Prefix { get; init; }

    /// <summary>Column: value; SQL: varchar(64); nullable.</summary>
    [Column("value", TypeName = "varchar(64)")]
    public string? Value { get; init; }

    /// <summary>Column: sufix; SQL: varchar(64); nullable.</summary>
    [Column("sufix", TypeName = "varchar(64)")]
    public string? Sufix { get; init; }

    /// <summary>Column: resetEachYear; SQL: tinyint(1); nullable.</summary>
    [Column("resetEachYear", TypeName = "tinyint(1)")]
    public bool? ResetEachYear { get; init; }

    /// <summary>Column: resetEachMonth; SQL: tinyint(1); nullable.</summary>
    [Column("resetEachMonth", TypeName = "tinyint(1)")]
    public bool? ResetEachMonth { get; init; }
}
