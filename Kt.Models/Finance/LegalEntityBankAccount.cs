// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_legal_entity_bank_account table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_legal_entity_bank_account")]
public sealed record LegalEntityBankAccount
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: party_bank_account_id; SQL: bigint unsigned; not null. Bank account whose owning party must equal the legal-entity party</summary>
    [Column("party_bank_account_id", TypeName = "bigint unsigned")]
    public required ulong PartyBankAccountId { get; init; }

    /// <summary>Column: gl_account_id; SQL: bigint unsigned; not null. BANK control account receiving statement postings for this account</summary>
    [Column("gl_account_id", TypeName = "bigint unsigned")]
    public required ulong GlAccountId { get; init; }

    /// <summary>Column: journal_id; SQL: bigint unsigned; not null. BANK journal used for imported-statement postings</summary>
    [Column("journal_id", TypeName = "bigint unsigned")]
    public required ulong JournalId { get; init; }

    /// <summary>Column: statement_import_enabled; SQL: tinyint(1); not null.</summary>
    [Column("statement_import_enabled", TypeName = "tinyint(1)")]
    public required bool StatementImportEnabled { get; init; }

    /// <summary>Column: is_primary; SQL: tinyint(1); not null. Preferred operating account; uniqueness per entity/currency is enforced by the application</summary>
    [Column("is_primary", TypeName = "tinyint(1)")]
    public required bool IsPrimary { get; init; }

    /// <summary>Column: valid_from; SQL: date; nullable.</summary>
    [Column("valid_from", TypeName = "date")]
    public DateOnly? ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable.</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }
}
