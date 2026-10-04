// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_claim table. Includes all 19 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_claim")]
public sealed record DeviceClaim
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: device_module_ref; SQL: varchar(128); not null. Stable case-sensitive reference owned by the Device module.</summary>
    [Column("device_module_ref", TypeName = "varchar(128)")]
    public required string DeviceModuleRef { get; init; }

    /// <summary>Column: claimant_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("claimant_user_id", TypeName = "bigint unsigned")]
    public required ulong ClaimantUserId { get; init; }

    /// <summary>Column: target_workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("target_workspace_id", TypeName = "bigint unsigned")]
    public required ulong TargetWorkspaceId { get; init; }

    /// <summary>Column: identifier_type; SQL: varchar(32); not null.</summary>
    [Column("identifier_type", TypeName = "varchar(32)")]
    public required string IdentifierType { get; init; }

    /// <summary>Column: protected_lookup_hash; SQL: varchar(64); not null. Protected device-claim lookup value; never store a plaintext claim secret.</summary>
    [Column("protected_lookup_hash", TypeName = "varchar(64)")]
    public required string ProtectedLookupHash { get; init; }

    /// <summary>Column: status; SQL: varchar(32); not null.</summary>
    [Column("status", TypeName = "varchar(32)")]
    public required string Status { get; init; }

    /// <summary>Column: resolved_ownership_ref; SQL: varchar(128); nullable.</summary>
    [Column("resolved_ownership_ref", TypeName = "varchar(128)")]
    public string? ResolvedOwnershipRef { get; init; }

    /// <summary>Column: actor_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("actor_principal_id", TypeName = "bigint unsigned")]
    public required ulong ActorPrincipalId { get; init; }

    /// <summary>Column: source_ip_risk_ref; SQL: varchar(128); nullable.</summary>
    [Column("source_ip_risk_ref", TypeName = "varchar(128)")]
    public string? SourceIpRiskRef { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: submitted_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("submitted_utc", TypeName = "datetime(6)")]
    public required DateTime SubmittedUtc { get; init; }

    /// <summary>Column: verified_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("verified_utc", TypeName = "datetime(6)")]
    public DateTime? VerifiedUtc { get; init; }

    /// <summary>Column: accepted_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("accepted_utc", TypeName = "datetime(6)")]
    public DateTime? AcceptedUtc { get; init; }

    /// <summary>Column: rejected_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("rejected_utc", TypeName = "datetime(6)")]
    public DateTime? RejectedUtc { get; init; }

    /// <summary>Column: cancelled_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("cancelled_utc", TypeName = "datetime(6)")]
    public DateTime? CancelledUtc { get; init; }

    /// <summary>Column: expired_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("expired_utc", TypeName = "datetime(6)")]
    public DateTime? ExpiredUtc { get; init; }

    /// <summary>Column: accepted_device_module_ref; SQL: varchar(128); nullable. Generated key preventing more than one accepted initial claim for a device.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("accepted_device_module_ref", TypeName = "varchar(128)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string? AcceptedDeviceModuleRef { get; init; }
}
