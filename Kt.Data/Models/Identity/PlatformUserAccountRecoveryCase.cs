// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_account_recovery_case table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_account_recovery_case")]
public sealed record PlatformUserAccountRecoveryCase
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

    /// <summary>Column: recovery_method; SQL: varchar(64); not null.</summary>
    [Column("recovery_method", TypeName = "varchar(64)")]
    public required string RecoveryMethod { get; init; }

    /// <summary>Column: status; SQL: varchar(32); not null.</summary>
    [Column("status", TypeName = "varchar(32)")]
    public required string Status { get; init; }

    /// <summary>Column: identity_document_evidence_ref; SQL: varchar(255); nullable. Optional opaque verification evidence reference; never store document numbers, images or credentials here.</summary>
    [Column("identity_document_evidence_ref", TypeName = "varchar(255)")]
    public string? IdentityDocumentEvidenceRef { get; init; }

    /// <summary>Column: reviewer_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("reviewer_principal_id", TypeName = "bigint unsigned")]
    public ulong? ReviewerPrincipalId { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: initiated_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("initiated_utc", TypeName = "datetime(6)")]
    public required DateTime InitiatedUtc { get; init; }

    /// <summary>Column: approved_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("approved_utc", TypeName = "datetime(6)")]
    public DateTime? ApprovedUtc { get; init; }

    /// <summary>Column: denied_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("denied_utc", TypeName = "datetime(6)")]
    public DateTime? DeniedUtc { get; init; }

    /// <summary>Column: completed_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("completed_utc", TypeName = "datetime(6)")]
    public DateTime? CompletedUtc { get; init; }

    /// <summary>Column: expired_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("expired_utc", TypeName = "datetime(6)")]
    public DateTime? ExpiredUtc { get; init; }
}
