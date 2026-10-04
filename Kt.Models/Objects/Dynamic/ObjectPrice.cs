// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects.Dynamic;

/// <summary>Maps one row of the obj_dyn_price table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_dyn_price")]
public sealed record ObjectPrice
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; not null.</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public required ulong CurrencyId { get; init; }

    /// <summary>Column: value; SQL: decimal(20,6) unsigned; not null.</summary>
    [Column("value", TypeName = "decimal(20,6) unsigned")]
    public required decimal Value { get; init; }

    /// <summary>Column: date; SQL: date; not null.</summary>
    [Column("date", TypeName = "date")]
    public required DateOnly Date { get; init; }
}
