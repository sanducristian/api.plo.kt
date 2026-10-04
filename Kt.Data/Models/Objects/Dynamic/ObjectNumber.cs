// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects.Dynamic;

/// <summary>Maps one row of the obj_dyn_number table. Includes all 5 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_dyn_number")]
public sealed record ObjectDynNumber {
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: value; SQL: bigint unsigned; not null.</summary>
    [Column("value", TypeName = "bigint unsigned")]
    public required ulong Value { get; init; }

    /// <summary>Column: year; SQL: int unsigned; not null.</summary>
    [Column("year", TypeName = "int unsigned")]
    public required uint Year { get; init; }

    /// <summary>Column: month; SQL: int unsigned; not null.</summary>
    [Column("month", TypeName = "int unsigned")]
    public required uint Month { get; init; }

    /// <summary>Column: roleId; SQL: bigint; nullable.</summary>
    [Column("roleId", TypeName = "bigint")]
    public long? RoleId { get; init; }
}
