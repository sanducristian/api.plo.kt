// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Accounting;

/// <summary>Maps one row of the acc_journal_entry table. Includes all 21 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("acc_journal_entry")]
public sealed record JournalEntry
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID exposed by APIs</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: journal_id; SQL: bigint unsigned; not null.</summary>
    [Column("journal_id", TypeName = "bigint unsigned")]
    public required ulong JournalId { get; init; }

    /// <summary>Column: accounting_period_id; SQL: bigint unsigned; nullable. Set before posting; the referenced period must be open</summary>
    [Column("accounting_period_id", TypeName = "bigint unsigned")]
    public ulong? AccountingPeriodId { get; init; }

    /// <summary>Column: entry_number; SQL: varchar(64); nullable. Immutable journal-specific number allocated when the entry is posted</summary>
    [Column("entry_number", TypeName = "varchar(64)")]
    public string? EntryNumber { get; init; }

    /// <summary>Column: entry_date; SQL: date; not null. Accounting date used for ledger and period selection</summary>
    [Column("entry_date", TypeName = "date")]
    public required DateOnly EntryDate { get; init; }

    /// <summary>Column: document_date; SQL: date; nullable. Underlying invoice, customs, receipt, or source-document date</summary>
    [Column("document_date", TypeName = "date")]
    public DateOnly? DocumentDate { get; init; }

    /// <summary>Column: description; SQL: varchar(512); not null.</summary>
    [Column("description", TypeName = "varchar(512)")]
    public required string Description { get; init; }

    /// <summary>Column: source_document_type; SQL: varchar(32); nullable. INVOICE, CUSTOMS, RECEIPT, MANUAL, MIGRATION, or another controlled source type</summary>
    [Column("source_document_type", TypeName = "varchar(32)")]
    public string? SourceDocumentType { get; init; }

    /// <summary>Column: source_document_id; SQL: bigint unsigned; nullable. ID resolved by the module identified in source_document_type</summary>
    [Column("source_document_id", TypeName = "bigint unsigned")]
    public ulong? SourceDocumentId { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null. DRAFT, VALIDATED, POSTED, REVERSED, or VOID</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: book_currency_id; SQL: bigint unsigned; not null. Legal entity book currency used for debit and credit totals</summary>
    [Column("book_currency_id", TypeName = "bigint unsigned")]
    public required ulong BookCurrencyId { get; init; }

    /// <summary>Column: total_debit; SQL: decimal(20,6); not null. Cached book-currency debit total verified again during posting</summary>
    [Column("total_debit", TypeName = "decimal(20,6)")]
    public required decimal TotalDebit { get; init; }

    /// <summary>Column: total_credit; SQL: decimal(20,6); not null. Cached book-currency credit total verified again during posting</summary>
    [Column("total_credit", TypeName = "decimal(20,6)")]
    public required decimal TotalCredit { get; init; }

    /// <summary>Column: reversal_of_entry_id; SQL: bigint unsigned; nullable. Original posted entry reversed by this entry</summary>
    [Column("reversal_of_entry_id", TypeName = "bigint unsigned")]
    public ulong? ReversalOfEntryId { get; init; }

    /// <summary>Column: posted_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("posted_at_utc", TypeName = "datetime(6)")]
    public DateTime? PostedAtUtc { get; init; }

    /// <summary>Column: posted_by_user_id; SQL: bigint unsigned; nullable. New Identity-module user ID; never a legacy MySQL account ID</summary>
    [Column("posted_by_user_id", TypeName = "bigint unsigned")]
    public ulong? PostedByUserId { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null. New Identity-module user ID</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }

    /// <summary>Column: updated_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_at_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedAtUtc { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
