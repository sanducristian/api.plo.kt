// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_measurement_definition table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_measurement_definition")]
public sealed record DeviceMeasurementDefinition
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global identity allocated via AllocateId()</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null. Application UUID stored as binary for safe API exposure</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: measurement_code; SQL: varchar(64); not null. Unique identifier (e.g., VOL_WATER_M3, ACTIVE_POWER_KW, ENERGY_KWH)</summary>
    [Column("measurement_code", TypeName = "varchar(64)")]
    public required string MeasurementCode { get; init; }

    /// <summary>Column: measurement_name; SQL: varchar(128); not null. Human-readable measurement name</summary>
    [Column("measurement_name", TypeName = "varchar(128)")]
    public required string MeasurementName { get; init; }

    /// <summary>Column: physical_domain; SQL: varchar(32); not null. Domain classification: WATER, ELECTRICITY, GAS, THERMAL, ENVIRONMENTAL</summary>
    [Column("physical_domain", TypeName = "varchar(32)")]
    public required string PhysicalDomain { get; init; }

    /// <summary>Column: base_unit_symbol; SQL: varchar(32); not null. Standard ISO/SI base unit symbol (e.g., m3, kWh, kW, L/s, C)</summary>
    [Column("base_unit_symbol", TypeName = "varchar(32)")]
    public required string BaseUnitSymbol { get; init; }

    /// <summary>Column: description; SQL: varchar(255); nullable. Detailed measurement scope or calculation context</summary>
    [Column("description", TypeName = "varchar(255)")]
    public string? Description { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null. Active flag enabling channel configuration</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. Microsecond UTC creation timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null. Microsecond UTC update timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
