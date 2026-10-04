// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Legacy;

/// <summary>Maps one row of the legacy_invoice table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("legacy_invoice")]
public sealed record LegacyInvoice
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: companyId; SQL: bigint unsigned; not null.</summary>
    [Column("companyId", TypeName = "bigint unsigned")]
    public required ulong CompanyId { get; init; }

    /// <summary>Column: invoiceNo; SQL: varchar(64); nullable.</summary>
    [Column("invoiceNo", TypeName = "varchar(64)")]
    public string? InvoiceNo { get; init; }

    /// <summary>Column: invoiceDate; SQL: date; nullable.</summary>
    [Column("invoiceDate", TypeName = "date")]
    public DateOnly? InvoiceDate { get; init; }

    /// <summary>Column: recordDate; SQL: date; nullable.</summary>
    [Column("recordDate", TypeName = "date")]
    public DateOnly? RecordDate { get; init; }

    /// <summary>Column: TVA; SQL: double; nullable.</summary>
    [Column("TVA", TypeName = "double")]
    public double? TVA { get; init; }

    /// <summary>Column: TotalWritten; SQL: double; nullable.</summary>
    [Column("TotalWritten", TypeName = "double")]
    public double? TotalWritten { get; init; }

    /// <summary>Column: Currency; SQL: bigint unsigned; nullable.</summary>
    [Column("Currency", TypeName = "bigint unsigned")]
    public ulong? Currency { get; init; }
}
