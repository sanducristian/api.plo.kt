// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Fleet;

/// <summary>Maps one row of the fleet_vehicle_model table. Includes all 18 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("fleet_vehicle_model")]
public sealed record FleetVehicleModel
{
    /// <summary>Column: device_model_id; SQL: bigint unsigned; not null.</summary>
    [Column("device_model_id", TypeName = "bigint unsigned")]
    public required ulong DeviceModelId { get; init; }

    /// <summary>Column: platform_type; SQL: varchar(24); not null. AERIAL, STREET, BOAT or AMPHIBIOUS</summary>
    [Column("platform_type", TypeName = "varchar(24)")]
    public required string PlatformType { get; init; }

    /// <summary>Column: service_type; SQL: varchar(24); not null. CARGO, PASSENGER or MIXED</summary>
    [Column("service_type", TypeName = "varchar(24)")]
    public required string ServiceType { get; init; }

    /// <summary>Column: propulsion_type; SQL: varchar(24); not null.</summary>
    [Column("propulsion_type", TypeName = "varchar(24)")]
    public required string PropulsionType { get; init; }

    /// <summary>Column: max_speed_mps; SQL: decimal(12,6); not null. Design/configuration limit, not a legal permission to operate</summary>
    [Column("max_speed_mps", TypeName = "decimal(12,6)")]
    public required decimal MaxSpeedMps { get; init; }

    /// <summary>Column: max_payload_kg; SQL: decimal(20,6); not null.</summary>
    [Column("max_payload_kg", TypeName = "decimal(20,6)")]
    public required decimal MaxPayloadKg { get; init; }

    /// <summary>Column: cargo_volume_m3; SQL: decimal(20,6); nullable.</summary>
    [Column("cargo_volume_m3", TypeName = "decimal(20,6)")]
    public decimal? CargoVolumeM3 { get; init; }

    /// <summary>Column: passenger_capacity; SQL: smallint unsigned; not null.</summary>
    [Column("passenger_capacity", TypeName = "smallint unsigned")]
    public required ushort PassengerCapacity { get; init; }

    /// <summary>Column: max_installed_battery_capacity_wh; SQL: decimal(20,6); nullable. Maximum total nominal energy supported by model</summary>
    [Column("max_installed_battery_capacity_wh", TypeName = "decimal(20,6)")]
    public decimal? MaxInstalledBatteryCapacityWh { get; init; }

    /// <summary>Column: nominal_voltage_v; SQL: decimal(12,6); nullable.</summary>
    [Column("nominal_voltage_v", TypeName = "decimal(12,6)")]
    public decimal? NominalVoltageV { get; init; }

    /// <summary>Column: empty_mass_kg; SQL: decimal(20,6); nullable.</summary>
    [Column("empty_mass_kg", TypeName = "decimal(20,6)")]
    public decimal? EmptyMassKg { get; init; }

    /// <summary>Column: max_gross_mass_kg; SQL: decimal(20,6); nullable.</summary>
    [Column("max_gross_mass_kg", TypeName = "decimal(20,6)")]
    public decimal? MaxGrossMassKg { get; init; }

    /// <summary>Column: length_m; SQL: decimal(12,6); nullable.</summary>
    [Column("length_m", TypeName = "decimal(12,6)")]
    public decimal? LengthM { get; init; }

    /// <summary>Column: width_m; SQL: decimal(12,6); nullable.</summary>
    [Column("width_m", TypeName = "decimal(12,6)")]
    public decimal? WidthM { get; init; }

    /// <summary>Column: height_m; SQL: decimal(12,6); nullable.</summary>
    [Column("height_m", TypeName = "decimal(12,6)")]
    public decimal? HeightM { get; init; }

    /// <summary>Column: specifications_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("specifications_json", TypeName = "json")]
    public string? SpecificationsJson { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }
}
