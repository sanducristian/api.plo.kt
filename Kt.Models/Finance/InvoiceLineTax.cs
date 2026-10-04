// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_line_tax table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_line_tax")]
public sealed record InvoiceLineTax
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_line_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_line_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceLineId { get; init; }

    /// <summary>Column: tax_code_id; SQL: bigint unsigned; nullable.</summary>
    [Column("tax_code_id", TypeName = "bigint unsigned")]
    public ulong? TaxCodeId { get; init; }

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

    /// <summary>Column: exemption_reason_code; SQL: varchar(64); nullable.</summary>
    [Column("exemption_reason_code", TypeName = "varchar(64)")]
    public string? ExemptionReasonCode { get; init; }

    /// <summary>Column: exemption_reason_text; SQL: varchar(512); nullable.</summary>
    [Column("exemption_reason_text", TypeName = "varchar(512)")]
    public string? ExemptionReasonText { get; init; }
}
