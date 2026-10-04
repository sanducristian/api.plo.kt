// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Procurement;

/// <summary>Maps one row of the proc_purchase_order table. Includes all 22 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("proc_purchase_order")]
public sealed record PurchaseOrder
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID exposed by APIs</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: accounting_legal_entity_id; SQL: bigint unsigned; not null. PLO entity buying the goods/services and owning the payable</summary>
    [Column("accounting_legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong AccountingLegalEntityId { get; init; }

    /// <summary>Column: buyer_party_id; SQL: bigint unsigned; not null. Party placing the order; normally the legal entity party</summary>
    [Column("buyer_party_id", TypeName = "bigint unsigned")]
    public required ulong BuyerPartyId { get; init; }

    /// <summary>Column: supplier_party_id; SQL: bigint unsigned; not null. Party expected to supply the goods/services</summary>
    [Column("supplier_party_id", TypeName = "bigint unsigned")]
    public required ulong SupplierPartyId { get; init; }

    /// <summary>Column: invoice_recipient_party_id; SQL: bigint unsigned; not null. Party/address to which the supplier must issue the invoice</summary>
    [Column("invoice_recipient_party_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceRecipientPartyId { get; init; }

    /// <summary>Column: order_number; SQL: varchar(64); not null. PLO-controlled purchase-order number</summary>
    [Column("order_number", TypeName = "varchar(64)")]
    public required string OrderNumber { get; init; }

    /// <summary>Column: supplier_reference; SQL: varchar(128); nullable. Supplier quotation, acknowledgement, contract, or portal reference</summary>
    [Column("supplier_reference", TypeName = "varchar(128)")]
    public string? SupplierReference { get; init; }

    /// <summary>Column: order_date; SQL: date; not null.</summary>
    [Column("order_date", TypeName = "date")]
    public required DateOnly OrderDate { get; init; }

    /// <summary>Column: expected_delivery_date; SQL: date; nullable.</summary>
    [Column("expected_delivery_date", TypeName = "date")]
    public DateOnly? ExpectedDeliveryDate { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; not null.</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public required ulong CurrencyId { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null. DRAFT, APPROVAL_PENDING, APPROVED, SENT, PARTIALLY_RECEIVED, RECEIVED, CLOSED, CANCELLED</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: incoterm_code; SQL: varchar(8); nullable. Incoterms rule code when applicable, such as EXW, FCA, DAP, or DDP</summary>
    [Column("incoterm_code", TypeName = "varchar(8)")]
    public string? IncotermCode { get; init; }

    /// <summary>Column: incoterm_location; SQL: varchar(256); nullable. Named place/location associated with the Incoterms rule</summary>
    [Column("incoterm_location", TypeName = "varchar(256)")]
    public string? IncotermLocation { get; init; }

    /// <summary>Column: subtotal_amount; SQL: decimal(20,6); not null.</summary>
    [Column("subtotal_amount", TypeName = "decimal(20,6)")]
    public required decimal SubtotalAmount { get; init; }

    /// <summary>Column: tax_amount; SQL: decimal(20,6); not null.</summary>
    [Column("tax_amount", TypeName = "decimal(20,6)")]
    public required decimal TaxAmount { get; init; }

    /// <summary>Column: total_amount; SQL: decimal(20,6); not null.</summary>
    [Column("total_amount", TypeName = "decimal(20,6)")]
    public required decimal TotalAmount { get; init; }

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

    /// <summary>Column: updated_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_at_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedAtUtc { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
