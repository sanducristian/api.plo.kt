// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Compatibility;

/// <summary>Maps one row of the conversion table. Includes all 2 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("conversion")]
public sealed record Conversion
{
    /// <summary>Column: oldId; SQL: bigint unsigned; not null.</summary>
    [Column("oldId", TypeName = "bigint unsigned")]
    public required ulong OldId { get; init; }

    /// <summary>Column: newId; SQL: bigint unsigned; not null.</summary>
    [Column("newId", TypeName = "bigint unsigned")]
    public required ulong NewId { get; init; }
}
