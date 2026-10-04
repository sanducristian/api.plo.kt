// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_referral_attribution table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_referral_attribution")]
public sealed record PlatformUserReferralAttribution
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: referred_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("referred_user_id", TypeName = "bigint unsigned")]
    public required ulong ReferredUserId { get; init; }

    /// <summary>Column: referral_code_id; SQL: bigint unsigned; not null.</summary>
    [Column("referral_code_id", TypeName = "bigint unsigned")]
    public required ulong ReferralCodeId { get; init; }

    /// <summary>Column: onboarding_id; SQL: bigint unsigned; nullable.</summary>
    [Column("onboarding_id", TypeName = "bigint unsigned")]
    public ulong? OnboardingId { get; init; }

    /// <summary>Column: validation_outcome; SQL: varchar(32); not null.</summary>
    [Column("validation_outcome", TypeName = "varchar(32)")]
    public required string ValidationOutcome { get; init; }

    /// <summary>Column: anti_fraud_state; SQL: varchar(32); not null.</summary>
    [Column("anti_fraud_state", TypeName = "varchar(32)")]
    public required string AntiFraudState { get; init; }

    /// <summary>Column: accepted_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("accepted_utc", TypeName = "datetime(6)")]
    public required DateTime AcceptedUtc { get; init; }

    /// <summary>Column: reversed_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("reversed_utc", TypeName = "datetime(6)")]
    public DateTime? ReversedUtc { get; init; }

    /// <summary>Column: reversal_reason; SQL: varchar(255); nullable.</summary>
    [Column("reversal_reason", TypeName = "varchar(255)")]
    public string? ReversalReason { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }
}
