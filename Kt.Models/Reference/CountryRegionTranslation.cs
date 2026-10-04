// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Reference;

/// <summary>Maps one row of the sys_country_region_translation table. Includes all 6 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_country_region_translation")]
public sealed record CountryRegionTranslation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global obj_id identity; allocate via AllocateId()</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: region_id; SQL: bigint unsigned; not null. Foreign key referencing sys_country_region.id</summary>
    [Column("region_id", TypeName = "bigint unsigned")]
    public required ulong RegionId { get; init; }

    /// <summary>Column: language_id; SQL: bigint unsigned; not null. Foreign key referencing sys_language.id</summary>
    [Column("language_id", TypeName = "bigint unsigned")]
    public required ulong LanguageId { get; init; }

    /// <summary>Column: region_name; SQL: varchar(128); not null. Localized region display name</summary>
    [Column("region_name", TypeName = "varchar(128)")]
    public required string RegionName { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. Immutable microsecond UTC creation timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null. Microsecond UTC update timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
