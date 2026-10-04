// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_channel table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_channel")]
public sealed record DeviceChannel
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global identity allocated via AllocateId()</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null. Application UUID stored as binary for safe API exposure</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: device_unit_id; SQL: bigint unsigned; not null. Parent hardware device unit reference</summary>
    [Column("device_unit_id", TypeName = "bigint unsigned")]
    public required ulong DeviceUnitId { get; init; }

    /// <summary>Column: measurement_definition_id; SQL: bigint unsigned; not null. Semantic measurement definition</summary>
    [Column("measurement_definition_id", TypeName = "bigint unsigned")]
    public required ulong MeasurementDefinitionId { get; init; }

    /// <summary>Column: channel_index; SQL: int unsigned; not null. Physical hardware or logical channel index (e.g., 1 for L1, 2 for L2)</summary>
    [Column("channel_index", TypeName = "int unsigned")]
    public required uint ChannelIndex { get; init; }

    /// <summary>Column: channel_code; SQL: varchar(64); not null. Device-unique channel identifier (e.g., CH1_FLOW, L1_ACTIVE_POWER)</summary>
    [Column("channel_code", TypeName = "varchar(64)")]
    public required string ChannelCode { get; init; }

    /// <summary>Column: unit_symbol; SQL: varchar(32); not null. Reporting unit symbol override if distinct from base unit</summary>
    [Column("unit_symbol", TypeName = "varchar(32)")]
    public required string UnitSymbol { get; init; }

    /// <summary>Column: value_data_type; SQL: varchar(32); not null. Data encoding type: DECIMAL, DOUBLE, INT, BOOLEAN, STRING, JSON</summary>
    [Column("value_data_type", TypeName = "varchar(32)")]
    public required string ValueDataType { get; init; }

    /// <summary>Column: decimal_precision; SQL: tinyint unsigned; nullable. Configured decimal precision digits for fixed-point parsing</summary>
    [Column("decimal_precision", TypeName = "tinyint unsigned")]
    public byte? DecimalPrecision { get; init; }

    /// <summary>Column: expected_interval_seconds; SQL: int unsigned; nullable. Configured sampling interval in seconds</summary>
    [Column("expected_interval_seconds", TypeName = "int unsigned")]
    public uint? ExpectedIntervalSeconds { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null. Active collection flag</summary>
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
