// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects.Dynamic;

/// <summary>Maps one row of the obj_dyn_quantity table. Includes all 3 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_dyn_quantity")]
public sealed record ObjectDynQuantity {
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: umId; SQL: bigint unsigned; not null.</summary>
    [Column("umId", TypeName = "bigint unsigned")]
    public required ulong UmId { get; init; }

    /// <summary>Column: value; SQL: double; nullable.</summary>
    [Column("value", TypeName = "double")]
    public double? Value { get; init; }
}
