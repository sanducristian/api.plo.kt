// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_line_purchase_order_match table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_line_purchase_order_match")]
public sealed record InvoiceLinePurchaseOrderMatch
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_line_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_line_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceLineId { get; init; }

    /// <summary>Column: purchase_order_line_id; SQL: bigint unsigned; not null.</summary>
    [Column("purchase_order_line_id", TypeName = "bigint unsigned")]
    public required ulong PurchaseOrderLineId { get; init; }

    /// <summary>Column: matched_quantity; SQL: decimal(20,6); nullable. Invoice quantity attributed to this purchase-order line</summary>
    [Column("matched_quantity", TypeName = "decimal(20,6)")]
    public decimal? MatchedQuantity { get; init; }

    /// <summary>Column: matched_net_amount; SQL: decimal(20,6); nullable. Invoice net amount attributed to this purchase-order line in invoice currency</summary>
    [Column("matched_net_amount", TypeName = "decimal(20,6)")]
    public decimal? MatchedNetAmount { get; init; }

    /// <summary>Column: matched_order_net_amount; SQL: decimal(20,6); nullable. Comparable value in purchase-order currency after approved conversion/variance handling</summary>
    [Column("matched_order_net_amount", TypeName = "decimal(20,6)")]
    public decimal? MatchedOrderNetAmount { get; init; }

    /// <summary>Column: match_method; SQL: varchar(24); not null. MANUAL, REFERENCE, RULE, SUGGESTED, or IMPORTED</summary>
    [Column("match_method", TypeName = "varchar(24)")]
    public required string MatchMethod { get; init; }

    /// <summary>Column: exception_code; SQL: varchar(64); nullable. Price, quantity, currency, tax, or reference exception requiring review</summary>
    [Column("exception_code", TypeName = "varchar(64)")]
    public string? ExceptionCode { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null. New Identity-module user ID</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }
}
