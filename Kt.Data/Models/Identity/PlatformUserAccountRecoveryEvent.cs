// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_account_recovery_event table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_account_recovery_event")]
public sealed record PlatformUserAccountRecoveryEvent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: account_recovery_case_id; SQL: bigint unsigned; not null.</summary>
    [Column("account_recovery_case_id", TypeName = "bigint unsigned")]
    public required ulong AccountRecoveryCaseId { get; init; }

    /// <summary>Column: event_type; SQL: varchar(64); not null.</summary>
    [Column("event_type", TypeName = "varchar(64)")]
    public required string EventType { get; init; }

    /// <summary>Column: previous_status; SQL: varchar(32); nullable.</summary>
    [Column("previous_status", TypeName = "varchar(32)")]
    public string? PreviousStatus { get; init; }

    /// <summary>Column: new_status; SQL: varchar(32); nullable.</summary>
    [Column("new_status", TypeName = "varchar(32)")]
    public string? NewStatus { get; init; }

    /// <summary>Column: actor_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("actor_principal_id", TypeName = "bigint unsigned")]
    public required ulong ActorPrincipalId { get; init; }

    /// <summary>Column: reason_code; SQL: varchar(64); nullable.</summary>
    [Column("reason_code", TypeName = "varchar(64)")]
    public string? ReasonCode { get; init; }

    /// <summary>Column: identity_service_event_ref; SQL: varchar(255); nullable. Opaque Identity Service reference; never a token, OTP, password or recovery code.</summary>
    [Column("identity_service_event_ref", TypeName = "varchar(255)")]
    public string? IdentityServiceEventRef { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: details_json; SQL: json; nullable. Redacted structured details; authentication secrets and raw identity documents are forbidden.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("details_json", TypeName = "json")]
    public string? DetailsJson { get; init; }

    /// <summary>Column: occurred_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredUtc { get; init; }
}
