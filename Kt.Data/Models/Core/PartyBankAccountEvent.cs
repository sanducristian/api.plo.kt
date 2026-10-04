// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_party_bank_account_event table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_party_bank_account_event")]
public sealed record PartyBankAccountEvent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: party_bank_account_id; SQL: bigint unsigned; not null.</summary>
    [Column("party_bank_account_id", TypeName = "bigint unsigned")]
    public required ulong PartyBankAccountId { get; init; }

    /// <summary>Column: action_code; SQL: varchar(32); not null. CREATED, VERIFICATION_REQUESTED, VERIFIED, REJECTED, SUSPENDED, CLOSED, DEFAULT_CHANGED, or EVIDENCE_ADDED</summary>
    [Column("action_code", TypeName = "varchar(32)")]
    public required string ActionCode { get; init; }

    /// <summary>Column: from_status_code; SQL: varchar(24); nullable.</summary>
    [Column("from_status_code", TypeName = "varchar(24)")]
    public string? FromStatusCode { get; init; }

    /// <summary>Column: to_status_code; SQL: varchar(24); nullable.</summary>
    [Column("to_status_code", TypeName = "varchar(24)")]
    public string? ToStatusCode { get; init; }

    /// <summary>Column: evidence_file_id; SQL: bigint unsigned; nullable. Bank certificate, account proof, verification report, or other supporting file in objfile</summary>
    [Column("evidence_file_id", TypeName = "bigint unsigned")]
    public ulong? EvidenceFileId { get; init; }

    /// <summary>Column: external_reference; SQL: varchar(512); nullable. Stable bank/open-banking/verification reference; never a credential or token</summary>
    [Column("external_reference", TypeName = "varchar(512)")]
    public string? ExternalReference { get; init; }

    /// <summary>Column: comment; SQL: varchar(1024); nullable.</summary>
    [Column("comment", TypeName = "varchar(1024)")]
    public string? Comment { get; init; }

    /// <summary>Column: occurred_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_at_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredAtUtc { get; init; }

    /// <summary>Column: actor_user_id; SQL: bigint unsigned; nullable. New Identity-module user ID or reserved system identity</summary>
    [Column("actor_user_id", TypeName = "bigint unsigned")]
    public ulong? ActorUserId { get; init; }

    /// <summary>Column: correlation_id; SQL: binary(16); nullable.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("correlation_id", TypeName = "binary(16)")]
    public byte[]? CorrelationId { get; init; }
}
