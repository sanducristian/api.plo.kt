// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_declaration_item table. Includes all 17 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_declaration_item")]
public sealed record DeclarationItem
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: customs_declaration_id; SQL: bigint unsigned; not null.</summary>
    [Column("customs_declaration_id", TypeName = "bigint unsigned")]
    public required ulong CustomsDeclarationId { get; init; }

    /// <summary>Column: item_number; SQL: int unsigned; not null. Line/item number used in the accepted customs declaration</summary>
    [Column("item_number", TypeName = "int unsigned")]
    public required uint ItemNumber { get; init; }

    /// <summary>Column: tariff_classification_id; SQL: bigint unsigned; nullable. Versioned classification valid for the declaration acceptance date</summary>
    [Column("tariff_classification_id", TypeName = "bigint unsigned")]
    public ulong? TariffClassificationId { get; init; }

    /// <summary>Column: tariff_code_snapshot; SQL: varchar(32); not null. Exact commodity/tariff code declared to customs</summary>
    [Column("tariff_code_snapshot", TypeName = "varchar(32)")]
    public required string TariffCodeSnapshot { get; init; }

    /// <summary>Column: goods_description; SQL: varchar(1024); not null. Goods description used on the declaration</summary>
    [Column("goods_description", TypeName = "varchar(1024)")]
    public required string GoodsDescription { get; init; }

    /// <summary>Column: origin_country_code; SQL: char(2); nullable. Declared non-preferential country of origin</summary>
    [Column("origin_country_code", TypeName = "char(2)")]
    public string? OriginCountryCode { get; init; }

    /// <summary>Column: preferential_origin_country_code; SQL: char(2); nullable. Preferential origin claimed for a trade preference, if different</summary>
    [Column("preferential_origin_country_code", TypeName = "char(2)")]
    public string? PreferentialOriginCountryCode { get; init; }

    /// <summary>Column: gross_mass_kg; SQL: decimal(20,6); nullable. Gross mass in kilograms for this customs item</summary>
    [Column("gross_mass_kg", TypeName = "decimal(20,6)")]
    public decimal? GrossMassKg { get; init; }

    /// <summary>Column: net_mass_kg; SQL: decimal(20,6); nullable. Net mass in kilograms excluding packaging</summary>
    [Column("net_mass_kg", TypeName = "decimal(20,6)")]
    public decimal? NetMassKg { get; init; }

    /// <summary>Column: supplementary_quantity; SQL: decimal(20,6); nullable. Quantity required by the tariff nomenclature in addition to mass</summary>
    [Column("supplementary_quantity", TypeName = "decimal(20,6)")]
    public decimal? SupplementaryQuantity { get; init; }

    /// <summary>Column: supplementary_unit_code; SQL: varchar(32); nullable. Unit for supplementary_quantity as required by the tariff measure</summary>
    [Column("supplementary_unit_code", TypeName = "varchar(32)")]
    public string? SupplementaryUnitCode { get; init; }

    /// <summary>Column: customs_value_amount; SQL: decimal(20,6); not null. Customs valuation base attributed to this declaration item</summary>
    [Column("customs_value_amount", TypeName = "decimal(20,6)")]
    public required decimal CustomsValueAmount { get; init; }

    /// <summary>Column: customs_value_currency_id; SQL: bigint unsigned; not null.</summary>
    [Column("customs_value_currency_id", TypeName = "bigint unsigned")]
    public required ulong CustomsValueCurrencyId { get; init; }

    /// <summary>Column: invoice_value_amount; SQL: decimal(20,6); nullable. Commercial invoice value attributed to this declaration item</summary>
    [Column("invoice_value_amount", TypeName = "decimal(20,6)")]
    public decimal? InvoiceValueAmount { get; init; }

    /// <summary>Column: statistical_value_amount; SQL: decimal(20,6); nullable. Statistical value reported on the declaration when required</summary>
    [Column("statistical_value_amount", TypeName = "decimal(20,6)")]
    public decimal? StatisticalValueAmount { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }
}
