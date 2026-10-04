// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_tariff_classification table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_tariff_classification")]
public sealed record TariffClassification
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: nomenclature_system; SQL: varchar(24); not null. HS, CN, TARIC, HTS, UKGT, or another jurisdictional nomenclature</summary>
    [Column("nomenclature_system", TypeName = "varchar(24)")]
    public required string NomenclatureSystem { get; init; }

    /// <summary>Column: jurisdiction_code; SQL: varchar(16); not null. EU, ISO country code, or another customs-territory code</summary>
    [Column("jurisdiction_code", TypeName = "varchar(16)")]
    public required string JurisdictionCode { get; init; }

    /// <summary>Column: tariff_code; SQL: varchar(32); not null. Classification code stored without display spaces or punctuation</summary>
    [Column("tariff_code", TypeName = "varchar(32)")]
    public required string TariffCode { get; init; }

    /// <summary>Column: description; SQL: varchar(1024); not null. Description applicable to this code/version; not a translated UI resource</summary>
    [Column("description", TypeName = "varchar(1024)")]
    public required string Description { get; init; }

    /// <summary>Column: valid_from; SQL: date; not null. First day on which this classification version is applicable</summary>
    [Column("valid_from", TypeName = "date")]
    public required DateOnly ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable. Last day on which this classification version is applicable</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: source_version; SQL: varchar(64); nullable. Published nomenclature edition, for example CN 2026</summary>
    [Column("source_version", TypeName = "varchar(64)")]
    public string? SourceVersion { get; init; }

    /// <summary>Column: source_reference; SQL: varchar(256); nullable. Official regulation, decision, ruling, or dataset identifier</summary>
    [Column("source_reference", TypeName = "varchar(256)")]
    public string? SourceReference { get; init; }

    /// <summary>Column: source_url; SQL: varchar(2048); nullable. Stable official source URL used to verify the classification</summary>
    [Column("source_url", TypeName = "varchar(2048)")]
    public string? SourceUrl { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }
}
