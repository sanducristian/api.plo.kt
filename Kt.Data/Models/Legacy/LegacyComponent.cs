// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Legacy;

/// <summary>Maps one row of the legacy_component table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("legacy_component")]
public sealed record LegacyComponent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: manufacturerId; SQL: bigint unsigned; nullable.</summary>
    [Column("manufacturerId", TypeName = "bigint unsigned")]
    public ulong? ManufacturerId { get; init; }

    /// <summary>Column: weight; SQL: double; nullable. Weight in grams</summary>
    [Column("weight", TypeName = "double")]
    public double? Weight { get; init; }

    /// <summary>Column: douaneId; SQL: varchar(64); nullable.</summary>
    [Column("douaneId", TypeName = "varchar(64)")]
    public string? DouaneId { get; init; }
}
