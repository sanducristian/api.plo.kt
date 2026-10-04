// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_party_registration_verification table. Includes all 15 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_party_registration_verification")]
public sealed record PartyRegistrationVerification
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: party_registration_id; SQL: bigint unsigned; not null.</summary>
    [Column("party_registration_id", TypeName = "bigint unsigned")]
    public required ulong PartyRegistrationId { get; init; }

    /// <summary>Column: verification_source; SQL: varchar(64); not null. Registry, provider or MANUAL_REVIEW source code.</summary>
    [Column("verification_source", TypeName = "varchar(64)")]
    public required string VerificationSource { get; init; }

    /// <summary>Column: verification_method; SQL: varchar(64); not null.</summary>
    [Column("verification_method", TypeName = "varchar(64)")]
    public required string VerificationMethod { get; init; }

    /// <summary>Column: result_code; SQL: varchar(24); not null. PENDING, VALID, INVALID, INCONCLUSIVE, ERROR or CANCELLED.</summary>
    [Column("result_code", TypeName = "varchar(24)")]
    public required string ResultCode { get; init; }

    /// <summary>Column: reason_code; SQL: varchar(64); nullable.</summary>
    [Column("reason_code", TypeName = "varchar(64)")]
    public string? ReasonCode { get; init; }

    /// <summary>Column: external_reference; SQL: varchar(512); nullable. Opaque registry/provider response reference; never a credential or token.</summary>
    [Column("external_reference", TypeName = "varchar(512)")]
    public string? ExternalReference { get; init; }

    /// <summary>Column: evidence_file_id; SQL: bigint unsigned; nullable. Optional verification evidence stored through objfile under an approved retention policy.</summary>
    [Column("evidence_file_id", TypeName = "bigint unsigned")]
    public ulong? EvidenceFileId { get; init; }

    /// <summary>Column: matched_legal_name; SQL: varchar(256); nullable.</summary>
    [Column("matched_legal_name", TypeName = "varchar(256)")]
    public string? MatchedLegalName { get; init; }

    /// <summary>Column: matched_country_code; SQL: char(2); nullable.</summary>
    [Column("matched_country_code", TypeName = "char(2)")]
    public string? MatchedCountryCode { get; init; }

    /// <summary>Column: requested_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("requested_utc", TypeName = "datetime(6)")]
    public required DateTime RequestedUtc { get; init; }

    /// <summary>Column: completed_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("completed_utc", TypeName = "datetime(6)")]
    public DateTime? CompletedUtc { get; init; }

    /// <summary>Column: valid_until_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("valid_until_utc", TypeName = "datetime(6)")]
    public DateTime? ValidUntilUtc { get; init; }

    /// <summary>Column: actor_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("actor_principal_id", TypeName = "bigint unsigned")]
    public required ulong ActorPrincipalId { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }
}
