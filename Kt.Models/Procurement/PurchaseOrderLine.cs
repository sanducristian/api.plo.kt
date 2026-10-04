// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Procurement;

/// <summary>Maps one row of the proc_purchase_order_line table. Includes all 20 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("proc_purchase_order_line")]
public sealed record PurchaseOrderLine
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: purchase_order_id; SQL: bigint unsigned; not null.</summary>
    [Column("purchase_order_id", TypeName = "bigint unsigned")]
    public required ulong PurchaseOrderId { get; init; }

    /// <summary>Column: line_number; SQL: int unsigned; not null.</summary>
    [Column("line_number", TypeName = "int unsigned")]
    public required uint LineNumber { get; init; }

    /// <summary>Column: line_type; SQL: varchar(24); not null. ITEM, SERVICE, SHIPPING, CHARGE, DISCOUNT, or COMMENT</summary>
    [Column("line_type", TypeName = "varchar(24)")]
    public required string LineType { get; init; }

    /// <summary>Column: product_id; SQL: bigint unsigned; nullable. Cross-module product/component ID; NULL is allowed for free-text services or charges</summary>
    [Column("product_id", TypeName = "bigint unsigned")]
    public ulong? ProductId { get; init; }

    /// <summary>Column: product_code_snapshot; SQL: varchar(128); nullable.</summary>
    [Column("product_code_snapshot", TypeName = "varchar(128)")]
    public string? ProductCodeSnapshot { get; init; }

    /// <summary>Column: description; SQL: varchar(1024); not null.</summary>
    [Column("description", TypeName = "varchar(1024)")]
    public required string Description { get; init; }

    /// <summary>Column: ordered_quantity; SQL: decimal(20,6); not null. Commercial quantity originally approved on the order line</summary>
    [Column("ordered_quantity", TypeName = "decimal(20,6)")]
    public required decimal OrderedQuantity { get; init; }

    /// <summary>Column: cancelled_quantity; SQL: decimal(20,6); not null. Quantity formally cancelled without deleting or reducing the original order</summary>
    [Column("cancelled_quantity", TypeName = "decimal(20,6)")]
    public required decimal CancelledQuantity { get; init; }

    /// <summary>Column: unit_of_measure_code; SQL: varchar(32); nullable.</summary>
    [Column("unit_of_measure_code", TypeName = "varchar(32)")]
    public string? UnitOfMeasureCode { get; init; }

    /// <summary>Column: packaging_configuration_id; SQL: bigint unsigned; nullable. Stable Product/Packaging configuration selected for the order</summary>
    [Column("packaging_configuration_id", TypeName = "bigint unsigned")]
    public ulong? PackagingConfigurationId { get; init; }

    /// <summary>Column: unit_price; SQL: decimal(20,6); not null.</summary>
    [Column("unit_price", TypeName = "decimal(20,6)")]
    public required decimal UnitPrice { get; init; }

    /// <summary>Column: discount_amount; SQL: decimal(20,6); not null.</summary>
    [Column("discount_amount", TypeName = "decimal(20,6)")]
    public required decimal DiscountAmount { get; init; }

    /// <summary>Column: net_amount; SQL: decimal(20,6); not null.</summary>
    [Column("net_amount", TypeName = "decimal(20,6)")]
    public required decimal NetAmount { get; init; }

    /// <summary>Column: tax_amount; SQL: decimal(20,6); not null.</summary>
    [Column("tax_amount", TypeName = "decimal(20,6)")]
    public required decimal TaxAmount { get; init; }

    /// <summary>Column: gross_amount; SQL: decimal(20,6); not null.</summary>
    [Column("gross_amount", TypeName = "decimal(20,6)")]
    public required decimal GrossAmount { get; init; }

    /// <summary>Column: expected_delivery_date; SQL: date; nullable.</summary>
    [Column("expected_delivery_date", TypeName = "date")]
    public DateOnly? ExpectedDeliveryDate { get; init; }

    /// <summary>Column: quantity_tolerance_percent; SQL: decimal(9,6); not null. Allowed over/under receipt or invoice quantity variance before exception</summary>
    [Column("quantity_tolerance_percent", TypeName = "decimal(9,6)")]
    public required decimal QuantityTolerancePercent { get; init; }

    /// <summary>Column: price_tolerance_percent; SQL: decimal(9,6); not null. Allowed invoiced unit-price variance before exception</summary>
    [Column("price_tolerance_percent", TypeName = "decimal(9,6)")]
    public required decimal PriceTolerancePercent { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null. OPEN, PARTIALLY_RECEIVED, RECEIVED, CLOSED, or CANCELLED</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }
}
