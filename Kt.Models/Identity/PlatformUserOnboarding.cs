// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_onboarding table. Includes all 16 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_onboarding")]
public sealed record PlatformUserOnboarding
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: platform_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("platform_user_id", TypeName = "bigint unsigned")]
    public required ulong PlatformUserId { get; init; }

    /// <summary>Column: registration_channel; SQL: varchar(64); not null.</summary>
    [Column("registration_channel", TypeName = "varchar(64)")]
    public required string RegistrationChannel { get; init; }

    /// <summary>Column: stage; SQL: varchar(32); not null.</summary>
    [Column("stage", TypeName = "varchar(32)")]
    public required string Stage { get; init; }

    /// <summary>Column: policy_version; SQL: varchar(32); not null.</summary>
    [Column("policy_version", TypeName = "varchar(32)")]
    public required string PolicyVersion { get; init; }

    /// <summary>Column: company_invitation_id; SQL: bigint unsigned; nullable.</summary>
    [Column("company_invitation_id", TypeName = "bigint unsigned")]
    public ulong? CompanyInvitationId { get; init; }

    /// <summary>Column: device_claim_id; SQL: bigint unsigned; nullable.</summary>
    [Column("device_claim_id", TypeName = "bigint unsigned")]
    public ulong? DeviceClaimId { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: initiating_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("initiating_principal_id", TypeName = "bigint unsigned")]
    public required ulong InitiatingPrincipalId { get; init; }

    /// <summary>Column: started_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("started_utc", TypeName = "datetime(6)")]
    public required DateTime StartedUtc { get; init; }

    /// <summary>Column: completed_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("completed_utc", TypeName = "datetime(6)")]
    public DateTime? CompletedUtc { get; init; }

    /// <summary>Column: cancelled_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("cancelled_utc", TypeName = "datetime(6)")]
    public DateTime? CancelledUtc { get; init; }

    /// <summary>Column: expired_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("expired_utc", TypeName = "datetime(6)")]
    public DateTime? ExpiredUtc { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
