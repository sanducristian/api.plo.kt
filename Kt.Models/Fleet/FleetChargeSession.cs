// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Fleet;

/// <summary>Maps one row of the fleet_charge_session table. Includes all 20 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("fleet_charge_session")]
public sealed record FleetChargeSession
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: battery_id; SQL: bigint unsigned; not null.</summary>
    [Column("battery_id", TypeName = "bigint unsigned")]
    public required ulong BatteryId { get; init; }

    /// <summary>Column: vehicle_id; SQL: bigint unsigned; nullable.</summary>
    [Column("vehicle_id", TypeName = "bigint unsigned")]
    public ulong? VehicleId { get; init; }

    /// <summary>Column: location_id; SQL: bigint unsigned; nullable.</summary>
    [Column("location_id", TypeName = "bigint unsigned")]
    public ulong? LocationId { get; init; }

    /// <summary>Column: charger_reference; SQL: varchar(128); nullable.</summary>
    [Column("charger_reference", TypeName = "varchar(128)")]
    public string? ChargerReference { get; init; }

    /// <summary>Column: source_session_key; SQL: varchar(128); not null.</summary>
    [Column("source_session_key", TypeName = "varchar(128)")]
    public required string SourceSessionKey { get; init; }

    /// <summary>Column: started_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("started_at_utc", TypeName = "datetime(6)")]
    public required DateTime StartedAtUtc { get; init; }

    /// <summary>Column: ended_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("ended_at_utc", TypeName = "datetime(6)")]
    public DateTime? EndedAtUtc { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null.</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: start_soc_pct; SQL: decimal(7,4); nullable.</summary>
    [Column("start_soc_pct", TypeName = "decimal(7,4)")]
    public decimal? StartSocPct { get; init; }

    /// <summary>Column: end_soc_pct; SQL: decimal(7,4); nullable.</summary>
    [Column("end_soc_pct", TypeName = "decimal(7,4)")]
    public decimal? EndSocPct { get; init; }

    /// <summary>Column: grid_energy_wh; SQL: decimal(20,6); nullable.</summary>
    [Column("grid_energy_wh", TypeName = "decimal(20,6)")]
    public decimal? GridEnergyWh { get; init; }

    /// <summary>Column: battery_energy_added_wh; SQL: decimal(20,6); nullable.</summary>
    [Column("battery_energy_added_wh", TypeName = "decimal(20,6)")]
    public decimal? BatteryEnergyAddedWh { get; init; }

    /// <summary>Column: peak_charge_power_w; SQL: decimal(20,6); nullable.</summary>
    [Column("peak_charge_power_w", TypeName = "decimal(20,6)")]
    public decimal? PeakChargePowerW { get; init; }

    /// <summary>Column: stop_reason; SQL: varchar(255); nullable.</summary>
    [Column("stop_reason", TypeName = "varchar(255)")]
    public string? StopReason { get; init; }

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
}
