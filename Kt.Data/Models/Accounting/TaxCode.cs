// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Accounting;

/// <summary>Maps one row of the acc_tax_code table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("acc_tax_code")]
public sealed record TaxCode
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: country_pack_id; SQL: bigint unsigned; nullable. Country-pack version under which this tax code is valid</summary>
    [Column("country_pack_id", TypeName = "bigint unsigned")]
    public ulong? CountryPackId { get; init; }

    /// <summary>Column: code; SQL: varchar(32); not null.</summary>
    [Column("code", TypeName = "varchar(32)")]
    public required string Code { get; init; }

    /// <summary>Column: name_resource_key; SQL: varchar(128); not null.</summary>
    [Column("name_resource_key", TypeName = "varchar(128)")]
    public required string NameResourceKey { get; init; }

    /// <summary>Column: country_code; SQL: char(2); not null.</summary>
    [Column("country_code", TypeName = "char(2)")]
    public required string CountryCode { get; init; }

    /// <summary>Column: tax_type; SQL: varchar(24); not null.</summary>
    [Column("tax_type", TypeName = "varchar(24)")]
    public required string TaxType { get; init; }

    /// <summary>Column: rate_percent; SQL: decimal(9,6); not null.</summary>
    [Column("rate_percent", TypeName = "decimal(9,6)")]
    public required decimal RatePercent { get; init; }

    /// <summary>Column: recoverable_percent; SQL: decimal(9,6); not null.</summary>
    [Column("recoverable_percent", TypeName = "decimal(9,6)")]
    public required decimal RecoverablePercent { get; init; }

    /// <summary>Column: external_tax_code; SQL: varchar(64); nullable.</summary>
    [Column("external_tax_code", TypeName = "varchar(64)")]
    public string? ExternalTaxCode { get; init; }

    /// <summary>Column: valid_from; SQL: date; not null.</summary>
    [Column("valid_from", TypeName = "date")]
    public required DateOnly ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable.</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }
}
