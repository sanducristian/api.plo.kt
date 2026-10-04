// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_party_registration table. Includes all 14 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_party_registration")]
public sealed record PartyRegistration
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: party_id; SQL: bigint unsigned; not null. Organization or person that owns the registration</summary>
    [Column("party_id", TypeName = "bigint unsigned")]
    public required ulong PartyId { get; init; }

    /// <summary>Column: scheme_category; SQL: varchar(24); not null. TAX, CUSTOMS, COMPANY, FINANCIAL, TRADE, or OTHER</summary>
    [Column("scheme_category", TypeName = "varchar(24)")]
    public required string SchemeCategory { get; init; }

    /// <summary>Column: scheme_code; SQL: varchar(32); not null. Examples: VAT, EORI, SIREN, SIRET, CUI, LEI, DUNS, AEO</summary>
    [Column("scheme_code", TypeName = "varchar(32)")]
    public required string SchemeCode { get; init; }

    /// <summary>Column: issuing_jurisdiction_code; SQL: varchar(16); not null. ISO country, EU, GLOBAL, or another authority jurisdiction code</summary>
    [Column("issuing_jurisdiction_code", TypeName = "varchar(16)")]
    public required string IssuingJurisdictionCode { get; init; }

    /// <summary>Column: issuing_authority; SQL: varchar(256); nullable. Authority or registry that assigned the identifier</summary>
    [Column("issuing_authority", TypeName = "varchar(256)")]
    public string? IssuingAuthority { get; init; }

    /// <summary>Column: registration_number; SQL: varchar(128); not null. Identifier exactly as issued and displayed</summary>
    [Column("registration_number", TypeName = "varchar(128)")]
    public required string RegistrationNumber { get; init; }

    /// <summary>Column: registration_number_normalized; SQL: varchar(128); not null. Uppercase comparison form without display separators</summary>
    [Column("registration_number_normalized", TypeName = "varchar(128)")]
    public required string RegistrationNumberNormalized { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null. UNVERIFIED, VALID, INVALID, SUSPENDED, or REVOKED</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: is_primary; SQL: tinyint(1); not null. Preferred identifier for this scheme when several exist</summary>
    [Column("is_primary", TypeName = "tinyint(1)")]
    public required bool IsPrimary { get; init; }

    /// <summary>Column: valid_from; SQL: date; nullable. First day on which this registration is valid, when supplied</summary>
    [Column("valid_from", TypeName = "date")]
    public DateOnly? ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable. Last day on which this registration is valid; EORI normally has no expiry but may be invalidated</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: last_verified_at_utc; SQL: datetime(6); nullable. Most recent validation against the issuing authority or trusted service</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("last_verified_at_utc", TypeName = "datetime(6)")]
    public DateTime? LastVerifiedAtUtc { get; init; }

    /// <summary>Column: verification_reference; SQL: varchar(512); nullable. Validation response, URL, or external verification identifier</summary>
    [Column("verification_reference", TypeName = "varchar(512)")]
    public string? VerificationReference { get; init; }
}
