// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_declaration_item_invoice_line table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_declaration_item_invoice_line")]
public sealed record DeclarationItemInvoiceLine
{
    /// <summary>Column: customs_declaration_item_id; SQL: bigint unsigned; not null.</summary>
    [Column("customs_declaration_item_id", TypeName = "bigint unsigned")]
    public required ulong CustomsDeclarationItemId { get; init; }

    /// <summary>Column: invoice_line_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_line_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceLineId { get; init; }

    /// <summary>Column: allocated_quantity; SQL: decimal(20,6); nullable. Invoice-line quantity represented by this customs item</summary>
    [Column("allocated_quantity", TypeName = "decimal(20,6)")]
    public decimal? AllocatedQuantity { get; init; }

    /// <summary>Column: allocated_invoice_value; SQL: decimal(20,6); nullable. Invoice value assigned to this customs item in invoice currency</summary>
    [Column("allocated_invoice_value", TypeName = "decimal(20,6)")]
    public decimal? AllocatedInvoiceValue { get; init; }
}
