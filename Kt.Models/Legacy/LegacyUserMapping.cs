// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Legacy;

/// <summary>Maps one row of the legacy_user_mapping table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("legacy_user_mapping")]
public sealed record LegacyUserMapping
{
    /// <summary>Column: legacy_user_id; SQL: bigint unsigned; not null. Existing plo4.user.id value.</summary>
    [Column("legacy_user_id", TypeName = "bigint unsigned")]
    public required ulong LegacyUserId { get; init; }

    /// <summary>Column: platform_user_id; SQL: bigint unsigned; not null. Modern global user receiving the legacy account history.</summary>
    [Column("platform_user_id", TypeName = "bigint unsigned")]
    public required ulong PlatformUserId { get; init; }

    /// <summary>Column: mapped_utc; SQL: datetime(6); not null. UTC time when the mapping was approved.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("mapped_utc", TypeName = "datetime(6)")]
    public required DateTime MappedUtc { get; init; }

    /// <summary>Column: mapping_note; SQL: varchar(1000); nullable. Reason or migration evidence supporting the mapping.</summary>
    [Column("mapping_note", TypeName = "varchar(1000)")]
    public string? MappingNote { get; init; }
}
