// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Compatibility;

/// <summary>Maps one row of the useraccess table. Includes all 5 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("useraccess")]
public sealed record UserAccess
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: who; SQL: bigint unsigned; not null.</summary>
    [Column("who", TypeName = "bigint unsigned")]
    public required ulong Who { get; init; }

    /// <summary>Column: what; SQL: bigint unsigned; not null.</summary>
    [Column("what", TypeName = "bigint unsigned")]
    public required ulong What { get; init; }

    /// <summary>Column: where; SQL: bigint unsigned; not null.</summary>
    [Column("where", TypeName = "bigint unsigned")]
    public required ulong Where { get; init; }

    /// <summary>Column: typeId; SQL: bigint unsigned; not null. The type id for the access: users.access.allow users.access.deny users.access.notspecified</summary>
    [Column("typeId", TypeName = "bigint unsigned")]
    public required ulong TypeId { get; init; }
}
