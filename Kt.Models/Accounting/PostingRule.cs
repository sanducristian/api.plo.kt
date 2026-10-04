// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Accounting;

/// <summary>Maps one row of the acc_posting_rule table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("acc_posting_rule")]
public sealed record PostingRule
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: rule_code; SQL: varchar(64); not null.</summary>
    [Column("rule_code", TypeName = "varchar(64)")]
    public required string RuleCode { get; init; }

    /// <summary>Column: source_type; SQL: varchar(32); not null.</summary>
    [Column("source_type", TypeName = "varchar(32)")]
    public required string SourceType { get; init; }

    /// <summary>Column: event_type; SQL: varchar(32); not null.</summary>
    [Column("event_type", TypeName = "varchar(32)")]
    public required string EventType { get; init; }

    /// <summary>Column: rule_version; SQL: varchar(32); not null.</summary>
    [Column("rule_version", TypeName = "varchar(32)")]
    public required string RuleVersion { get; init; }

    /// <summary>Column: valid_from; SQL: date; not null.</summary>
    [Column("valid_from", TypeName = "date")]
    public required DateOnly ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable.</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: rule_json; SQL: json; not null. Deterministic account-selection and line-generation configuration</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("rule_json", TypeName = "json")]
    public required string RuleJson { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }
}
