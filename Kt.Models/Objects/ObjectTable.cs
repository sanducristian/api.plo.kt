// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects;

/// <summary>Maps one row of the obj_table table. Includes all 7 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_table")]
public sealed record ObjectTable
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: table_name; SQL: varchar(196); not null. The table name</summary>
    [Column("table_name", TypeName = "varchar(196)")]
    public required string TableName { get; init; }

    /// <summary>Column: table_index_field; SQL: varchar(64); nullable. The table index field name (ex: ID)</summary>
    [Column("table_index_field", TypeName = "varchar(64)")]
    public string? TableIndexField { get; init; }

    /// <summary>Column: table_index_name; SQL: varchar(64); nullable. The table index name (ex: idxId)</summary>
    [Column("table_index_name", TypeName = "varchar(64)")]
    public string? TableIndexName { get; init; }

    /// <summary>Column: type_id; SQL: bigint unsigned; nullable.</summary>
    [Column("type_id", TypeName = "bigint unsigned")]
    public ulong? TypeId { get; init; }

    /// <summary>Column: ref_nil; SQL: tinyint(1); nullable. If set, when no other references are made to the specified object then it can become nil.</summary>
    [Column("ref_nil", TypeName = "tinyint(1)")]
    public bool? RefNil { get; init; }

    /// <summary>Column: uses_global_object_id; SQL: tinyint(1); not null.</summary>
    [Column("uses_global_object_id", TypeName = "tinyint(1)")]
    public required bool UsesGlobalObjectId { get; init; }
}
