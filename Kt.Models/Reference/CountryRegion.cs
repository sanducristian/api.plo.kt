// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Reference;

/// <summary>Maps one row of the sys_country_region table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_country_region")]
public sealed record CountryRegion
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global obj_id identity; allocate via AllocateId()</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null. Application UUID for safe API exposure</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: country_id; SQL: bigint unsigned; not null. Foreign key referencing sys_country.id</summary>
    [Column("country_id", TypeName = "bigint unsigned")]
    public required ulong CountryId { get; init; }

    /// <summary>Column: parent_region_id; SQL: bigint unsigned; nullable. Self-referential parent region (e.g. Department -&gt; Region)</summary>
    [Column("parent_region_id", TypeName = "bigint unsigned")]
    public ulong? ParentRegionId { get; init; }

    /// <summary>Column: subdivision_code; SQL: varchar(32); not null. ISO 3166-2 code (e.g. FR-92, RO-HD, DE-BY)</summary>
    [Column("subdivision_code", TypeName = "varchar(32)")]
    public required string SubdivisionCode { get; init; }

    /// <summary>Column: region_name; SQL: varchar(128); not null. Human-readable region or department display name (e.g., Hauts-de-Seine, Bavaria, Hunedoara)</summary>
    [Column("region_name", TypeName = "varchar(128)")]
    public required string RegionName { get; init; }

    /// <summary>Column: region_number; SQL: varchar(32); nullable. Local administrative or numerical code (e.g., 92 for Hauts-de-Seine, HD for Hunedoara, 75 for Paris)</summary>
    [Column("region_number", TypeName = "varchar(32)")]
    public string? RegionNumber { get; init; }

    /// <summary>Column: subdivision_type; SQL: varchar(64); not null. STATE, REGION, DEPARTMENT, COUNTY, PROVINCE, CANTON, TERRITORY</summary>
    [Column("subdivision_type", TypeName = "varchar(64)")]
    public required string SubdivisionType { get; init; }

    /// <summary>Column: default_name; SQL: varchar(128); not null. Default English or official native reference name</summary>
    [Column("default_name", TypeName = "varchar(128)")]
    public required string DefaultName { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null. Active status flag (1=Active, 0=Disabled)</summary>
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
