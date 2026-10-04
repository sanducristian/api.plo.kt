// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Manufacturing;

/// <summary>Maps one row of the mfg_work_order_event table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("mfg_work_order_event")]
public sealed record WorkOrderEvent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: work_order_id; SQL: bigint unsigned; not null.</summary>
    [Column("work_order_id", TypeName = "bigint unsigned")]
    public required ulong WorkOrderId { get; init; }

    /// <summary>Column: event_type; SQL: varchar(32); not null.</summary>
    [Column("event_type", TypeName = "varchar(32)")]
    public required string EventType { get; init; }

    /// <summary>Column: from_status_code; SQL: varchar(24); nullable.</summary>
    [Column("from_status_code", TypeName = "varchar(24)")]
    public string? FromStatusCode { get; init; }

    /// <summary>Column: to_status_code; SQL: varchar(24); nullable.</summary>
    [Column("to_status_code", TypeName = "varchar(24)")]
    public string? ToStatusCode { get; init; }

    /// <summary>Column: event_payload_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("event_payload_json", TypeName = "json")]
    public string? EventPayloadJson { get; init; }

    /// <summary>Column: occurred_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_at_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredAtUtc { get; init; }

    /// <summary>Column: recorded_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("recorded_at_utc", TypeName = "datetime(6)")]
    public required DateTime RecordedAtUtc { get; init; }

    /// <summary>Column: actor_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("actor_principal_id", TypeName = "bigint unsigned")]
    public ulong? ActorPrincipalId { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }
}
