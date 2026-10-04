// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user")]
public sealed record PlatformUser
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Principal ID for this human user; shared with security_principal.id.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: person_id; SQL: bigint; nullable. Optional link to the legacy person row while person data is migrated.</summary>
    [Column("person_id", TypeName = "bigint")]
    public long? PersonId { get; init; }

    /// <summary>Column: status; SQL: varchar(24); not null. PENDING, ACTIVE, SUSPENDED or CLOSED.</summary>
    [Column("status", TypeName = "varchar(24)")]
    public required string Status { get; init; }

    /// <summary>Column: display_name; SQL: varchar(255); not null. Preferred user-facing name; authentication never relies on this value.</summary>
    [Column("display_name", TypeName = "varchar(255)")]
    public required string DisplayName { get; init; }

    /// <summary>Column: preferred_locale; SQL: varchar(16); nullable. Optional BCP 47 locale such as en-GB, fr-FR or ro-RO.</summary>
    [Column("preferred_locale", TypeName = "varchar(16)")]
    public string? PreferredLocale { get; init; }

    /// <summary>Column: time_zone; SQL: varchar(64); nullable. Optional IANA time-zone identifier used for display, never for storing timestamps.</summary>
    [Column("time_zone", TypeName = "varchar(64)")]
    public string? TimeZone { get; init; }

    /// <summary>Column: email_requirement_met_utc; SQL: datetime(6); nullable. UTC time when the mandatory PLO email-verification requirement was last satisfied.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("email_requirement_met_utc", TypeName = "datetime(6)")]
    public DateTime? EmailRequirementMetUtc { get; init; }

    /// <summary>Column: phone_requirement_met_utc; SQL: datetime(6); nullable. UTC time when the mandatory PLO phone-verification requirement was last satisfied.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("phone_requirement_met_utc", TypeName = "datetime(6)")]
    public DateTime? PhoneRequirementMetUtc { get; init; }

    /// <summary>Column: mfa_requirement_met_utc; SQL: datetime(6); nullable. UTC time when the mandatory MFA-enrollment requirement was last satisfied.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("mfa_requirement_met_utc", TypeName = "datetime(6)")]
    public DateTime? MfaRequirementMetUtc { get; init; }

    /// <summary>Column: activated_utc; SQL: datetime(6); nullable. UTC time when access to business modules was activated.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("activated_utc", TypeName = "datetime(6)")]
    public DateTime? ActivatedUtc { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. UTC creation date and time.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null. UTC time of the latest profile or status update.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
