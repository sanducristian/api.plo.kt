// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_country_pack table. Includes all 14 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_country_pack")]
public sealed record CountryPack
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: jurisdiction_code; SQL: varchar(16); not null. ISO country code, EU, or another tax/accounting jurisdiction such as AE, FR, or RO</summary>
    [Column("jurisdiction_code", TypeName = "varchar(16)")]
    public required string JurisdictionCode { get; init; }

    /// <summary>Column: pack_code; SQL: varchar(64); not null. Stable policy family code used by application configuration</summary>
    [Column("pack_code", TypeName = "varchar(64)")]
    public required string PackCode { get; init; }

    /// <summary>Column: pack_version; SQL: varchar(64); not null. Published configuration/business-rule version retained for reproducibility</summary>
    [Column("pack_version", TypeName = "varchar(64)")]
    public required string PackVersion { get; init; }

    /// <summary>Column: valid_from; SQL: date; not null. First legal date on which this pack version may be selected</summary>
    [Column("valid_from", TypeName = "date")]
    public required DateOnly ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable. Last legal date on which this pack version may be selected</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: tax_rounding_scope; SQL: varchar(16); not null. Whether tax is rounded at LINE or DOCUMENT level</summary>
    [Column("tax_rounding_scope", TypeName = "varchar(16)")]
    public required string TaxRoundingScope { get; init; }

    /// <summary>Column: rounding_mode; SQL: varchar(16); not null. HALF_UP, HALF_EVEN, UP, DOWN, CEILING, or FLOOR</summary>
    [Column("rounding_mode", TypeName = "varchar(16)")]
    public required string RoundingMode { get; init; }

    /// <summary>Column: tax_decimal_places; SQL: tinyint unsigned; not null. Default tax calculation/display precision; currency policy may impose a different posting precision</summary>
    [Column("tax_decimal_places", TypeName = "tinyint unsigned")]
    public required byte TaxDecimalPlaces { get; init; }

    /// <summary>Column: numbering_policy_json; SQL: json; nullable. Jurisdiction rules for invoice series, continuity, gaps, prefixes, fiscal years, and corrections</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("numbering_policy_json", TypeName = "json")]
    public string? NumberingPolicyJson { get; init; }

    /// <summary>Column: retention_policy_json; SQL: json; nullable. Legal retention duration, evidence classes, and original-format requirements</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("retention_policy_json", TypeName = "json")]
    public string? RetentionPolicyJson { get; init; }

    /// <summary>Column: tax_policy_json; SQL: json; nullable. Additional versioned tax rules not represented by normalized tax-code rows</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("tax_policy_json", TypeName = "json")]
    public string? TaxPolicyJson { get; init; }

    /// <summary>Column: einvoice_policy_json; SQL: json; nullable. Mandates, routing, deadlines, statuses, and exception rules; credentials are never stored here</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("einvoice_policy_json", TypeName = "json")]
    public string? EinvoicePolicyJson { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }
}
