// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Fleet;

/// <summary>Maps one row of the fleet_battery_state table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("fleet_battery_state")]
public sealed record FleetBatteryState
{
    /// <summary>Column: battery_id; SQL: bigint unsigned; not null.</summary>
    [Column("battery_id", TypeName = "bigint unsigned")]
    public required ulong BatteryId { get; init; }

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

    /// <summary>Column: soc_pct; SQL: decimal(7,4); nullable.</summary>
    [Column("soc_pct", TypeName = "decimal(7,4)")]
    public decimal? SocPct { get; init; }

    /// <summary>Column: soh_pct; SQL: decimal(7,4); nullable.</summary>
    [Column("soh_pct", TypeName = "decimal(7,4)")]
    public decimal? SohPct { get; init; }

    /// <summary>Column: usable_capacity_wh; SQL: decimal(20,6); nullable.</summary>
    [Column("usable_capacity_wh", TypeName = "decimal(20,6)")]
    public decimal? UsableCapacityWh { get; init; }

    /// <summary>Column: voltage_v; SQL: decimal(12,6); nullable.</summary>
    [Column("voltage_v", TypeName = "decimal(12,6)")]
    public decimal? VoltageV { get; init; }

    /// <summary>Column: current_a; SQL: decimal(14,6); nullable. Positive into battery, negative during discharge</summary>
    [Column("current_a", TypeName = "decimal(14,6)")]
    public decimal? CurrentA { get; init; }

    /// <summary>Column: temperature_c; SQL: decimal(9,4); nullable.</summary>
    [Column("temperature_c", TypeName = "decimal(9,4)")]
    public decimal? TemperatureC { get; init; }

    /// <summary>Column: equivalent_full_cycles; SQL: decimal(16,6); nullable.</summary>
    [Column("equivalent_full_cycles", TypeName = "decimal(16,6)")]
    public decimal? EquivalentFullCycles { get; init; }
}
