// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Accounting;

/// <summary>Maps one row of the acc_gl_account table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("acc_gl_account")]
public sealed record GlAccount
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: account_code; SQL: varchar(32); not null.</summary>
    [Column("account_code", TypeName = "varchar(32)")]
    public required string AccountCode { get; init; }

    /// <summary>Column: name; SQL: varchar(256); not null.</summary>
    [Column("name", TypeName = "varchar(256)")]
    public required string Name { get; init; }

    /// <summary>Column: account_type; SQL: varchar(24); not null.</summary>
    [Column("account_type", TypeName = "varchar(24)")]
    public required string AccountType { get; init; }

    /// <summary>Column: parent_account_id; SQL: bigint unsigned; nullable. Optional parent used to build the chart-of-accounts hierarchy</summary>
    [Column("parent_account_id", TypeName = "bigint unsigned")]
    public ulong? ParentAccountId { get; init; }

    /// <summary>Column: control_type; SQL: varchar(24); not null. NONE, RECEIVABLE, PAYABLE, BANK, CASH, TAX, INVENTORY, or FIXED_ASSET</summary>
    [Column("control_type", TypeName = "varchar(24)")]
    public required string ControlType { get; init; }

    /// <summary>Column: normal_balance; SQL: varchar(8); not null. DEBIT or CREDIT presentation/default balance</summary>
    [Column("normal_balance", TypeName = "varchar(8)")]
    public required string NormalBalance { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; nullable. Optional fixed account currency; NULL allows the legal-entity policy to decide</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public ulong? CurrencyId { get; init; }

    /// <summary>Column: is_postable; SQL: tinyint(1); not null.</summary>
    [Column("is_postable", TypeName = "tinyint(1)")]
    public required bool IsPostable { get; init; }

    /// <summary>Column: allow_manual_posting; SQL: tinyint(1); not null. False for control accounts that may only be posted through subledger workflows</summary>
    [Column("allow_manual_posting", TypeName = "tinyint(1)")]
    public required bool AllowManualPosting { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }
}
