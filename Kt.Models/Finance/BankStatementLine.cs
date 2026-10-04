// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_bank_statement_line table. Includes all 24 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_bank_statement_line")]
public sealed record BankStatementLine
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: bank_statement_import_id; SQL: bigint unsigned; not null.</summary>
    [Column("bank_statement_import_id", TypeName = "bigint unsigned")]
    public required ulong BankStatementImportId { get; init; }

    /// <summary>Column: line_number; SQL: int unsigned; not null. Stable sequence within the imported statement</summary>
    [Column("line_number", TypeName = "int unsigned")]
    public required uint LineNumber { get; init; }

    /// <summary>Column: external_transaction_id; SQL: varchar(256); nullable. Bank/connector transaction identifier when available</summary>
    [Column("external_transaction_id", TypeName = "varchar(256)")]
    public string? ExternalTransactionId { get; init; }

    /// <summary>Column: duplicate_fingerprint; SQL: binary(32); not null. Deterministic keyed hash of account/date/amount/reference fields for duplicate detection</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("duplicate_fingerprint", TypeName = "binary(32)")]
    public required byte[] DuplicateFingerprint { get; init; }

    /// <summary>Column: booking_date; SQL: date; not null. Date the bank booked the transaction to the account</summary>
    [Column("booking_date", TypeName = "date")]
    public required DateOnly BookingDate { get; init; }

    /// <summary>Column: value_date; SQL: date; nullable. Interest/value date reported by the bank</summary>
    [Column("value_date", TypeName = "date")]
    public DateOnly? ValueDate { get; init; }

    /// <summary>Column: amount; SQL: decimal(20,6); not null. Signed account-currency amount: positive increases the operating account, negative decreases it</summary>
    [Column("amount", TypeName = "decimal(20,6)")]
    public required decimal Amount { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; not null.</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public required ulong CurrencyId { get; init; }

    /// <summary>Column: debit_credit_indicator; SQL: varchar(8); not null. CREDIT or DEBIT as reported/normalized from the bank statement</summary>
    [Column("debit_credit_indicator", TypeName = "varchar(8)")]
    public required string DebitCreditIndicator { get; init; }

    /// <summary>Column: bank_transaction_code; SQL: varchar(64); nullable. ISO 20022 or bank-specific transaction classification</summary>
    [Column("bank_transaction_code", TypeName = "varchar(64)")]
    public string? BankTransactionCode { get; init; }

    /// <summary>Column: counterparty_party_id; SQL: bigint unsigned; nullable. Matched PLO party; NULL until identified</summary>
    [Column("counterparty_party_id", TypeName = "bigint unsigned")]
    public ulong? CounterpartyPartyId { get; init; }

    /// <summary>Column: counterparty_name; SQL: varchar(256); nullable. Counterparty name exactly as reported by the bank</summary>
    [Column("counterparty_name", TypeName = "varchar(256)")]
    public string? CounterpartyName { get; init; }

    /// <summary>Column: counterparty_iban_ciphertext; SQL: varbinary(512); nullable. Encrypted counterparty IBAN from the statement</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("counterparty_iban_ciphertext", TypeName = "varbinary(512)")]
    public byte[]? CounterpartyIbanCiphertext { get; init; }

    /// <summary>Column: counterparty_iban_hash; SQL: binary(32); nullable. Keyed normalized comparison value used for party/account matching</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("counterparty_iban_hash", TypeName = "binary(32)")]
    public byte[]? CounterpartyIbanHash { get; init; }

    /// <summary>Column: counterparty_iban_masked; SQL: varchar(64); nullable. Safe display form of the counterparty IBAN</summary>
    [Column("counterparty_iban_masked", TypeName = "varchar(64)")]
    public string? CounterpartyIbanMasked { get; init; }

    /// <summary>Column: counterparty_bic; SQL: varchar(11); nullable.</summary>
    [Column("counterparty_bic", TypeName = "varchar(11)")]
    public string? CounterpartyBic { get; init; }

    /// <summary>Column: end_to_end_reference; SQL: varchar(256); nullable. Payer/payee end-to-end identifier, including structured payment references</summary>
    [Column("end_to_end_reference", TypeName = "varchar(256)")]
    public string? EndToEndReference { get; init; }

    /// <summary>Column: bank_reference; SQL: varchar(256); nullable. Bank-assigned entry, transaction, or reconciliation reference</summary>
    [Column("bank_reference", TypeName = "varchar(256)")]
    public string? BankReference { get; init; }

    /// <summary>Column: remittance_information; SQL: varchar(2048); nullable. Unstructured or flattened structured remittance text used for matching</summary>
    [Column("remittance_information", TypeName = "varchar(2048)")]
    public string? RemittanceInformation { get; init; }

    /// <summary>Column: raw_line_json; SQL: json; nullable. Normalized source payload retained for unsupported bank-specific fields</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("raw_line_json", TypeName = "json")]
    public string? RawLineJson { get; init; }

    /// <summary>Column: reconciliation_status; SQL: varchar(24); not null. UNMATCHED, SUGGESTED, PARTIAL, MATCHED, EXCLUDED, or REVERSED</summary>
    [Column("reconciliation_status", TypeName = "varchar(24)")]
    public required string ReconciliationStatus { get; init; }

    /// <summary>Column: journal_entry_id; SQL: bigint unsigned; nullable. Balanced BANK journal entry created from this imported transaction when posted</summary>
    [Column("journal_entry_id", TypeName = "bigint unsigned")]
    public ulong? JournalEntryId { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }
}
