// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Transport;

/// <summary>Maps one row of the transport_event table. Includes all 15 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("transport_event")]
public sealed record TransportEvent
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

    /// <summary>Column: stop_id; SQL: bigint unsigned; nullable.</summary>
    [Column("stop_id", TypeName = "bigint unsigned")]
    public ulong? StopId { get; init; }

    /// <summary>Column: assignment_id; SQL: bigint unsigned; nullable.</summary>
    [Column("assignment_id", TypeName = "bigint unsigned")]
    public ulong? AssignmentId { get; init; }

    /// <summary>Column: cargo_line_id; SQL: bigint unsigned; nullable.</summary>
    [Column("cargo_line_id", TypeName = "bigint unsigned")]
    public ulong? CargoLineId { get; init; }

    /// <summary>Column: event_type; SQL: varchar(32); not null.</summary>
    [Column("event_type", TypeName = "varchar(32)")]
    public required string EventType { get; init; }

    /// <summary>Column: occurred_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_at_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredAtUtc { get; init; }

    /// <summary>Column: received_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("received_at_utc", TypeName = "datetime(6)")]
    public required DateTime ReceivedAtUtc { get; init; }

    /// <summary>Column: source_event_key; SQL: varchar(128); not null.</summary>
    [Column("source_event_key", TypeName = "varchar(128)")]
    public required string SourceEventKey { get; init; }

    /// <summary>Column: quantity; SQL: decimal(20,6); nullable. Loaded/unloaded quantity in the cargo line unit</summary>
    [Column("quantity", TypeName = "decimal(20,6)")]
    public decimal? Quantity { get; init; }

    /// <summary>Column: evidence_file_id; SQL: bigint unsigned; nullable.</summary>
    [Column("evidence_file_id", TypeName = "bigint unsigned")]
    public ulong? EvidenceFileId { get; init; }

    /// <summary>Column: details_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("details_json", TypeName = "json")]
    public string? DetailsJson { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }
}
