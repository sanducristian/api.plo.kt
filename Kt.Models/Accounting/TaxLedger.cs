// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Accounting;

/// <summary>Maps one row of the acc_tax_ledger table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("acc_tax_ledger")]
public sealed record TaxLedger
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: tax_period_id; SQL: bigint unsigned; nullable.</summary>
    [Column("tax_period_id", TypeName = "bigint unsigned")]
    public ulong? TaxPeriodId { get; init; }

    /// <summary>Column: journal_entry_line_id; SQL: bigint unsigned; not null.</summary>
    [Column("journal_entry_line_id", TypeName = "bigint unsigned")]
    public required ulong JournalEntryLineId { get; init; }

    /// <summary>Column: tax_code_id; SQL: bigint unsigned; not null.</summary>
    [Column("tax_code_id", TypeName = "bigint unsigned")]
    public required ulong TaxCodeId { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; nullable.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public ulong? InvoiceId { get; init; }

    /// <summary>Column: invoice_line_tax_id; SQL: bigint unsigned; nullable.</summary>
    [Column("invoice_line_tax_id", TypeName = "bigint unsigned")]
    public ulong? InvoiceLineTaxId { get; init; }

    /// <summary>Column: tax_point_date; SQL: date; not null.</summary>
    [Column("tax_point_date", TypeName = "date")]
    public required DateOnly TaxPointDate { get; init; }

    /// <summary>Column: taxable_amount_book; SQL: decimal(20,6); not null.</summary>
    [Column("taxable_amount_book", TypeName = "decimal(20,6)")]
    public required decimal TaxableAmountBook { get; init; }

    /// <summary>Column: tax_amount_book; SQL: decimal(20,6); not null.</summary>
    [Column("tax_amount_book", TypeName = "decimal(20,6)")]
    public required decimal TaxAmountBook { get; init; }

    /// <summary>Column: recoverable_amount_book; SQL: decimal(20,6); not null.</summary>
    [Column("recoverable_amount_book", TypeName = "decimal(20,6)")]
    public required decimal RecoverableAmountBook { get; init; }

    /// <summary>Column: reporting_box_code; SQL: varchar(32); nullable.</summary>
    [Column("reporting_box_code", TypeName = "varchar(32)")]
    public string? ReportingBoxCode { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }
}
