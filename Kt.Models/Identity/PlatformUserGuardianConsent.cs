// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_guardian_consent table. Includes all 15 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_guardian_consent")]
public sealed record PlatformUserGuardianConsent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: guardian_relationship_id; SQL: bigint unsigned; not null.</summary>
    [Column("guardian_relationship_id", TypeName = "bigint unsigned")]
    public required ulong GuardianRelationshipId { get; init; }

    /// <summary>Column: policy_code; SQL: varchar(64); not null.</summary>
    [Column("policy_code", TypeName = "varchar(64)")]
    public required string PolicyCode { get; init; }

    /// <summary>Column: policy_version; SQL: varchar(32); not null.</summary>
    [Column("policy_version", TypeName = "varchar(32)")]
    public required string PolicyVersion { get; init; }

    /// <summary>Column: jurisdiction_code; SQL: varchar(10); not null.</summary>
    [Column("jurisdiction_code", TypeName = "varchar(10)")]
    public required string JurisdictionCode { get; init; }

    /// <summary>Column: consent_scope; SQL: varchar(64); not null.</summary>
    [Column("consent_scope", TypeName = "varchar(64)")]
    public required string ConsentScope { get; init; }

    /// <summary>Column: status; SQL: varchar(32); not null.</summary>
    [Column("status", TypeName = "varchar(32)")]
    public required string Status { get; init; }

    /// <summary>Column: evidence_reference; SQL: varchar(255); not null. Opaque consent evidence reference; never a credential, token or raw identity document.</summary>
    [Column("evidence_reference", TypeName = "varchar(255)")]
    public required string EvidenceReference { get; init; }

    /// <summary>Column: actor_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("actor_principal_id", TypeName = "bigint unsigned")]
    public required ulong ActorPrincipalId { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: granted_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("granted_utc", TypeName = "datetime(6)")]
    public required DateTime GrantedUtc { get; init; }

    /// <summary>Column: expires_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("expires_utc", TypeName = "datetime(6)")]
    public DateTime? ExpiresUtc { get; init; }

    /// <summary>Column: revoked_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("revoked_utc", TypeName = "datetime(6)")]
    public DateTime? RevokedUtc { get; init; }

    /// <summary>Column: revocation_reason; SQL: varchar(255); nullable.</summary>
    [Column("revocation_reason", TypeName = "varchar(255)")]
    public string? RevocationReason { get; init; }
}
