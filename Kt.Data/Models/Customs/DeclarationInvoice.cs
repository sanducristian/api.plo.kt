// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_declaration_invoice table. Includes all 3 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_declaration_invoice")]
public sealed record DeclarationInvoice
{
    /// <summary>Column: customs_declaration_id; SQL: bigint unsigned; not null.</summary>
    [Column("customs_declaration_id", TypeName = "bigint unsigned")]
    public required ulong CustomsDeclarationId { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceId { get; init; }

    /// <summary>Column: relation_type; SQL: varchar(24); not null. COMMERCIAL, PRO_FORMA, SUPPORTING, or CORRECTION</summary>
    [Column("relation_type", TypeName = "varchar(24)")]
    public required string RelationType { get; init; }
}
