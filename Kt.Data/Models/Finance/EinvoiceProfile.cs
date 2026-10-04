// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_einvoice_profile table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_einvoice_profile")]
public sealed record EinvoiceProfile
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: country_pack_id; SQL: bigint unsigned; nullable. Country-pack version whose mandate and posting rules govern this profile</summary>
    [Column("country_pack_id", TypeName = "bigint unsigned")]
    public ulong? CountryPackId { get; init; }

    /// <summary>Column: jurisdiction_code; SQL: varchar(16); not null. FR, RO, AE, EU, or another country/customs/tax jurisdiction</summary>
    [Column("jurisdiction_code", TypeName = "varchar(16)")]
    public required string JurisdictionCode { get; init; }

    /// <summary>Column: profile_code; SQL: varchar(64); not null. Stable profile such as FR_PPF, RO_EFACTURA, AE_EINVOICING, PEPPOL_BIS, or GENERIC_UBL</summary>
    [Column("profile_code", TypeName = "varchar(64)")]
    public required string ProfileCode { get; init; }

    /// <summary>Column: direction_code; SQL: varchar(16); not null. INBOUND, OUTBOUND, or BOTH</summary>
    [Column("direction_code", TypeName = "varchar(16)")]
    public required string DirectionCode { get; init; }

    /// <summary>Column: network_code; SQL: varchar(64); nullable. Exchange network/platform such as PPF/PDP, RO e-Factura, Peppol, or UAE accredited provider</summary>
    [Column("network_code", TypeName = "varchar(64)")]
    public string? NetworkCode { get; init; }

    /// <summary>Column: syntax_code; SQL: varchar(32); not null. UBL, CII, FACTUR_X, JSON, XML, or another accepted structured syntax</summary>
    [Column("syntax_code", TypeName = "varchar(32)")]
    public required string SyntaxCode { get; init; }

    /// <summary>Column: profile_version; SQL: varchar(64); not null. Schema/business-rules version used to validate and reproduce the exchange</summary>
    [Column("profile_version", TypeName = "varchar(64)")]
    public required string ProfileVersion { get; init; }

    /// <summary>Column: valid_from; SQL: date; not null.</summary>
    [Column("valid_from", TypeName = "date")]
    public required DateOnly ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable.</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: configuration_key; SQL: varchar(128); nullable. Non-secret key selecting endpoint/configuration; credentials stay in the secrets store</summary>
    [Column("configuration_key", TypeName = "varchar(128)")]
    public string? ConfigurationKey { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }
}
