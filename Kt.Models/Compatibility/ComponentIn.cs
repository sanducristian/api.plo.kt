// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Compatibility;

/// <summary>Maps one row of the componentin table. Includes all 5 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("componentin")]
public sealed record ComponentIn
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: orderId; SQL: bigint unsigned; nullable.</summary>
    [Column("orderId", TypeName = "bigint unsigned")]
    public ulong? OrderId { get; init; }

    /// <summary>Column: documentNumber; SQL: varchar(128); nullable.</summary>
    [Column("documentNumber", TypeName = "varchar(128)")]
    public string? DocumentNumber { get; init; }

    /// <summary>Column: documentDate; SQL: date; nullable.</summary>
    [Column("documentDate", TypeName = "date")]
    public DateOnly? DocumentDate { get; init; }

    /// <summary>Column: supplierId; SQL: bigint unsigned; nullable.</summary>
    [Column("supplierId", TypeName = "bigint unsigned")]
    public ulong? SupplierId { get; init; }
}
