// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_country_policy table. Includes all 7 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_country_policy")]
public sealed record CountryPolicy
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: core_country_pack_id; SQL: bigint unsigned; not null.</summary>
    [Column("core_country_pack_id", TypeName = "bigint unsigned")]
    public required ulong CoreCountryPackId { get; init; }

    /// <summary>Column: numbering_policy_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("numbering_policy_json", TypeName = "json")]
    public string? NumberingPolicyJson { get; init; }

    /// <summary>Column: retention_policy_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("retention_policy_json", TypeName = "json")]
    public string? RetentionPolicyJson { get; init; }

    /// <summary>Column: einvoice_policy_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("einvoice_policy_json", TypeName = "json")]
    public string? EinvoicePolicyJson { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: updated_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_at_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedAtUtc { get; init; }
}
