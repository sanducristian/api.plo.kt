// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Accounting;

/// <summary>Maps one row of the acc_journal table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("acc_journal")]
public sealed record Journal
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: journal_code; SQL: varchar(32); not null. Stable code such as PURCHASE, SALES, BANK, CASH, INVENTORY, or GENERAL</summary>
    [Column("journal_code", TypeName = "varchar(32)")]
    public required string JournalCode { get; init; }

    /// <summary>Column: name_resource_key; SQL: varchar(128); not null. Localized display-name resource key</summary>
    [Column("name_resource_key", TypeName = "varchar(128)")]
    public required string NameResourceKey { get; init; }

    /// <summary>Column: journal_type; SQL: varchar(24); not null.</summary>
    [Column("journal_type", TypeName = "varchar(24)")]
    public required string JournalType { get; init; }

    /// <summary>Column: next_entry_number; SQL: bigint unsigned; not null. Next number allocated atomically inside this journal sequence</summary>
    [Column("next_entry_number", TypeName = "bigint unsigned")]
    public required ulong NextEntryNumber { get; init; }

    /// <summary>Column: number_prefix; SQL: varchar(32); nullable. Optional prefix used when formatting journal entry numbers</summary>
    [Column("number_prefix", TypeName = "varchar(32)")]
    public string? NumberPrefix { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }
}
