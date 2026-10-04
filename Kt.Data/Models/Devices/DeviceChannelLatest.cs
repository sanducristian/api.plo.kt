// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_channel_latest table. Includes all 7 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_channel_latest")]
public sealed record DeviceChannelLatest
{
    /// <summary>Column: device_channel_id; SQL: bigint unsigned; not null. Primary key matching device_channel.id</summary>
    [Column("device_channel_id", TypeName = "bigint unsigned")]
    public required ulong DeviceChannelId { get; init; }

    /// <summary>Column: last_read_utc; SQL: datetime(6); not null. Exact UTC timestamp of the latest accepted sample</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("last_read_utc", TypeName = "datetime(6)")]
    public required DateTime LastReadUtc { get; init; }

    /// <summary>Column: numeric_value; SQL: decimal(20,6); nullable. Exact fixed-point value representation for numeric metrics</summary>
    [Column("numeric_value", TypeName = "decimal(20,6)")]
    public decimal? NumericValue { get; init; }

    /// <summary>Column: text_value; SQL: varchar(255); nullable. Raw or string representation for state/alarm values</summary>
    [Column("text_value", TypeName = "varchar(255)")]
    public string? TextValue { get; init; }

    /// <summary>Column: status_code; SQL: varchar(32); not null. Quality indicator: OK, OUT_OF_BOUNDS, SENSOR_FAULT, STALE</summary>
    [Column("status_code", TypeName = "varchar(32)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: raw_payload_json; SQL: json; nullable. Optional diagnostic snapshot payload</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("raw_payload_json", TypeName = "json")]
    public string? RawPayloadJson { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null. Microsecond UTC write timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
