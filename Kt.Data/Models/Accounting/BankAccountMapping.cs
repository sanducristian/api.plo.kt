// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Accounting;

/// <summary>Maps one row of the acc_bank_account_mapping table. Includes all 5 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("acc_bank_account_mapping")]
public sealed record BankAccountMapping
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: finance_legal_entity_bank_account_id; SQL: bigint unsigned; not null.</summary>
    [Column("finance_legal_entity_bank_account_id", TypeName = "bigint unsigned")]
    public required ulong FinanceLegalEntityBankAccountId { get; init; }

    /// <summary>Column: gl_account_id; SQL: bigint unsigned; not null.</summary>
    [Column("gl_account_id", TypeName = "bigint unsigned")]
    public required ulong GlAccountId { get; init; }

    /// <summary>Column: journal_id; SQL: bigint unsigned; not null.</summary>
    [Column("journal_id", TypeName = "bigint unsigned")]
    public required ulong JournalId { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }
}
