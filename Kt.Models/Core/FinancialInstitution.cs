// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_financial_institution table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_financial_institution")]
public sealed record FinancialInstitution
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID exposed by APIs</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: legal_name; SQL: varchar(256); not null. Official bank, payment institution, treasury, or other account-servicing institution name</summary>
    [Column("legal_name", TypeName = "varchar(256)")]
    public required string LegalName { get; init; }

    /// <summary>Column: country_code; SQL: char(2); nullable. ISO 3166-1 alpha-2 country of the institution or branch</summary>
    [Column("country_code", TypeName = "char(2)")]
    public string? CountryCode { get; init; }

    /// <summary>Column: bic; SQL: varchar(11); nullable. ISO 9362 BIC/SWIFT code without spaces; 8 or 11 characters when available</summary>
    [Column("bic", TypeName = "varchar(11)")]
    public string? Bic { get; init; }

    /// <summary>Column: national_clearing_code; SQL: varchar(64); nullable. National bank/clearing identifier when BIC is absent or insufficient</summary>
    [Column("national_clearing_code", TypeName = "varchar(64)")]
    public string? NationalClearingCode { get; init; }

    /// <summary>Column: branch_code; SQL: varchar(64); nullable. Institution branch identifier used by the relevant national payment system</summary>
    [Column("branch_code", TypeName = "varchar(64)")]
    public string? BranchCode { get; init; }

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
}
