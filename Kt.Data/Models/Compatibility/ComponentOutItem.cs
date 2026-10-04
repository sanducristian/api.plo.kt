// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Compatibility;

/// <summary>Maps one row of the componentoutitem table. Includes all 2 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("componentoutitem")]
public sealed record ComponentOutItem
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: productInId; SQL: bigint unsigned; nullable.</summary>
    [Column("productInId", TypeName = "bigint unsigned")]
    public ulong? ProductInId { get; init; }
}
