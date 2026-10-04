// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Compatibility;

/// <summary>Maps one row of the componentclass table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("componentclass")]
public sealed record ComponentClass
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: parentId; SQL: bigint unsigned; not null. The parent class. 0 if top class</summary>
    [Column("parentId", TypeName = "bigint unsigned")]
    public required ulong ParentId { get; init; }

    /// <summary>Column: resaleActive; SQL: tinyint(1); not null. Set if the class can be used for resale</summary>
    [Column("resaleActive", TypeName = "tinyint(1)")]
    public required bool ResaleActive { get; init; }

    /// <summary>Column: umId; SQL: bigint; not null. The default quantity for the class</summary>
    [Column("umId", TypeName = "bigint")]
    public required long UmId { get; init; }
}
