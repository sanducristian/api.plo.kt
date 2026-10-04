// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Audit;

/// <summary>Maps one row of the audit_event table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("audit_event")]
public sealed record AuditEvent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; nullable. Workspace in which the action occurred; NULL only for platform-level events.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public ulong? WorkspaceId { get; init; }

    /// <summary>Column: actor_principal_id; SQL: bigint unsigned; not null. Human, system, integration or migration actor responsible for the event.</summary>
    [Column("actor_principal_id", TypeName = "bigint unsigned")]
    public required ulong ActorPrincipalId { get; init; }

    /// <summary>Column: event_type; SQL: varchar(160); not null. Invariant machine-readable event code.</summary>
    [Column("event_type", TypeName = "varchar(160)")]
    public required string EventType { get; init; }

    /// <summary>Column: entity_type; SQL: varchar(128); nullable. Module-defined entity type affected by the event.</summary>
    [Column("entity_type", TypeName = "varchar(128)")]
    public string? EntityType { get; init; }

    /// <summary>Column: entity_id; SQL: varchar(191); nullable. Entity identifier rendered as invariant text so modules may use different ID formats.</summary>
    [Column("entity_id", TypeName = "varchar(191)")]
    public string? EntityId { get; init; }

    /// <summary>Column: occurred_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredUtc { get; init; }

    /// <summary>Column: correlation_id; SQL: char(36); nullable. Request or workflow correlation identifier.</summary>
    [Column("correlation_id", TypeName = "char(36)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: authentication_method; SQL: varchar(32); nullable. LOCAL, ENTRA_ID, GOOGLE, SYSTEM or another validated method.</summary>
    [Column("authentication_method", TypeName = "varchar(32)")]
    public string? AuthenticationMethod { get; init; }

    /// <summary>Column: outcome; SQL: varchar(16); not null. SUCCESS, FAILURE or DENIED.</summary>
    [Column("outcome", TypeName = "varchar(16)")]
    public required string Outcome { get; init; }

    /// <summary>Column: source_ip; SQL: varbinary(16); nullable. IPv4 or IPv6 address in packed binary form when collection is permitted.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("source_ip", TypeName = "varbinary(16)")]
    public byte[]? SourceIp { get; init; }

    /// <summary>Column: user_agent; SQL: varchar(1000); nullable.</summary>
    [Column("user_agent", TypeName = "varchar(1000)")]
    public string? UserAgent { get; init; }

    /// <summary>Column: details_json; SQL: json; nullable. Structured event details; secrets and unnecessary personal data are forbidden.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("details_json", TypeName = "json")]
    public string? DetailsJson { get; init; }
}
