// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Audit;

/// <summary>Maps one row of the sys_event table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_event")]
public sealed record SystemEvent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global internal identity and primary key for the event</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null. Application-generated UUID stored as binary for safe API exposure</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: event_category; SQL: varchar(64); not null. High-level grouping category for SaaS analytics and filtering</summary>
    [Column("event_category", TypeName = "varchar(64)")]
    public required string EventCategory { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. Immutable exact microsecond UTC timestamp of event creation</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: actor_principal_id; SQL: bigint unsigned; not null. Reference to security_principal.id identifying the human or non-human actor</summary>
    [Column("actor_principal_id", TypeName = "bigint unsigned")]
    public required ulong ActorPrincipalId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; nullable. Isolation boundary for SaaS company, personal, or household tenant data</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public ulong? WorkspaceId { get; init; }

    /// <summary>Column: event_description; SQL: varchar(255); nullable. Human-readable short description of the system event</summary>
    [Column("event_description", TypeName = "varchar(255)")]
    public string? EventDescription { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable. Distributed tracing identifier for cross-module request linking</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: details_json; SQL: json; nullable. Flexible JSON payload for variable SaaS usage metrics and structured event data</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("details_json", TypeName = "json")]
    public string? DetailsJson { get; init; }

    /// <summary>Column: is_externally_logged; SQL: tinyint(1); not null. Flags 1 if the major event was successfully routed to an external log file</summary>
    [Column("is_externally_logged", TypeName = "tinyint(1)")]
    public required bool IsExternallyLogged { get; init; }
}
