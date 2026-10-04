// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Reference.Units;

/// <summary>Maps one row of the sys_um_collection table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_um_collection")]
public sealed record UnitOfMeasureCollection
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global obj_id identity allocated via AllocateId()</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null. Application UUID stored as binary for safe API exposure</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: collection_code; SQL: varchar(32); not null. Unique invariant code (e.g., BOX_12, PACK_6, PALLET_STD)</summary>
    [Column("collection_code", TypeName = "varchar(32)")]
    public required string CollectionCode { get; init; }

    /// <summary>Column: umId; SQL: bigint unsigned; not null.</summary>
    [Column("umId", TypeName = "bigint unsigned")]
    public required ulong UmId { get; init; }

    /// <summary>Column: collection_symbol; SQL: varchar(32); nullable. Optional shorthand symbol for the collection set</summary>
    [Column("collection_symbol", TypeName = "varchar(32)")]
    public string? CollectionSymbol { get; init; }

    /// <summary>Column: base_unit_id; SQL: bigint unsigned; nullable. Base unit of measure contained inside this collection set</summary>
    [Column("base_unit_id", TypeName = "bigint unsigned")]
    public ulong? BaseUnitId { get; init; }

    /// <summary>Column: base_quantity; SQL: decimal(20,10); not null. Quantity of base units inside the set (e.g., 12.0 for a box of 12)</summary>
    [Column("base_quantity", TypeName = "decimal(20,10)")]
    public required decimal BaseQuantity { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null. Soft-disable status flag (1=Active, 0=Disabled)</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

    /// <summary>Column: collection_name; SQL: varchar(64); not null. Collection or package set name (e.g., Box of 12, Pack of 6)</summary>
    [Column("collection_name", TypeName = "varchar(64)")]
    public required string CollectionName { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. Immutable microsecond UTC creation timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null. Microsecond UTC update timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
