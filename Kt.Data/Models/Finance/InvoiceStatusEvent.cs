// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_status_event table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_status_event")]
public sealed record InvoiceStatusEvent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceId { get; init; }

    /// <summary>Column: status_dimension; SQL: varchar(24); not null.</summary>
    [Column("status_dimension", TypeName = "varchar(24)")]
    public required string StatusDimension { get; init; }

    /// <summary>Column: from_status_code; SQL: varchar(32); nullable.</summary>
    [Column("from_status_code", TypeName = "varchar(32)")]
    public string? FromStatusCode { get; init; }

    /// <summary>Column: to_status_code; SQL: varchar(32); not null.</summary>
    [Column("to_status_code", TypeName = "varchar(32)")]
    public required string ToStatusCode { get; init; }

    /// <summary>Column: reason_code; SQL: varchar(64); nullable.</summary>
    [Column("reason_code", TypeName = "varchar(64)")]
    public string? ReasonCode { get; init; }

    /// <summary>Column: comment; SQL: varchar(1024); nullable.</summary>
    [Column("comment", TypeName = "varchar(1024)")]
    public string? Comment { get; init; }

    /// <summary>Column: occurred_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_at_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredAtUtc { get; init; }

    /// <summary>Column: actor_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("actor_user_id", TypeName = "bigint unsigned")]
    public ulong? ActorUserId { get; init; }

    /// <summary>Column: correlation_id; SQL: binary(16); nullable.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("correlation_id", TypeName = "binary(16)")]
    public byte[]? CorrelationId { get; init; }
}
