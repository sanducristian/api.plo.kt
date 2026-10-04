// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Reference.Units;

/// <summary>Maps one row of the sys_um_class table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_um_class")]
public sealed record UnitOfMeasureClass
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global obj_id identity allocated via AllocateId()</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null. Application UUID stored as binary for safe API exposure</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: class_code; SQL: varchar(32); not null. Unique invariant code (e.g., LENGTH, MASS, ELECTRIC_ENERGY)</summary>
    [Column("class_code", TypeName = "varchar(32)")]
    public required string ClassCode { get; init; }

    /// <summary>Column: class_name; SQL: varchar(64); not null. Unit class name (e.g., Length, Mass, Electric Current, Volume)</summary>
    [Column("class_name", TypeName = "varchar(64)")]
    public required string ClassName { get; init; }

    /// <summary>Column: base_unit_id; SQL: bigint unsigned; nullable. Reference to the default base sys_um.id for this measurement class</summary>
    [Column("base_unit_id", TypeName = "bigint unsigned")]
    public ulong? BaseUnitId { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null. Soft-disable status flag (1=Active, 0=Disabled)</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. Immutable microsecond UTC creation timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null. Microsecond UTC update timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
