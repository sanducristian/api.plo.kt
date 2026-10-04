// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_office table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_office")]
public sealed record Office
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: country_code; SQL: char(2); not null. ISO 3166-1 alpha-2 country in which the customs office operates</summary>
    [Column("country_code", TypeName = "char(2)")]
    public required string CountryCode { get; init; }

    /// <summary>Column: office_reference_code; SQL: varchar(32); not null. Official customs-office reference used on the declaration</summary>
    [Column("office_reference_code", TypeName = "varchar(32)")]
    public required string OfficeReferenceCode { get; init; }

    /// <summary>Column: office_name; SQL: varchar(256); not null. Official or locally displayed office name</summary>
    [Column("office_name", TypeName = "varchar(256)")]
    public required string OfficeName { get; init; }

    /// <summary>Column: address_json; SQL: json; nullable. Structured address snapshot when supplied by the customs-office directory</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("address_json", TypeName = "json")]
    public string? AddressJson { get; init; }

    /// <summary>Column: source_system; SQL: varchar(64); nullable. Registry or customs system from which the office record was obtained</summary>
    [Column("source_system", TypeName = "varchar(64)")]
    public string? SourceSystem { get; init; }

    /// <summary>Column: source_url; SQL: varchar(2048); nullable. Stable source or registry URL, not an expiring session link</summary>
    [Column("source_url", TypeName = "varchar(2048)")]
    public string? SourceUrl { get; init; }

    /// <summary>Column: valid_from; SQL: date; nullable. First date on which this office reference is valid</summary>
    [Column("valid_from", TypeName = "date")]
    public DateOnly? ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable. Last date on which this office reference is valid</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }
}
