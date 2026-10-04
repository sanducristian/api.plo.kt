// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Reference;

/// <summary>Maps one row of the sys_currency table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_currency")]
public sealed record Currency
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: name; SQL: varchar(128); not null.</summary>
    [Column("name", TypeName = "varchar(128)")]
    public required string Name { get; init; }

    /// <summary>Column: short_name; SQL: varchar(12); not null.</summary>
    [Column("short_name", TypeName = "varchar(12)")]
    public required string ShortName { get; init; }

    /// <summary>Column: iso_code; SQL: char(3); not null. ISO 4217 alphabetic code</summary>
    [Column("iso_code", TypeName = "char(3)")]
    public required string IsoCode { get; init; }

    /// <summary>Column: numeric_code; SQL: char(3); nullable. ISO 4217 numeric code</summary>
    [Column("numeric_code", TypeName = "char(3)")]
    public string? NumericCode { get; init; }

    /// <summary>Column: symbol; SQL: varchar(16); not null.</summary>
    [Column("symbol", TypeName = "varchar(16)")]
    public required string Symbol { get; init; }

    /// <summary>Column: minor_unit; SQL: tinyint unsigned; nullable. ISO 4217 minor-unit precision; NULL when ISO publishes N.A.</summary>
    [Column("minor_unit", TypeName = "tinyint unsigned")]
    public byte? MinorUnit { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null. Available for new transactions</summary>
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

    /// <summary>Column: row_version; SQL: bigint unsigned; not null. Incremented by the application on catalogue edits</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
