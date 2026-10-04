// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_age_profile table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_age_profile")]
public sealed record PlatformUserAgeProfile
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: platform_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("platform_user_id", TypeName = "bigint unsigned")]
    public required ulong PlatformUserId { get; init; }

    /// <summary>Column: date_of_birth; SQL: date; not null.</summary>
    [Column("date_of_birth", TypeName = "date")]
    public required DateOnly DateOfBirth { get; init; }

    /// <summary>Column: residence_country_iso2; SQL: char(2); not null.</summary>
    [Column("residence_country_iso2", TypeName = "char(2)")]
    public required string ResidenceCountryIso2 { get; init; }

    /// <summary>Column: info_source; SQL: varchar(64); not null.</summary>
    [Column("info_source", TypeName = "varchar(64)")]
    public required string InfoSource { get; init; }

    /// <summary>Column: verification_level; SQL: varchar(32); not null.</summary>
    [Column("verification_level", TypeName = "varchar(32)")]
    public required string VerificationLevel { get; init; }

    /// <summary>Column: review_state; SQL: varchar(32); not null.</summary>
    [Column("review_state", TypeName = "varchar(32)")]
    public required string ReviewState { get; init; }

    /// <summary>Column: effective_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("effective_utc", TypeName = "datetime(6)")]
    public required DateTime EffectiveUtc { get; init; }

    /// <summary>Column: last_evaluated_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("last_evaluated_utc", TypeName = "datetime(6)")]
    public DateTime? LastEvaluatedUtc { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
