// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_bank_statement_line_invoice_match table. Includes all 14 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_bank_statement_line_invoice_match")]
public sealed record BankStatementLineInvoiceMatch
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: bank_statement_line_id; SQL: bigint unsigned; not null.</summary>
    [Column("bank_statement_line_id", TypeName = "bigint unsigned")]
    public required ulong BankStatementLineId { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceId { get; init; }

    /// <summary>Column: statement_amount; SQL: decimal(20,6); not null. Absolute amount attributed to the invoice in statement-line currency</summary>
    [Column("statement_amount", TypeName = "decimal(20,6)")]
    public required decimal StatementAmount { get; init; }

    /// <summary>Column: invoice_amount; SQL: decimal(20,6); not null. Absolute amount settled in invoice currency</summary>
    [Column("invoice_amount", TypeName = "decimal(20,6)")]
    public required decimal InvoiceAmount { get; init; }

    /// <summary>Column: exchange_rate; SQL: decimal(20,10); nullable. Approved statement-currency to invoice-currency conversion rate when currencies differ</summary>
    [Column("exchange_rate", TypeName = "decimal(20,10)")]
    public decimal? ExchangeRate { get; init; }

    /// <summary>Column: match_status; SQL: varchar(24); not null. SUGGESTED, CONFIRMED, REJECTED, or REVERSED</summary>
    [Column("match_status", TypeName = "varchar(24)")]
    public required string MatchStatus { get; init; }

    /// <summary>Column: match_method; SQL: varchar(24); not null. REFERENCE, AMOUNT_DATE, PARTY_ACCOUNT, RULE, MANUAL, or IMPORTED</summary>
    [Column("match_method", TypeName = "varchar(24)")]
    public required string MatchMethod { get; init; }

    /// <summary>Column: match_score; SQL: decimal(5,4); nullable. Suggestion confidence from 0.0000 to 1.0000</summary>
    [Column("match_score", TypeName = "decimal(5,4)")]
    public decimal? MatchScore { get; init; }

    /// <summary>Column: reason_code; SQL: varchar(64); nullable. Difference, fee, FX, discount, withholding, write-off, refund, or other reconciliation reason</summary>
    [Column("reason_code", TypeName = "varchar(64)")]
    public string? ReasonCode { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; nullable. New Identity-module user ID or reserved system identity</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public ulong? CreatedByUserId { get; init; }

    /// <summary>Column: confirmed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("confirmed_at_utc", TypeName = "datetime(6)")]
    public DateTime? ConfirmedAtUtc { get; init; }

    /// <summary>Column: confirmed_by_user_id; SQL: bigint unsigned; nullable. New Identity-module user ID</summary>
    [Column("confirmed_by_user_id", TypeName = "bigint unsigned")]
    public ulong? ConfirmedByUserId { get; init; }
}
