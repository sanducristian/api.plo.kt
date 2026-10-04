// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_product_classification table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_product_classification")]
public sealed record ProductClassification
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: product_id; SQL: bigint unsigned; not null. Cross-module product/component ID; formal FK follows the product migration</summary>
    [Column("product_id", TypeName = "bigint unsigned")]
    public required ulong ProductId { get; init; }

    /// <summary>Column: tariff_classification_id; SQL: bigint unsigned; not null.</summary>
    [Column("tariff_classification_id", TypeName = "bigint unsigned")]
    public required ulong TariffClassificationId { get; init; }

    /// <summary>Column: country_of_origin_code; SQL: char(2); nullable. Optional origin-specific assignment where classification or measures differ by origin</summary>
    [Column("country_of_origin_code", TypeName = "char(2)")]
    public string? CountryOfOriginCode { get; init; }

    /// <summary>Column: binding_decision_reference; SQL: varchar(128); nullable. Binding tariff information or equivalent ruling number, when available</summary>
    [Column("binding_decision_reference", TypeName = "varchar(128)")]
    public string? BindingDecisionReference { get; init; }

    /// <summary>Column: valid_from; SQL: date; not null. First date this assignment may be used for the product</summary>
    [Column("valid_from", TypeName = "date")]
    public required DateOnly ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable. Last date this assignment may be used for the product</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: is_preferred; SQL: tinyint(1); not null. Default assignment selected by the application for this jurisdiction and date</summary>
    [Column("is_preferred", TypeName = "tinyint(1)")]
    public required bool IsPreferred { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }
}
