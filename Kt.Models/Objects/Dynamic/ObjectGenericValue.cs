// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects.Dynamic;

/// <summary>Maps one row of the obj_dyn_val_generic table. Includes all 2 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_dyn_val_generic")]
public sealed record ObjectDynGenericValue {
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: value; SQL: varchar(128); nullable. The attribute value</summary>
    [Column("value", TypeName = "varchar(128)")]
    public string? Value { get; init; }
}
