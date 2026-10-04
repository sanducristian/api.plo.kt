// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_mfa_method table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_mfa_method")]
public sealed record PlatformUserMfaMethod
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: platform_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("platform_user_id", TypeName = "bigint unsigned")]
    public required ulong PlatformUserId { get; init; }

    /// <summary>Column: method_type; SQL: varchar(24); not null. TOTP, SMS, RECOVERY_CODES or PASSKEY.</summary>
    [Column("method_type", TypeName = "varchar(24)")]
    public required string MethodType { get; init; }

    /// <summary>Column: identity_method_reference; SQL: varchar(255); not null. Opaque Identity Service method identifier; never store the method secret here.</summary>
    [Column("identity_method_reference", TypeName = "varchar(255)")]
    public required string IdentityMethodReference { get; init; }

    /// <summary>Column: platform_user_phone_id; SQL: bigint unsigned; nullable. Verified phone association used by an SMS method, when applicable.</summary>
    [Column("platform_user_phone_id", TypeName = "bigint unsigned")]
    public ulong? PlatformUserPhoneId { get; init; }

    /// <summary>Column: status; SQL: varchar(16); not null. PENDING, ACTIVE, SUSPENDED or REVOKED.</summary>
    [Column("status", TypeName = "varchar(16)")]
    public required string Status { get; init; }

    /// <summary>Column: enrolled_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("enrolled_utc", TypeName = "datetime(6)")]
    public required DateTime EnrolledUtc { get; init; }

    /// <summary>Column: last_used_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("last_used_utc", TypeName = "datetime(6)")]
    public DateTime? LastUsedUtc { get; init; }

    /// <summary>Column: revoked_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("revoked_utc", TypeName = "datetime(6)")]
    public DateTime? RevokedUtc { get; init; }
}
