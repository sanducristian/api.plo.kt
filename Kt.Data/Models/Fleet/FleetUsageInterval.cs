// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Fleet;

/// <summary>Maps one row of the fleet_usage_interval table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("fleet_usage_interval")]
public sealed record FleetUsageInterval
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

    /// <summary>Column: counter_kind; SQL: varchar(24); not null.</summary>
    [Column("counter_kind", TypeName = "varchar(24)")]
    public required string CounterKind { get; init; }

    /// <summary>Column: started_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("started_at_utc", TypeName = "datetime(6)")]
    public required DateTime StartedAtUtc { get; init; }

    /// <summary>Column: ended_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("ended_at_utc", TypeName = "datetime(6)")]
    public required DateTime EndedAtUtc { get; init; }

    /// <summary>Column: duration_s; SQL: decimal(20,6); not null.</summary>
    [Column("duration_s", TypeName = "decimal(20,6)")]
    public required decimal DurationS { get; init; }

    /// <summary>Column: distance_m; SQL: decimal(20,6); nullable.</summary>
    [Column("distance_m", TypeName = "decimal(20,6)")]
    public decimal? DistanceM { get; init; }

    /// <summary>Column: source_event_key; SQL: varchar(128); not null.</summary>
    [Column("source_event_key", TypeName = "varchar(128)")]
    public required string SourceEventKey { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }
}
