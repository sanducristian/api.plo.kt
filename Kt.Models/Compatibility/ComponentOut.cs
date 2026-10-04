// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Compatibility;

/// <summary>Maps one row of the componentout table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("componentout")]
public sealed record ComponentOut
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: documentDate; SQL: date; nullable.</summary>
    [Column("documentDate", TypeName = "date")]
    public DateOnly? DocumentDate { get; init; }

    /// <summary>Column: documentNumber; SQL: varchar(64); nullable.</summary>
    [Column("documentNumber", TypeName = "varchar(64)")]
    public string? DocumentNumber { get; init; }

    /// <summary>Column: invoiceId; SQL: bigint unsigned; nullable.</summary>
    [Column("invoiceId", TypeName = "bigint unsigned")]
    public ulong? InvoiceId { get; init; }
}
