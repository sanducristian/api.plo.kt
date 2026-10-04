// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Fleet;

/// <summary>Maps one row of the fleet_battery_installation table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("fleet_battery_installation")]
public sealed record FleetBatteryInstallation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: vehicle_id; SQL: bigint unsigned; not null.</summary>
    [Column("vehicle_id", TypeName = "bigint unsigned")]
    public required ulong VehicleId { get; init; }

    /// <summary>Column: battery_id; SQL: bigint unsigned; not null.</summary>
    [Column("battery_id", TypeName = "bigint unsigned")]
    public required ulong BatteryId { get; init; }

    /// <summary>Column: slot_code; SQL: varchar(32); not null.</summary>
    [Column("slot_code", TypeName = "varchar(32)")]
    public required string SlotCode { get; init; }

    /// <summary>Column: installed_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("installed_at_utc", TypeName = "datetime(6)")]
    public required DateTime InstalledAtUtc { get; init; }

    /// <summary>Column: removed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("removed_at_utc", TypeName = "datetime(6)")]
    public DateTime? RemovedAtUtc { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }

    /// <summary>Column: active_battery_id; SQL: bigint unsigned; nullable.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_battery_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveBatteryId { get; init; }

    /// <summary>Column: active_vehicle_id; SQL: bigint unsigned; nullable.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_vehicle_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveVehicleId { get; init; }
}
