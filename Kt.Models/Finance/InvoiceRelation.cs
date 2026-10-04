// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_relation table. Includes all 6 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_relation")]
public sealed record InvoiceRelation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: source_invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("source_invoice_id", TypeName = "bigint unsigned")]
    public required ulong SourceInvoiceId { get; init; }

    /// <summary>Column: related_invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("related_invoice_id", TypeName = "bigint unsigned")]
    public required ulong RelatedInvoiceId { get; init; }

    /// <summary>Column: relation_type; SQL: varchar(24); not null.</summary>
    [Column("relation_type", TypeName = "varchar(24)")]
    public required string RelationType { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }
}
