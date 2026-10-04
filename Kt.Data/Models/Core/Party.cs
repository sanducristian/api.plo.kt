// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_party table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_party")]
public sealed record Party
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID exposed through APIs instead of the database key</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: party_type; SQL: varchar(24); not null. ORGANIZATION or INDIVIDUAL; both may buy, sell, own devices, or receive invoices</summary>
    [Column("party_type", TypeName = "varchar(24)")]
    public required string PartyType { get; init; }

    /// <summary>Column: legal_name; SQL: varchar(256); not null. Registered organization name or official individual name</summary>
    [Column("legal_name", TypeName = "varchar(256)")]
    public required string LegalName { get; init; }

    /// <summary>Column: trade_name; SQL: varchar(256); nullable. Trading or display name when different from legal_name</summary>
    [Column("trade_name", TypeName = "varchar(256)")]
    public string? TradeName { get; init; }

    /// <summary>Column: country_code; SQL: char(2); nullable. ISO 3166-1 alpha-2 country of establishment or residence</summary>
    [Column("country_code", TypeName = "char(2)")]
    public string? CountryCode { get; init; }

    /// <summary>Column: registration_identifier; SQL: varchar(64); nullable. Convenience display identifier; authoritative registrations are stored in core_party_registration</summary>
    [Column("registration_identifier", TypeName = "varchar(64)")]
    public string? RegistrationIdentifier { get; init; }

    /// <summary>Column: address_json; SQL: json; nullable. Current structured primary address; legal document snapshots remain on each invoice</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("address_json", TypeName = "json")]
    public string? AddressJson { get; init; }

    /// <summary>Column: legacy_company_id; SQL: bigint unsigned; nullable. Populated only by the later legacy-client migration</summary>
    [Column("legacy_company_id", TypeName = "bigint unsigned")]
    public ulong? LegacyCompanyId { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: updated_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_at_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedAtUtc { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null. Incremented by the application for optimistic concurrency</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
