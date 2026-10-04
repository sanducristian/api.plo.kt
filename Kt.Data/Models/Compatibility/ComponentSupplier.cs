// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Compatibility;

/// <summary>Maps one row of the componentsupplier table. Includes all 3 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("componentsupplier")]
public sealed record ComponentSupplier
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: supplierId; SQL: bigint unsigned; nullable.</summary>
    [Column("supplierId", TypeName = "bigint unsigned")]
    public ulong? SupplierId { get; init; }

    /// <summary>Column: referenceNumber; SQL: varchar(128); nullable.</summary>
    [Column("referenceNumber", TypeName = "varchar(128)")]
    public string? ReferenceNumber { get; init; }
}
