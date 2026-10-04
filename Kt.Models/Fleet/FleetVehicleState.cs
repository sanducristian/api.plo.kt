// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Fleet;

/// <summary>Maps one row of the fleet_vehicle_state table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("fleet_vehicle_state")]
public sealed record FleetVehicleState
{
    /// <summary>Column: vehicle_id; SQL: bigint unsigned; not null.</summary>
    [Column("vehicle_id", TypeName = "bigint unsigned")]
    public required ulong VehicleId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: observed_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("observed_at_utc", TypeName = "datetime(6)")]
    public required DateTime ObservedAtUtc { get; init; }

    /// <summary>Column: received_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("received_at_utc", TypeName = "datetime(6)")]
    public required DateTime ReceivedAtUtc { get; init; }

    /// <summary>Column: source_event_key; SQL: varchar(128); not null.</summary>
    [Column("source_event_key", TypeName = "varchar(128)")]
    public required string SourceEventKey { get; init; }

    /// <summary>Column: operational_status; SQL: varchar(24); not null.</summary>
    [Column("operational_status", TypeName = "varchar(24)")]
    public required string OperationalStatus { get; init; }

    /// <summary>Column: charge_status; SQL: varchar(24); not null.</summary>
    [Column("charge_status", TypeName = "varchar(24)")]
    public required string ChargeStatus { get; init; }

    /// <summary>Column: battery_soc_pct; SQL: decimal(7,4); nullable. Vehicle aggregate; unknown is NULL, not zero</summary>
    [Column("battery_soc_pct", TypeName = "decimal(7,4)")]
    public decimal? BatterySocPct { get; init; }

    /// <summary>Column: battery_energy_remaining_wh; SQL: decimal(20,6); nullable.</summary>
    [Column("battery_energy_remaining_wh", TypeName = "decimal(20,6)")]
    public decimal? BatteryEnergyRemainingWh { get; init; }

    /// <summary>Column: speed_mps; SQL: decimal(12,6); nullable.</summary>
    [Column("speed_mps", TypeName = "decimal(12,6)")]
    public decimal? SpeedMps { get; init; }

    /// <summary>Column: current_payload_kg; SQL: decimal(20,6); nullable.</summary>
    [Column("current_payload_kg", TypeName = "decimal(20,6)")]
    public decimal? CurrentPayloadKg { get; init; }

    /// <summary>Column: passenger_count; SQL: smallint unsigned; nullable.</summary>
    [Column("passenger_count", TypeName = "smallint unsigned")]
    public ushort? PassengerCount { get; init; }

    /// <summary>Column: last_contact_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("last_contact_at_utc", TypeName = "datetime(6)")]
    public required DateTime LastContactAtUtc { get; init; }
}
