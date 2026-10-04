// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_tax_summary table. Includes all 6 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_tax_summary")]
public sealed record InvoiceTaxSummary
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceId { get; init; }

    /// <summary>Column: tax_category_code; SQL: varchar(32); not null.</summary>
    [Column("tax_category_code", TypeName = "varchar(32)")]
    public required string TaxCategoryCode { get; init; }

    /// <summary>Column: rate_percent; SQL: decimal(9,6); not null.</summary>
    [Column("rate_percent", TypeName = "decimal(9,6)")]
    public required decimal RatePercent { get; init; }

    /// <summary>Column: taxable_amount; SQL: decimal(20,6); not null.</summary>
    [Column("taxable_amount", TypeName = "decimal(20,6)")]
    public required decimal TaxableAmount { get; init; }

    /// <summary>Column: tax_amount; SQL: decimal(20,6); not null.</summary>
    [Column("tax_amount", TypeName = "decimal(20,6)")]
    public required decimal TaxAmount { get; init; }
}
