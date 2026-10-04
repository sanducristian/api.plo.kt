// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_einvoice_event table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_einvoice_event")]
public sealed record EinvoiceEvent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: einvoice_exchange_id; SQL: bigint unsigned; not null.</summary>
    [Column("einvoice_exchange_id", TypeName = "bigint unsigned")]
    public required ulong EinvoiceExchangeId { get; init; }

    /// <summary>Column: event_type; SQL: varchar(32); not null.</summary>
    [Column("event_type", TypeName = "varchar(32)")]
    public required string EventType { get; init; }

    /// <summary>Column: from_status_code; SQL: varchar(32); nullable.</summary>
    [Column("from_status_code", TypeName = "varchar(32)")]
    public string? FromStatusCode { get; init; }

    /// <summary>Column: to_status_code; SQL: varchar(32); not null.</summary>
    [Column("to_status_code", TypeName = "varchar(32)")]
    public required string ToStatusCode { get; init; }

    /// <summary>Column: external_event_id; SQL: varchar(256); nullable.</summary>
    [Column("external_event_id", TypeName = "varchar(256)")]
    public string? ExternalEventId { get; init; }

    /// <summary>Column: response_code; SQL: varchar(128); nullable.</summary>
    [Column("response_code", TypeName = "varchar(128)")]
    public string? ResponseCode { get; init; }

    /// <summary>Column: message_text; SQL: varchar(2048); nullable.</summary>
    [Column("message_text", TypeName = "varchar(2048)")]
    public string? MessageText { get; init; }

    /// <summary>Column: payload_file_id; SQL: bigint unsigned; nullable.</summary>
    [Column("payload_file_id", TypeName = "bigint unsigned")]
    public ulong? PayloadFileId { get; init; }

    /// <summary>Column: occurred_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_at_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredAtUtc { get; init; }

    /// <summary>Column: recorded_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("recorded_at_utc", TypeName = "datetime(6)")]
    public required DateTime RecordedAtUtc { get; init; }

    /// <summary>Column: actor_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("actor_user_id", TypeName = "bigint unsigned")]
    public ulong? ActorUserId { get; init; }
}
