// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Transport;

/// <summary>Maps one row of the transport_mission_stop table. Includes all 17 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("transport_mission_stop")]
public sealed record TransportMissionStop
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

    /// <summary>Column: sequence_no; SQL: int unsigned; not null.</summary>
    [Column("sequence_no", TypeName = "int unsigned")]
    public required uint SequenceNo { get; init; }

    /// <summary>Column: stop_type; SQL: varchar(24); not null.</summary>
    [Column("stop_type", TypeName = "varchar(24)")]
    public required string StopType { get; init; }

    /// <summary>Column: party_id; SQL: bigint unsigned; nullable.</summary>
    [Column("party_id", TypeName = "bigint unsigned")]
    public ulong? PartyId { get; init; }

    /// <summary>Column: location_id; SQL: bigint unsigned; nullable. Local authorized location; external destinations can use address/position snapshots only</summary>
    [Column("location_id", TypeName = "bigint unsigned")]
    public ulong? LocationId { get; init; }

    /// <summary>Column: address_snapshot_json; SQL: json; not null. Immutable dispatch snapshot of address A/B, including station/access details</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("address_snapshot_json", TypeName = "json")]
    public required string AddressSnapshotJson { get; init; }

    /// <summary>Column: pose_snapshot_json; SQL: json; nullable. Coordinates, altitude datum, attitude, accuracy and source geo-pose public context</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("pose_snapshot_json", TypeName = "json")]
    public string? PoseSnapshotJson { get; init; }

    /// <summary>Column: time_zone_id; SQL: varchar(64); not null.</summary>
    [Column("time_zone_id", TypeName = "varchar(64)")]
    public required string TimeZoneId { get; init; }

    /// <summary>Column: window_start_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("window_start_at_utc", TypeName = "datetime(6)")]
    public DateTime? WindowStartAtUtc { get; init; }

    /// <summary>Column: window_end_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("window_end_at_utc", TypeName = "datetime(6)")]
    public DateTime? WindowEndAtUtc { get; init; }

    /// <summary>Column: arrived_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("arrived_at_utc", TypeName = "datetime(6)")]
    public DateTime? ArrivedAtUtc { get; init; }

    /// <summary>Column: departed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("departed_at_utc", TypeName = "datetime(6)")]
    public DateTime? DepartedAtUtc { get; init; }

    /// <summary>Column: completed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("completed_at_utc", TypeName = "datetime(6)")]
    public DateTime? CompletedAtUtc { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null.</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: instructions; SQL: text; nullable.</summary>
    [Column("instructions", TypeName = "text")]
    public string? Instructions { get; init; }
}
