// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Accounting;

/// <summary>Maps one row of the acc_journal_entry_line table. Includes all 17 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("acc_journal_entry_line")]
public sealed record JournalEntryLine
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: journal_entry_id; SQL: bigint unsigned; not null.</summary>
    [Column("journal_entry_id", TypeName = "bigint unsigned")]
    public required ulong JournalEntryId { get; init; }

    /// <summary>Column: line_number; SQL: int unsigned; not null.</summary>
    [Column("line_number", TypeName = "int unsigned")]
    public required uint LineNumber { get; init; }

    /// <summary>Column: gl_account_id; SQL: bigint unsigned; not null.</summary>
    [Column("gl_account_id", TypeName = "bigint unsigned")]
    public required ulong GlAccountId { get; init; }

    /// <summary>Column: party_id; SQL: bigint unsigned; nullable. Supplier, customer, employee, or other subledger party for receivable/payable lines</summary>
    [Column("party_id", TypeName = "bigint unsigned")]
    public ulong? PartyId { get; init; }

    /// <summary>Column: description; SQL: varchar(512); nullable.</summary>
    [Column("description", TypeName = "varchar(512)")]
    public string? Description { get; init; }

    /// <summary>Column: debit_amount; SQL: decimal(20,6); not null. Book-currency debit; exactly one of debit_amount and credit_amount must be positive</summary>
    [Column("debit_amount", TypeName = "decimal(20,6)")]
    public required decimal DebitAmount { get; init; }

    /// <summary>Column: credit_amount; SQL: decimal(20,6); not null. Book-currency credit; exactly one of debit_amount and credit_amount must be positive</summary>
    [Column("credit_amount", TypeName = "decimal(20,6)")]
    public required decimal CreditAmount { get; init; }

    /// <summary>Column: transaction_currency_id; SQL: bigint unsigned; nullable. Original transaction currency when different from the book currency</summary>
    [Column("transaction_currency_id", TypeName = "bigint unsigned")]
    public ulong? TransactionCurrencyId { get; init; }

    /// <summary>Column: transaction_amount; SQL: decimal(20,6); nullable. Signed original-currency amount; sign convention is defined by debit/credit direction</summary>
    [Column("transaction_amount", TypeName = "decimal(20,6)")]
    public decimal? TransactionAmount { get; init; }

    /// <summary>Column: exchange_rate; SQL: decimal(20,10); nullable. Rate used to convert transaction_amount into book currency</summary>
    [Column("exchange_rate", TypeName = "decimal(20,10)")]
    public decimal? ExchangeRate { get; init; }

    /// <summary>Column: tax_code_id; SQL: bigint unsigned; nullable.</summary>
    [Column("tax_code_id", TypeName = "bigint unsigned")]
    public ulong? TaxCodeId { get; init; }

    /// <summary>Column: cost_center_id; SQL: bigint unsigned; nullable.</summary>
    [Column("cost_center_id", TypeName = "bigint unsigned")]
    public ulong? CostCenterId { get; init; }

    /// <summary>Column: project_id; SQL: bigint unsigned; nullable.</summary>
    [Column("project_id", TypeName = "bigint unsigned")]
    public ulong? ProjectId { get; init; }

    /// <summary>Column: due_date; SQL: date; nullable. Open-item due date for receivable/payable control-account lines</summary>
    [Column("due_date", TypeName = "date")]
    public DateOnly? DueDate { get; init; }

    /// <summary>Column: source_line_type; SQL: varchar(32); nullable. INVOICE_LINE, CUSTOMS_ASSESSMENT, RECEIPT_LINE, or another controlled source</summary>
    [Column("source_line_type", TypeName = "varchar(32)")]
    public string? SourceLineType { get; init; }

    /// <summary>Column: source_line_id; SQL: bigint unsigned; nullable. Source row ID resolved through source_line_type</summary>
    [Column("source_line_id", TypeName = "bigint unsigned")]
    public ulong? SourceLineId { get; init; }
}
