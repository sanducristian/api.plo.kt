// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Transport;

/// <summary>Maps one row of the transport_cargo_line table. Includes all 17 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("transport_cargo_line")]
public sealed record TransportCargoLine
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: mission_id; SQL: bigint unsigned; not null.</summary>
    [Column("mission_id", TypeName = "bigint unsigned")]
    public required ulong MissionId { get; init; }

    /// <summary>Column: line_number; SQL: int unsigned; not null.</summary>
    [Column("line_number", TypeName = "int unsigned")]
    public required uint LineNumber { get; init; }

    /// <summary>Column: cargo_kind; SQL: varchar(24); not null.</summary>
    [Column("cargo_kind", TypeName = "varchar(24)")]
    public required string CargoKind { get; init; }

    /// <summary>Column: component_id; SQL: bigint unsigned; nullable.</summary>
    [Column("component_id", TypeName = "bigint unsigned")]
    public ulong? ComponentId { get; init; }

    /// <summary>Column: tracked_item_id; SQL: bigint unsigned; nullable.</summary>
    [Column("tracked_item_id", TypeName = "bigint unsigned")]
    public ulong? TrackedItemId { get; init; }

    /// <summary>Column: handling_unit_id; SQL: bigint unsigned; nullable.</summary>
    [Column("handling_unit_id", TypeName = "bigint unsigned")]
    public ulong? HandlingUnitId { get; init; }

    /// <summary>Column: description; SQL: varchar(1024); not null.</summary>
    [Column("description", TypeName = "varchar(1024)")]
    public required string Description { get; init; }

    /// <summary>Column: contents_snapshot_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("contents_snapshot_json", TypeName = "json")]
    public string? ContentsSnapshotJson { get; init; }

    /// <summary>Column: owner_party_id; SQL: bigint unsigned; not null.</summary>
    [Column("owner_party_id", TypeName = "bigint unsigned")]
    public required ulong OwnerPartyId { get; init; }

    /// <summary>Column: quantity; SQL: decimal(20,6); not null.</summary>
    [Column("quantity", TypeName = "decimal(20,6)")]
    public required decimal Quantity { get; init; }

    /// <summary>Column: unit_id; SQL: bigint unsigned; not null.</summary>
    [Column("unit_id", TypeName = "bigint unsigned")]
    public required ulong UnitId { get; init; }

    /// <summary>Column: gross_mass_kg; SQL: decimal(20,6); nullable.</summary>
    [Column("gross_mass_kg", TypeName = "decimal(20,6)")]
    public decimal? GrossMassKg { get; init; }

    /// <summary>Column: volume_m3; SQL: decimal(20,6); nullable.</summary>
    [Column("volume_m3", TypeName = "decimal(20,6)")]
    public decimal? VolumeM3 { get; init; }

    /// <summary>Column: pickup_stop_id; SQL: bigint unsigned; not null.</summary>
    [Column("pickup_stop_id", TypeName = "bigint unsigned")]
    public required ulong PickupStopId { get; init; }

    /// <summary>Column: dropoff_stop_id; SQL: bigint unsigned; not null.</summary>
    [Column("dropoff_stop_id", TypeName = "bigint unsigned")]
    public required ulong DropoffStopId { get; init; }
}
