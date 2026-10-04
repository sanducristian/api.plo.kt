// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Warehouse;

/// <summary>Maps one row of the wh_storage_location table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("wh_storage_location")]
public sealed record StorageLocation
{
    /// <summary>Column: location_id; SQL: bigint unsigned; not null.</summary>
    [Column("location_id", TypeName = "bigint unsigned")]
    public required ulong LocationId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: warehouse_id; SQL: bigint unsigned; not null.</summary>
    [Column("warehouse_id", TypeName = "bigint unsigned")]
    public required ulong WarehouseId { get; init; }

    /// <summary>Column: can_store_stock; SQL: tinyint unsigned; not null.</summary>
    [Column("can_store_stock", TypeName = "tinyint unsigned")]
    public required byte CanStoreStock { get; init; }

    /// <summary>Column: max_mass_kg; SQL: decimal(20,6); nullable.</summary>
    [Column("max_mass_kg", TypeName = "decimal(20,6)")]
    public decimal? MaxMassKg { get; init; }

    /// <summary>Column: max_volume_m3; SQL: decimal(20,6); nullable.</summary>
    [Column("max_volume_m3", TypeName = "decimal(20,6)")]
    public decimal? MaxVolumeM3 { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }
}
