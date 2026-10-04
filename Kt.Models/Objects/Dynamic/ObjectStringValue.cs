// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects.Dynamic;

/// <summary>Maps one row of the obj_dyn_val_string table. Includes all 3 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_dyn_val_string")]
public sealed record ObjectDynStringValue {
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: languageId; SQL: bigint unsigned; not null. The language ID for the current string</summary>
    [Column("languageId", TypeName = "bigint unsigned")]
    public required ulong LanguageId { get; init; }

    /// <summary>Column: data; SQL: varchar(128); nullable. The data for the string in question</summary>
    [Column("data", TypeName = "varchar(128)")]
    public string? Data { get; init; }
}
