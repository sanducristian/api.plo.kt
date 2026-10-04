// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_item_tariff_assessment table. Includes all 26 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_item_tariff_assessment")]
public sealed record ItemTariffAssessment
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: customs_declaration_item_id; SQL: bigint unsigned; not null.</summary>
    [Column("customs_declaration_item_id", TypeName = "bigint unsigned")]
    public required ulong CustomsDeclarationItemId { get; init; }

    /// <summary>Column: tariff_measure_id; SQL: bigint unsigned; nullable. Reference measure when matched to the tariff catalogue; snapshots below remain authoritative for the assessment</summary>
    [Column("tariff_measure_id", TypeName = "bigint unsigned")]
    public ulong? TariffMeasureId { get; init; }

    /// <summary>Column: measure_type; SQL: varchar(32); not null. Type of duty, tax, quota, suspension, or fee evaluated for this customs item</summary>
    [Column("measure_type", TypeName = "varchar(32)")]
    public required string MeasureType { get; init; }

    /// <summary>Column: measure_code_snapshot; SQL: varchar(64); nullable. Official measure code as used for this assessment</summary>
    [Column("measure_code_snapshot", TypeName = "varchar(64)")]
    public string? MeasureCodeSnapshot { get; init; }

    /// <summary>Column: was_applicable; SQL: tinyint(1); not null. Whether the rule applied to this item based on date, origin, procedure, quota, and classification</summary>
    [Column("was_applicable", TypeName = "tinyint(1)")]
    public required bool WasApplicable { get; init; }

    /// <summary>Column: was_applied; SQL: tinyint(1); not null. Whether customs actually assessed/used the measure for this item</summary>
    [Column("was_applied", TypeName = "tinyint(1)")]
    public required bool WasApplied { get; init; }

    /// <summary>Column: non_application_reason_code; SQL: varchar(64); nullable. Machine-readable reason when an applicable-looking measure was not applied</summary>
    [Column("non_application_reason_code", TypeName = "varchar(64)")]
    public string? NonApplicationReasonCode { get; init; }

    /// <summary>Column: non_application_reason_text; SQL: varchar(512); nullable. Human explanation or customs/broker statement for non-application</summary>
    [Column("non_application_reason_text", TypeName = "varchar(512)")]
    public string? NonApplicationReasonText { get; init; }

    /// <summary>Column: application_date; SQL: date; not null. Date used to select and apply the tariff measure, normally declaration acceptance date</summary>
    [Column("application_date", TypeName = "date")]
    public required DateOnly ApplicationDate { get; init; }

    /// <summary>Column: measure_valid_from_snapshot; SQL: date; nullable. Start of the measure validity copied at assessment time</summary>
    [Column("measure_valid_from_snapshot", TypeName = "date")]
    public DateOnly? MeasureValidFromSnapshot { get; init; }

    /// <summary>Column: measure_valid_to_snapshot; SQL: date; nullable. End of the measure validity copied at assessment time</summary>
    [Column("measure_valid_to_snapshot", TypeName = "date")]
    public DateOnly? MeasureValidToSnapshot { get; init; }

    /// <summary>Column: rate_type; SQL: varchar(24); nullable. PERCENT, AMOUNT_PER_UNIT, FIXED_AMOUNT, COMPOUND, ZERO, or VARIABLE</summary>
    [Column("rate_type", TypeName = "varchar(24)")]
    public string? RateType { get; init; }

    /// <summary>Column: rate_value; SQL: decimal(20,10); nullable. Applied percentage or unit rate; see calculation_json for compound/variable cases</summary>
    [Column("rate_value", TypeName = "decimal(20,10)")]
    public decimal? RateValue { get; init; }

    /// <summary>Column: rate_currency_id; SQL: bigint unsigned; nullable. Currency of a specific/fixed rate</summary>
    [Column("rate_currency_id", TypeName = "bigint unsigned")]
    public ulong? RateCurrencyId { get; init; }

    /// <summary>Column: rate_unit_code; SQL: varchar(32); nullable. Denominator unit for a specific rate</summary>
    [Column("rate_unit_code", TypeName = "varchar(32)")]
    public string? RateUnitCode { get; init; }

    /// <summary>Column: calculation_base_amount; SQL: decimal(20,6); nullable. Customs value or other monetary base used by the calculation</summary>
    [Column("calculation_base_amount", TypeName = "decimal(20,6)")]
    public decimal? CalculationBaseAmount { get; init; }

    /// <summary>Column: calculation_base_quantity; SQL: decimal(20,6); nullable. Mass, count, volume, or supplementary quantity used by a specific rate</summary>
    [Column("calculation_base_quantity", TypeName = "decimal(20,6)")]
    public decimal? CalculationBaseQuantity { get; init; }

    /// <summary>Column: calculation_json; SQL: json; nullable. Formula inputs, preference, quota, exchange rate, and rounding evidence needed to reproduce the result</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("calculation_json", TypeName = "json")]
    public string? CalculationJson { get; init; }

    /// <summary>Column: assessed_amount; SQL: decimal(20,6); not null. Amount assessed for this measure and declaration item</summary>
    [Column("assessed_amount", TypeName = "decimal(20,6)")]
    public required decimal AssessedAmount { get; init; }

    /// <summary>Column: paid_amount; SQL: decimal(20,6); nullable. Amount actually paid or secured, when known independently from assessment</summary>
    [Column("paid_amount", TypeName = "decimal(20,6)")]
    public decimal? PaidAmount { get; init; }

    /// <summary>Column: amount_currency_id; SQL: bigint unsigned; not null. Currency of assessed_amount and paid_amount</summary>
    [Column("amount_currency_id", TypeName = "bigint unsigned")]
    public required ulong AmountCurrencyId { get; init; }

    /// <summary>Column: assessment_reference; SQL: varchar(256); nullable. Customs debt, broker calculation, collection receipt, or decision reference</summary>
    [Column("assessment_reference", TypeName = "varchar(256)")]
    public string? AssessmentReference { get; init; }

    /// <summary>Column: assessed_at_utc; SQL: datetime(6); nullable. Timestamp at which the amount was assessed or imported into PLO</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("assessed_at_utc", TypeName = "datetime(6)")]
    public DateTime? AssessedAtUtc { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }
}
