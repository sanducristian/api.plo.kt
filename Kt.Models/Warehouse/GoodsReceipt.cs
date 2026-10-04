// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Warehouse;

/// <summary>Maps one row of the wh_goods_receipt table. Includes all 17 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("wh_goods_receipt")]
public sealed record GoodsReceipt
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID exposed by APIs</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: accounting_legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("accounting_legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong AccountingLegalEntityId { get; init; }

    /// <summary>Column: supplier_party_id; SQL: bigint unsigned; not null.</summary>
    [Column("supplier_party_id", TypeName = "bigint unsigned")]
    public required ulong SupplierPartyId { get; init; }

    /// <summary>Column: receipt_number; SQL: varchar(64); not null. PLO-controlled goods-receipt/NIR number</summary>
    [Column("receipt_number", TypeName = "varchar(64)")]
    public required string ReceiptNumber { get; init; }

    /// <summary>Column: supplier_delivery_note_number; SQL: varchar(128); nullable.</summary>
    [Column("supplier_delivery_note_number", TypeName = "varchar(128)")]
    public string? SupplierDeliveryNoteNumber { get; init; }

    /// <summary>Column: supplier_delivery_note_date; SQL: date; nullable.</summary>
    [Column("supplier_delivery_note_date", TypeName = "date")]
    public DateOnly? SupplierDeliveryNoteDate { get; init; }

    /// <summary>Column: received_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("received_at_utc", TypeName = "datetime(6)")]
    public required DateTime ReceivedAtUtc { get; init; }

    /// <summary>Column: site_id; SQL: bigint unsigned; nullable. Cross-module operating site ID; formal FK follows the site migration</summary>
    [Column("site_id", TypeName = "bigint unsigned")]
    public ulong? SiteId { get; init; }

    /// <summary>Column: warehouse_id; SQL: bigint unsigned; nullable. Cross-module warehouse ID; formal FK follows the warehouse migration</summary>
    [Column("warehouse_id", TypeName = "bigint unsigned")]
    public ulong? WarehouseId { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null. DRAFT, POSTED, REVERSED, or CANCELLED</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: notes; SQL: text; nullable.</summary>
    [Column("notes", TypeName = "text")]
    public string? Notes { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null. New Identity-module user ID</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }

    /// <summary>Column: posted_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("posted_at_utc", TypeName = "datetime(6)")]
    public DateTime? PostedAtUtc { get; init; }

    /// <summary>Column: posted_by_user_id; SQL: bigint unsigned; nullable. New Identity-module user ID</summary>
    [Column("posted_by_user_id", TypeName = "bigint unsigned")]
    public ulong? PostedByUserId { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
