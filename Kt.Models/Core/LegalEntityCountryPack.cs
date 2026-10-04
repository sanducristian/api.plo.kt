// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_legal_entity_country_pack table. Includes all 7 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_legal_entity_country_pack")]
public sealed record LegalEntityCountryPack
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: country_pack_id; SQL: bigint unsigned; not null.</summary>
    [Column("country_pack_id", TypeName = "bigint unsigned")]
    public required ulong CountryPackId { get; init; }

    /// <summary>Column: usage_role; SQL: varchar(24); not null. DOMESTIC, TAX_REGISTRATION, FIXED_ESTABLISHMENT, or REPORTING</summary>
    [Column("usage_role", TypeName = "varchar(24)")]
    public required string UsageRole { get; init; }

    /// <summary>Column: valid_from; SQL: date; not null.</summary>
    [Column("valid_from", TypeName = "date")]
    public required DateOnly ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable.</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: configuration_json; SQL: json; nullable. Entity-specific non-secret overrides allowed by the selected country pack</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("configuration_json", TypeName = "json")]
    public string? ConfigurationJson { get; init; }
}
