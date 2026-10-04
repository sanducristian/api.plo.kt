// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_email table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_email")]
public sealed record PlatformUserEmail
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: platform_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("platform_user_id", TypeName = "bigint unsigned")]
    public required ulong PlatformUserId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; nullable. Optional company or household context; NULL means a personal/global address.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public ulong? WorkspaceId { get; init; }

    /// <summary>Column: email_address; SQL: varchar(320); not null. Address used for delivery; preserve user-approved casing for display.</summary>
    [Column("email_address", TypeName = "varchar(320)")]
    public required string EmailAddress { get; init; }

    /// <summary>Column: normalized_email; SQL: varchar(320); not null. Canonical value used for comparisons; normalization is performed by the Identity Service.</summary>
    [Column("normalized_email", TypeName = "varchar(320)")]
    public required string NormalizedEmail { get; init; }

    /// <summary>Column: email_type; SQL: varchar(24); not null. PERSONAL, WORK or RECOVERY.</summary>
    [Column("email_type", TypeName = "varchar(24)")]
    public required string EmailType { get; init; }

    /// <summary>Column: is_primary; SQL: tinyint(1); not null. Preferred address in its context; the application enforces one primary address per context.</summary>
    [Column("is_primary", TypeName = "tinyint(1)")]
    public required bool IsPrimary { get; init; }

    /// <summary>Column: verified_utc; SQL: datetime(6); nullable. UTC time of independent PLO verification, even when the provider also verified the address.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("verified_utc", TypeName = "datetime(6)")]
    public DateTime? VerifiedUtc { get; init; }

    /// <summary>Column: disabled_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("disabled_utc", TypeName = "datetime(6)")]
    public DateTime? DisabledUtc { get; init; }

    /// <summary>Column: identity_contact_reference; SQL: varchar(255); nullable. Opaque reference to the corresponding contact in the Identity Service; never a secret.</summary>
    [Column("identity_contact_reference", TypeName = "varchar(255)")]
    public string? IdentityContactReference { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }
}
