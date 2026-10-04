// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_legal_entity table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_legal_entity")]
public sealed record LegalEntity
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: party_id; SQL: bigint unsigned; not null. Party whose accounting books and legal obligations are represented</summary>
    [Column("party_id", TypeName = "bigint unsigned")]
    public required ulong PartyId { get; init; }

    /// <summary>Column: code; SQL: varchar(32); not null. Stable short code used in numbering, permissions, and integrations</summary>
    [Column("code", TypeName = "varchar(32)")]
    public required string Code { get; init; }

    /// <summary>Column: default_currency_id; SQL: bigint unsigned; not null.</summary>
    [Column("default_currency_id", TypeName = "bigint unsigned")]
    public required ulong DefaultCurrencyId { get; init; }

    /// <summary>Column: time_zone_id; SQL: varchar(64); not null. IANA time-zone identifier</summary>
    [Column("time_zone_id", TypeName = "varchar(64)")]
    public required string TimeZoneId { get; init; }

    /// <summary>Column: is_book_active; SQL: tinyint(1); not null. Whether new accounting transactions may be posted to this entity</summary>
    [Column("is_book_active", TypeName = "tinyint(1)")]
    public required bool IsBookActive { get; init; }

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
