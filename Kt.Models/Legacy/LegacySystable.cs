// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Legacy;

/// <summary>Maps one row of the legacy_systable table. Includes all 6 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("legacy_systable")]
public sealed record LegacySystable
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: tableName; SQL: varchar(128); not null. The table name</summary>
    [Column("tableName", TypeName = "varchar(128)")]
    public required string TableName { get; init; }

    /// <summary>Column: tableIndexField; SQL: varchar(64); nullable. The table index field name (ex: ID)</summary>
    [Column("tableIndexField", TypeName = "varchar(64)")]
    public string? TableIndexField { get; init; }

    /// <summary>Column: tableIndexName; SQL: varchar(64); nullable. The table index name (ex: idxId)</summary>
    [Column("tableIndexName", TypeName = "varchar(64)")]
    public string? TableIndexName { get; init; }

    /// <summary>Column: typeId; SQL: bigint unsigned; nullable.</summary>
    [Column("typeId", TypeName = "bigint unsigned")]
    public ulong? TypeId { get; init; }

    /// <summary>Column: refNil; SQL: tinyint(1); nullable. If set, when no other references are made to the specified object then it can become nil.</summary>
    [Column("refNil", TypeName = "tinyint(1)")]
    public bool? RefNil { get; init; }
}
