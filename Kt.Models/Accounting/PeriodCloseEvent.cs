// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Accounting;

/// <summary>Maps one row of the acc_period_close_event table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("acc_period_close_event")]
public sealed record PeriodCloseEvent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: accounting_period_id; SQL: bigint unsigned; not null.</summary>
    [Column("accounting_period_id", TypeName = "bigint unsigned")]
    public required ulong AccountingPeriodId { get; init; }

    /// <summary>Column: event_type; SQL: varchar(24); not null.</summary>
    [Column("event_type", TypeName = "varchar(24)")]
    public required string EventType { get; init; }

    /// <summary>Column: from_status_code; SQL: varchar(16); nullable.</summary>
    [Column("from_status_code", TypeName = "varchar(16)")]
    public string? FromStatusCode { get; init; }

    /// <summary>Column: to_status_code; SQL: varchar(16); not null.</summary>
    [Column("to_status_code", TypeName = "varchar(16)")]
    public required string ToStatusCode { get; init; }

    /// <summary>Column: reason_text; SQL: varchar(1024); nullable.</summary>
    [Column("reason_text", TypeName = "varchar(1024)")]
    public string? ReasonText { get; init; }

    /// <summary>Column: evidence_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("evidence_json", TypeName = "json")]
    public string? EvidenceJson { get; init; }

    /// <summary>Column: occurred_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_at_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredAtUtc { get; init; }

    /// <summary>Column: actor_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("actor_user_id", TypeName = "bigint unsigned")]
    public required ulong ActorUserId { get; init; }
}
