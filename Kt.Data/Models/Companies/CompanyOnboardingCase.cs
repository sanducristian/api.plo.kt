// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Companies;

/// <summary>Maps one row of the company_onboarding_case table. Includes all 17 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("company_onboarding_case")]
public sealed record CompanyOnboardingCase
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: company_workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("company_workspace_id", TypeName = "bigint unsigned")]
    public required ulong CompanyWorkspaceId { get; init; }

    /// <summary>Column: core_party_id; SQL: bigint unsigned; nullable.</summary>
    [Column("core_party_id", TypeName = "bigint unsigned")]
    public ulong? CorePartyId { get; init; }

    /// <summary>Column: core_legal_entity_id; SQL: bigint unsigned; nullable.</summary>
    [Column("core_legal_entity_id", TypeName = "bigint unsigned")]
    public ulong? CoreLegalEntityId { get; init; }

    /// <summary>Column: requesting_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("requesting_user_id", TypeName = "bigint unsigned")]
    public required ulong RequestingUserId { get; init; }

    /// <summary>Column: responsible_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("responsible_principal_id", TypeName = "bigint unsigned")]
    public required ulong ResponsiblePrincipalId { get; init; }

    /// <summary>Column: creation_source; SQL: varchar(32); not null.</summary>
    [Column("creation_source", TypeName = "varchar(32)")]
    public required string CreationSource { get; init; }

    /// <summary>Column: validation_status; SQL: varchar(32); not null.</summary>
    [Column("validation_status", TypeName = "varchar(32)")]
    public required string ValidationStatus { get; init; }

    /// <summary>Column: validation_policy_version; SQL: varchar(32); not null.</summary>
    [Column("validation_policy_version", TypeName = "varchar(32)")]
    public required string ValidationPolicyVersion { get; init; }

    /// <summary>Column: reviewer_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("reviewer_principal_id", TypeName = "bigint unsigned")]
    public ulong? ReviewerPrincipalId { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: submitted_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("submitted_utc", TypeName = "datetime(6)")]
    public required DateTime SubmittedUtc { get; init; }

    /// <summary>Column: reviewed_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("reviewed_utc", TypeName = "datetime(6)")]
    public DateTime? ReviewedUtc { get; init; }

    /// <summary>Column: approved_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("approved_utc", TypeName = "datetime(6)")]
    public DateTime? ApprovedUtc { get; init; }

    /// <summary>Column: rejected_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("rejected_utc", TypeName = "datetime(6)")]
    public DateTime? RejectedUtc { get; init; }

    /// <summary>Column: suspended_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("suspended_utc", TypeName = "datetime(6)")]
    public DateTime? SuspendedUtc { get; init; }
}
