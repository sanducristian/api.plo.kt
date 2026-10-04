// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Warehouse;

/// <summary>Maps one row of the wh_goods_receipt_line table. Includes all 16 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("wh_goods_receipt_line")]
public sealed record GoodsReceiptLine
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: goods_receipt_id; SQL: bigint unsigned; not null.</summary>
    [Column("goods_receipt_id", TypeName = "bigint unsigned")]
    public required ulong GoodsReceiptId { get; init; }

    /// <summary>Column: line_number; SQL: int unsigned; not null.</summary>
    [Column("line_number", TypeName = "int unsigned")]
    public required uint LineNumber { get; init; }

    /// <summary>Column: product_id; SQL: bigint unsigned; not null. Cross-module product/component ID</summary>
    [Column("product_id", TypeName = "bigint unsigned")]
    public required ulong ProductId { get; init; }

    /// <summary>Column: product_code_snapshot; SQL: varchar(128); nullable.</summary>
    [Column("product_code_snapshot", TypeName = "varchar(128)")]
    public string? ProductCodeSnapshot { get; init; }

    /// <summary>Column: description; SQL: varchar(1024); not null.</summary>
    [Column("description", TypeName = "varchar(1024)")]
    public required string Description { get; init; }

    /// <summary>Column: received_quantity; SQL: decimal(20,6); not null. Physical quantity counted at receipt before quality disposition</summary>
    [Column("received_quantity", TypeName = "decimal(20,6)")]
    public required decimal ReceivedQuantity { get; init; }

    /// <summary>Column: accepted_quantity; SQL: decimal(20,6); not null. Quantity accepted into usable stock</summary>
    [Column("accepted_quantity", TypeName = "decimal(20,6)")]
    public required decimal AcceptedQuantity { get; init; }

    /// <summary>Column: rejected_quantity; SQL: decimal(20,6); not null. Quantity rejected and expected to be returned/scrapped</summary>
    [Column("rejected_quantity", TypeName = "decimal(20,6)")]
    public required decimal RejectedQuantity { get; init; }

    /// <summary>Column: quarantined_quantity; SQL: decimal(20,6); not null. Quantity held pending inspection or disposition</summary>
    [Column("quarantined_quantity", TypeName = "decimal(20,6)")]
    public required decimal QuarantinedQuantity { get; init; }

    /// <summary>Column: unit_of_measure_code; SQL: varchar(32); not null.</summary>
    [Column("unit_of_measure_code", TypeName = "varchar(32)")]
    public required string UnitOfMeasureCode { get; init; }

    /// <summary>Column: packaging_configuration_id; SQL: bigint unsigned; nullable. Packaging configuration physically received</summary>
    [Column("packaging_configuration_id", TypeName = "bigint unsigned")]
    public ulong? PackagingConfigurationId { get; init; }

    /// <summary>Column: country_of_origin_code; SQL: char(2); nullable.</summary>
    [Column("country_of_origin_code", TypeName = "char(2)")]
    public string? CountryOfOriginCode { get; init; }

    /// <summary>Column: supplier_lot_number; SQL: varchar(128); nullable. Supplier/manufacturer batch reference; not an internal tracked-item identity</summary>
    [Column("supplier_lot_number", TypeName = "varchar(128)")]
    public string? SupplierLotNumber { get; init; }

    /// <summary>Column: warehouse_location_id; SQL: bigint unsigned; nullable. Cross-module receiving/bin location ID</summary>
    [Column("warehouse_location_id", TypeName = "bigint unsigned")]
    public ulong? WarehouseLocationId { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null. RECEIVED, INSPECTION, ACCEPTED, PARTIALLY_ACCEPTED, REJECTED, or REVERSED</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }
}
