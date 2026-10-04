// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_line table. Includes all 24 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_line")]
public sealed record InvoiceLine
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceId { get; init; }

    /// <summary>Column: line_number; SQL: int unsigned; not null.</summary>
    [Column("line_number", TypeName = "int unsigned")]
    public required uint LineNumber { get; init; }

    /// <summary>Column: line_type; SQL: varchar(24); not null.</summary>
    [Column("line_type", TypeName = "varchar(24)")]
    public required string LineType { get; init; }

    /// <summary>Column: product_id; SQL: bigint unsigned; nullable. Stable modern product/component ID; formal FK added with the Product module</summary>
    [Column("product_id", TypeName = "bigint unsigned")]
    public ulong? ProductId { get; init; }

    /// <summary>Column: product_code_snapshot; SQL: varchar(128); nullable.</summary>
    [Column("product_code_snapshot", TypeName = "varchar(128)")]
    public string? ProductCodeSnapshot { get; init; }

    /// <summary>Column: description; SQL: varchar(1024); not null.</summary>
    [Column("description", TypeName = "varchar(1024)")]
    public required string Description { get; init; }

    /// <summary>Column: quantity; SQL: decimal(20,6); not null.</summary>
    [Column("quantity", TypeName = "decimal(20,6)")]
    public required decimal Quantity { get; init; }

    /// <summary>Column: unit_of_measure_code; SQL: varchar(32); nullable.</summary>
    [Column("unit_of_measure_code", TypeName = "varchar(32)")]
    public string? UnitOfMeasureCode { get; init; }

    /// <summary>Column: packaging_configuration_id; SQL: bigint unsigned; nullable. Cross-module reference to the product packaging configuration</summary>
    [Column("packaging_configuration_id", TypeName = "bigint unsigned")]
    public ulong? PackagingConfigurationId { get; init; }

    /// <summary>Column: unit_price; SQL: decimal(20,6); not null.</summary>
    [Column("unit_price", TypeName = "decimal(20,6)")]
    public required decimal UnitPrice { get; init; }

    /// <summary>Column: discount_amount; SQL: decimal(20,6); not null.</summary>
    [Column("discount_amount", TypeName = "decimal(20,6)")]
    public required decimal DiscountAmount { get; init; }

    /// <summary>Column: charge_amount; SQL: decimal(20,6); not null.</summary>
    [Column("charge_amount", TypeName = "decimal(20,6)")]
    public required decimal ChargeAmount { get; init; }

    /// <summary>Column: net_amount; SQL: decimal(20,6); not null.</summary>
    [Column("net_amount", TypeName = "decimal(20,6)")]
    public required decimal NetAmount { get; init; }

    /// <summary>Column: tax_amount; SQL: decimal(20,6); not null.</summary>
    [Column("tax_amount", TypeName = "decimal(20,6)")]
    public required decimal TaxAmount { get; init; }

    /// <summary>Column: gross_amount; SQL: decimal(20,6); not null.</summary>
    [Column("gross_amount", TypeName = "decimal(20,6)")]
    public required decimal GrossAmount { get; init; }

    /// <summary>Column: service_period_start; SQL: date; nullable.</summary>
    [Column("service_period_start", TypeName = "date")]
    public DateOnly? ServicePeriodStart { get; init; }

    /// <summary>Column: service_period_end; SQL: date; nullable.</summary>
    [Column("service_period_end", TypeName = "date")]
    public DateOnly? ServicePeriodEnd { get; init; }

    /// <summary>Column: country_of_origin_code; SQL: char(2); nullable. ISO 3166-1 alpha-2 non-preferential or declared origin for this invoice line</summary>
    [Column("country_of_origin_code", TypeName = "char(2)")]
    public string? CountryOfOriginCode { get; init; }

    /// <summary>Column: tariff_classification_id; SQL: bigint unsigned; nullable. Versioned classification selected for the invoice date/jurisdiction</summary>
    [Column("tariff_classification_id", TypeName = "bigint unsigned")]
    public ulong? TariffClassificationId { get; init; }

    /// <summary>Column: customs_tariff_code_snapshot; SQL: varchar(32); nullable. Tariff code preserved exactly as used on the invoice/customs evidence</summary>
    [Column("customs_tariff_code_snapshot", TypeName = "varchar(32)")]
    public string? CustomsTariffCodeSnapshot { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: updated_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_at_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedAtUtc { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
