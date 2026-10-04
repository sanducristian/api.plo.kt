// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Reference.Units;

/// <summary>Maps one row of the sys_um table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_um")]
public sealed record UnitOfMeasure
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global obj_id identity allocated via AllocateId()</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null. Application UUID stored as binary for safe API exposure</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: class_id; SQL: bigint unsigned; nullable. Direct optional reference to sys_um_class.id</summary>
    [Column("class_id", TypeName = "bigint unsigned")]
    public ulong? ClassId { get; init; }

    /// <summary>Column: unit_symbol; SQL: varchar(32); not null. Display symbol representation (e.g., m, kg, s, °C)</summary>
    [Column("unit_symbol", TypeName = "varchar(32)")]
    public required string UnitSymbol { get; init; }

    /// <summary>Column: is_base_unit; SQL: tinyint(1); not null. Flag indicating whether this is the SI / base reference unit for its class</summary>
    [Column("is_base_unit", TypeName = "tinyint(1)")]
    public required bool IsBaseUnit { get; init; }

    /// <summary>Column: decimal_precision; SQL: tinyint unsigned; nullable. Suggested display decimal precision for formatted outputs</summary>
    [Column("decimal_precision", TypeName = "tinyint unsigned")]
    public byte? DecimalPrecision { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null. Soft-disable status flag (1=Active, 0=Disabled)</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

    /// <summary>Column: unit_name; SQL: varchar(64); not null. Human-readable unit name (e.g., Meter, Kilogram, Second)</summary>
    [Column("unit_name", TypeName = "varchar(64)")]
    public required string UnitName { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. Immutable microsecond UTC creation timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null. Microsecond UTC update timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
