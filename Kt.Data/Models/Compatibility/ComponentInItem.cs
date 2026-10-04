// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Compatibility;

/// <summary>Maps one row of the componentinitem table. Includes all 7 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("componentinitem")]
public sealed record ComponentInItem
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: productId; SQL: bigint unsigned; nullable.</summary>
    [Column("productId", TypeName = "bigint unsigned")]
    public ulong? ProductId { get; init; }

    /// <summary>Column: invoiceItemNo; SQL: varchar(64); nullable.</summary>
    [Column("invoiceItemNo", TypeName = "varchar(64)")]
    public string? InvoiceItemNo { get; init; }

    /// <summary>Column: nir; SQL: varchar(64); nullable.</summary>
    [Column("nir", TypeName = "varchar(64)")]
    public string? Nir { get; init; }

    /// <summary>Column: countryOfOrigin; SQL: bigint unsigned; nullable.</summary>
    [Column("countryOfOrigin", TypeName = "bigint unsigned")]
    public ulong? CountryOfOrigin { get; init; }

    /// <summary>Column: douaneNumber; SQL: varchar(64); nullable.</summary>
    [Column("douaneNumber", TypeName = "varchar(64)")]
    public string? DouaneNumber { get; init; }

    /// <summary>Column: serialNumber; SQL: varchar(64); nullable.</summary>
    [Column("serialNumber", TypeName = "varchar(64)")]
    public string? SerialNumber { get; init; }
}
