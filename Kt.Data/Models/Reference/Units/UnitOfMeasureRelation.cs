// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Reference.Units;

/// <summary>Maps one row of the sys_um_relation table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_um_relation")]
public sealed record UnitOfMeasureRelation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global obj_id identity allocated via AllocateId()</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null. Application UUID stored as binary for safe API exposure</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: from_unit_id; SQL: bigint unsigned; not null. Source unit of measure ID (references sys_um.id)</summary>
    [Column("from_unit_id", TypeName = "bigint unsigned")]
    public required ulong FromUnitId { get; init; }

    /// <summary>Column: to_unit_id; SQL: bigint unsigned; not null. Target unit of measure ID (references sys_um.id)</summary>
    [Column("to_unit_id", TypeName = "bigint unsigned")]
    public required ulong ToUnitId { get; init; }

    /// <summary>Column: gain_factor; SQL: decimal(20,10); not null. Multiplier factor applied during conversion</summary>
    [Column("gain_factor", TypeName = "decimal(20,10)")]
    public required decimal GainFactor { get; init; }

    /// <summary>Column: offset_value; SQL: decimal(20,10); not null. Offset value added during conversion (output = input * gain + offset)</summary>
    [Column("offset_value", TypeName = "decimal(20,10)")]
    public required decimal OffsetValue { get; init; }

    /// <summary>Column: relation_type; SQL: varchar(32); not null. Type indicator: EXACT, APPROXIMATE, FINANCIAL</summary>
    [Column("relation_type", TypeName = "varchar(32)")]
    public required string RelationType { get; init; }

    /// <summary>Column: is_bidirectional; SQL: tinyint(1); not null. Flags whether reverse conversion rule can be mathematically inferred</summary>
    [Column("is_bidirectional", TypeName = "tinyint(1)")]
    public required bool IsBidirectional { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null. Soft-disable status flag (1=Active, 0=Disabled)</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. Immutable microsecond UTC creation timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }
}
