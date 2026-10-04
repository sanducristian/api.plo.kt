// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_line_match table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_line_match")]
public sealed record InvoiceLineMatch
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_line_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_line_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceLineId { get; init; }

    /// <summary>Column: target_type; SQL: varchar(32); not null.</summary>
    [Column("target_type", TypeName = "varchar(32)")]
    public required string TargetType { get; init; }

    /// <summary>Column: target_id; SQL: bigint unsigned; not null. Cross-module ID resolved through the owning module API</summary>
    [Column("target_id", TypeName = "bigint unsigned")]
    public required ulong TargetId { get; init; }

    /// <summary>Column: matched_quantity; SQL: decimal(20,6); nullable.</summary>
    [Column("matched_quantity", TypeName = "decimal(20,6)")]
    public decimal? MatchedQuantity { get; init; }

    /// <summary>Column: matched_net_amount; SQL: decimal(20,6); nullable.</summary>
    [Column("matched_net_amount", TypeName = "decimal(20,6)")]
    public decimal? MatchedNetAmount { get; init; }

    /// <summary>Column: match_method; SQL: varchar(24); not null.</summary>
    [Column("match_method", TypeName = "varchar(24)")]
    public required string MatchMethod { get; init; }

    /// <summary>Column: match_score; SQL: decimal(5,4); nullable.</summary>
    [Column("match_score", TypeName = "decimal(5,4)")]
    public decimal? MatchScore { get; init; }

    /// <summary>Column: exception_code; SQL: varchar(64); nullable.</summary>
    [Column("exception_code", TypeName = "varchar(64)")]
    public string? ExceptionCode { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }
}
