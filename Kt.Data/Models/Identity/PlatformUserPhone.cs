// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_phone table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_phone")]
public sealed record PlatformUserPhone
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: platform_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("platform_user_id", TypeName = "bigint unsigned")]
    public required ulong PlatformUserId { get; init; }

    /// <summary>Column: phone_number_id; SQL: bigint unsigned; not null.</summary>
    [Column("phone_number_id", TypeName = "bigint unsigned")]
    public required ulong PhoneNumberId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; nullable. Optional company or household context; NULL means a personal/global phone.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public ulong? WorkspaceId { get; init; }

    /// <summary>Column: user_slot; SQL: tinyint unsigned; not null. Slot 1 to 3, enforcing a maximum of three phone numbers for one user.</summary>
    [Column("user_slot", TypeName = "tinyint unsigned")]
    public required byte UserSlot { get; init; }

    /// <summary>Column: shared_account_slot; SQL: tinyint unsigned; not null. Slot 1 to 3 on the phone number, enforcing use by no more than three different users.</summary>
    [Column("shared_account_slot", TypeName = "tinyint unsigned")]
    public required byte SharedAccountSlot { get; init; }

    /// <summary>Column: phone_type; SQL: varchar(24); not null. MOBILE, WORK, HOME or RECOVERY.</summary>
    [Column("phone_type", TypeName = "varchar(24)")]
    public required string PhoneType { get; init; }

    /// <summary>Column: is_primary; SQL: tinyint(1); not null.</summary>
    [Column("is_primary", TypeName = "tinyint(1)")]
    public required bool IsPrimary { get; init; }

    /// <summary>Column: sms_enabled; SQL: tinyint(1); not null. Whether the verified number may receive authentication or notification SMS.</summary>
    [Column("sms_enabled", TypeName = "tinyint(1)")]
    public required bool SmsEnabled { get; init; }

    /// <summary>Column: verified_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("verified_utc", TypeName = "datetime(6)")]
    public DateTime? VerifiedUtc { get; init; }

    /// <summary>Column: disabled_utc; SQL: datetime(6); nullable. Disabled associations continue occupying their slots until formally anonymized and removed.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("disabled_utc", TypeName = "datetime(6)")]
    public DateTime? DisabledUtc { get; init; }

    /// <summary>Column: identity_contact_reference; SQL: varchar(255); nullable. Opaque reference to the Identity Service; no OTP or provider secret is stored here.</summary>
    [Column("identity_contact_reference", TypeName = "varchar(255)")]
    public string? IdentityContactReference { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }
}
