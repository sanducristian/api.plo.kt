// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Fleet;

/// <summary>Maps one row of the fleet_position_latest table. Includes all 17 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("fleet_position_latest")]
public sealed record FleetPositionLatest
{
    /// <summary>Column: vehicle_id; SQL: bigint unsigned; not null.</summary>
    [Column("vehicle_id", TypeName = "bigint unsigned")]
    public required ulong VehicleId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: stream_id; SQL: bigint unsigned; not null.</summary>
    [Column("stream_id", TypeName = "bigint unsigned")]
    public required ulong StreamId { get; init; }

    /// <summary>Column: source_event_key; SQL: varchar(128); not null.</summary>
    [Column("source_event_key", TypeName = "varchar(128)")]
    public required string SourceEventKey { get; init; }

    /// <summary>Column: observed_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("observed_at_utc", TypeName = "datetime(6)")]
    public required DateTime ObservedAtUtc { get; init; }

    /// <summary>Column: received_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("received_at_utc", TypeName = "datetime(6)")]
    public required DateTime ReceivedAtUtc { get; init; }

    /// <summary>Column: latitude_deg; SQL: decimal(12,9); not null.</summary>
    [Column("latitude_deg", TypeName = "decimal(12,9)")]
    public required decimal LatitudeDeg { get; init; }

    /// <summary>Column: longitude_deg; SQL: decimal(12,9); not null.</summary>
    [Column("longitude_deg", TypeName = "decimal(12,9)")]
    public required decimal LongitudeDeg { get; init; }

    /// <summary>Column: altitude_m; SQL: decimal(14,6); nullable.</summary>
    [Column("altitude_m", TypeName = "decimal(14,6)")]
    public decimal? AltitudeM { get; init; }

    /// <summary>Column: altitude_reference; SQL: varchar(24); not null.</summary>
    [Column("altitude_reference", TypeName = "varchar(24)")]
    public required string AltitudeReference { get; init; }

    /// <summary>Column: vertical_crs_code; SQL: varchar(128); nullable.</summary>
    [Column("vertical_crs_code", TypeName = "varchar(128)")]
    public string? VerticalCrsCode { get; init; }

    /// <summary>Column: horizontal_crs_code; SQL: varchar(64); not null.</summary>
    [Column("horizontal_crs_code", TypeName = "varchar(64)")]
    public required string HorizontalCrsCode { get; init; }

    /// <summary>Column: heading_deg; SQL: decimal(9,6); nullable.</summary>
    [Column("heading_deg", TypeName = "decimal(9,6)")]
    public decimal? HeadingDeg { get; init; }

    /// <summary>Column: speed_mps; SQL: decimal(12,6); nullable.</summary>
    [Column("speed_mps", TypeName = "decimal(12,6)")]
    public decimal? SpeedMps { get; init; }

    /// <summary>Column: horizontal_accuracy_m; SQL: decimal(12,6); nullable.</summary>
    [Column("horizontal_accuracy_m", TypeName = "decimal(12,6)")]
    public decimal? HorizontalAccuracyM { get; init; }

    /// <summary>Column: vertical_accuracy_m; SQL: decimal(12,6); nullable.</summary>
    [Column("vertical_accuracy_m", TypeName = "decimal(12,6)")]
    public decimal? VerticalAccuracyM { get; init; }

    /// <summary>Column: fix_type; SQL: varchar(24); not null.</summary>
    [Column("fix_type", TypeName = "varchar(24)")]
    public required string FixType { get; init; }
}
