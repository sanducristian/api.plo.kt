// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Warehouse;

/// <summary>Maps one row of the wh_goods_receipt_line_order_allocation table. Includes all 7 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("wh_goods_receipt_line_order_allocation")]
public sealed record GoodsReceiptLineOrderAllocation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: goods_receipt_line_id; SQL: bigint unsigned; not null.</summary>
    [Column("goods_receipt_line_id", TypeName = "bigint unsigned")]
    public required ulong GoodsReceiptLineId { get; init; }

    /// <summary>Column: purchase_order_line_id; SQL: bigint unsigned; not null.</summary>
    [Column("purchase_order_line_id", TypeName = "bigint unsigned")]
    public required ulong PurchaseOrderLineId { get; init; }

    /// <summary>Column: allocated_received_quantity; SQL: decimal(20,6); not null. Portion of the receipt line fulfilling this order line</summary>
    [Column("allocated_received_quantity", TypeName = "decimal(20,6)")]
    public required decimal AllocatedReceivedQuantity { get; init; }

    /// <summary>Column: allocated_order_net_amount; SQL: decimal(20,6); nullable. Value of the received portion in purchase-order currency; retained so discounts/charges and later price changes remain reproducible</summary>
    [Column("allocated_order_net_amount", TypeName = "decimal(20,6)")]
    public decimal? AllocatedOrderNetAmount { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null. New Identity-module user ID</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }
}
