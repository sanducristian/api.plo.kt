// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Reference;

/// <summary>Maps one row of the sys_country table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_country")]
public sealed record Country
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global obj_id identity; allocate via AllocateId()</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null. Application UUID stored as binary for safe API exposure</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: iso_alpha2; SQL: char(2); not null. ISO 3166-1 alpha-2 code (e.g. DE, FR, RO, AE)</summary>
    [Column("iso_alpha2", TypeName = "char(2)")]
    public required string IsoAlpha2 { get; init; }

    /// <summary>Column: iso_alpha3; SQL: char(3); not null. ISO 3166-1 alpha-3 code (e.g. DEU, FRA, ROU, ARE)</summary>
    [Column("iso_alpha3", TypeName = "char(3)")]
    public required string IsoAlpha3 { get; init; }

    /// <summary>Column: iso_numeric; SQL: char(3); not null. ISO 3166-1 numeric code (e.g. 276, 250, 642, 784)</summary>
    [Column("iso_numeric", TypeName = "char(3)")]
    public required string IsoNumeric { get; init; }

    /// <summary>Column: default_name; SQL: varchar(128); not null. Default English or standard reference name</summary>
    [Column("default_name", TypeName = "varchar(128)")]
    public required string DefaultName { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null. Soft-disable status flag (1=Active, 0=Disabled)</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. Immutable microsecond UTC creation timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null. Microsecond UTC update timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
