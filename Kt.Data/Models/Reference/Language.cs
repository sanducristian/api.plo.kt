// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Reference;

/// <summary>Maps one row of the sys_language table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_language")]
public sealed record Language
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global internal identity and primary key, allocated via objId</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: language_name; SQL: varchar(64); not null. Standard English name of the language</summary>
    [Column("language_name", TypeName = "varchar(64)")]
    public required string LanguageName { get; init; }

    /// <summary>Column: native_name; SQL: varchar(64); nullable. Native localized name of the language</summary>
    [Column("native_name", TypeName = "varchar(64)")]
    public string? NativeName { get; init; }

    /// <summary>Column: iso_code_2; SQL: varchar(16); nullable. ISO 639-1 two-letter language code (e.g., en, fr)</summary>
    [Column("iso_code_2", TypeName = "varchar(16)")]
    public string? IsoCode2 { get; init; }

    /// <summary>Column: iso_code_3; SQL: varchar(16); nullable. ISO 639-2 three-letter language code (e.g., eng, fra)</summary>
    [Column("iso_code_3", TypeName = "varchar(16)")]
    public string? IsoCode3 { get; init; }

    /// <summary>Column: locale_code; SQL: varchar(16); nullable. Standard locale code (e.g., en-US, fr-FR)</summary>
    [Column("locale_code", TypeName = "varchar(16)")]
    public string? LocaleCode { get; init; }

    /// <summary>Column: legacy_code; SQL: varchar(16); nullable. Legacy or alternative short code variant</summary>
    [Column("legacy_code", TypeName = "varchar(16)")]
    public string? LegacyCode { get; init; }

    /// <summary>Column: description; SQL: varchar(64); nullable. Internal notes or description of the language entry</summary>
    [Column("description", TypeName = "varchar(64)")]
    public string? Description { get; init; }
}
