// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_tariff_measure table. Includes all 16 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_tariff_measure")]
public sealed record TariffMeasure
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: tariff_classification_id; SQL: bigint unsigned; not null.</summary>
    [Column("tariff_classification_id", TypeName = "bigint unsigned")]
    public required ulong TariffClassificationId { get; init; }

    /// <summary>Column: measure_type; SQL: varchar(32); not null. Customs duty, anti-dumping, countervailing, excise, import VAT, quota, suspension, fee, or other measure</summary>
    [Column("measure_type", TypeName = "varchar(32)")]
    public required string MeasureType { get; init; }

    /// <summary>Column: measure_code; SQL: varchar(64); nullable. Official TARIC or national measure identifier when one exists</summary>
    [Column("measure_code", TypeName = "varchar(64)")]
    public string? MeasureCode { get; init; }

    /// <summary>Column: origin_country_code; SQL: char(2); nullable. Origin to which this measure applies; NULL means not origin-specific</summary>
    [Column("origin_country_code", TypeName = "char(2)")]
    public string? OriginCountryCode { get; init; }

    /// <summary>Column: destination_country_code; SQL: char(2); nullable. Destination country when the measure is national rather than customs-union wide</summary>
    [Column("destination_country_code", TypeName = "char(2)")]
    public string? DestinationCountryCode { get; init; }

    /// <summary>Column: rate_type; SQL: varchar(24); not null. PERCENT, AMOUNT_PER_UNIT, FIXED_AMOUNT, COMPOUND, ZERO, or VARIABLE</summary>
    [Column("rate_type", TypeName = "varchar(24)")]
    public required string RateType { get; init; }

    /// <summary>Column: rate_value; SQL: decimal(20,10); nullable. Percentage or unit rate; compound/variable formulas are described in rate_formula_json</summary>
    [Column("rate_value", TypeName = "decimal(20,10)")]
    public decimal? RateValue { get; init; }

    /// <summary>Column: rate_currency_id; SQL: bigint unsigned; nullable. Currency of a specific or fixed rate; NULL for percentage/zero rates</summary>
    [Column("rate_currency_id", TypeName = "bigint unsigned")]
    public ulong? RateCurrencyId { get; init; }

    /// <summary>Column: rate_unit_code; SQL: varchar(32); nullable. Unit denominator for AMOUNT_PER_UNIT, such as kg, item, litre, or supplementary unit</summary>
    [Column("rate_unit_code", TypeName = "varchar(32)")]
    public string? RateUnitCode { get; init; }

    /// <summary>Column: rate_formula_json; SQL: json; nullable. Structured compound or variable formula and conditions as supplied by the tariff source</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("rate_formula_json", TypeName = "json")]
    public string? RateFormulaJson { get; init; }

    /// <summary>Column: quota_reference; SQL: varchar(64); nullable. Official tariff-quota order/reference when the measure depends on a quota</summary>
    [Column("quota_reference", TypeName = "varchar(64)")]
    public string? QuotaReference { get; init; }

    /// <summary>Column: legal_basis_reference; SQL: varchar(256); nullable. Regulation, decision, or national legal basis for this measure</summary>
    [Column("legal_basis_reference", TypeName = "varchar(256)")]
    public string? LegalBasisReference { get; init; }

    /// <summary>Column: source_url; SQL: varchar(2048); nullable. Stable official URL used to verify the tariff measure</summary>
    [Column("source_url", TypeName = "varchar(2048)")]
    public string? SourceUrl { get; init; }

    /// <summary>Column: valid_from; SQL: date; not null. First day on which the measure may apply</summary>
    [Column("valid_from", TypeName = "date")]
    public required DateOnly ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable. Last day on which the measure may apply</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }
}
