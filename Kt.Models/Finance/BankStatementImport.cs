// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_bank_statement_import table. Includes all 22 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_bank_statement_import")]
public sealed record BankStatementImport
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID exposed by APIs</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: legal_entity_bank_account_id; SQL: bigint unsigned; not null. Configured operating account whose statement is imported</summary>
    [Column("legal_entity_bank_account_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityBankAccountId { get; init; }

    /// <summary>Column: source_file_id; SQL: bigint unsigned; nullable. Original CAMT, MT940, CSV, OFX, JSON, PDF, or other statement file in objfile</summary>
    [Column("source_file_id", TypeName = "bigint unsigned")]
    public ulong? SourceFileId { get; init; }

    /// <summary>Column: source_channel; SQL: varchar(24); not null. FILE_UPLOAD, BANK_API, OPEN_BANKING, MANUAL, or LEGACY_IMPORT</summary>
    [Column("source_channel", TypeName = "varchar(24)")]
    public required string SourceChannel { get; init; }

    /// <summary>Column: format_code; SQL: varchar(24); not null. CAMT_053, CAMT_054, MT940, BAI2, OFX, CSV, JSON, API_NATIVE, or OTHER</summary>
    [Column("format_code", TypeName = "varchar(24)")]
    public required string FormatCode { get; init; }

    /// <summary>Column: external_statement_id; SQL: varchar(256); nullable. Statement/message identifier assigned by the bank or connector</summary>
    [Column("external_statement_id", TypeName = "varchar(256)")]
    public string? ExternalStatementId { get; init; }

    /// <summary>Column: idempotency_key; SQL: varchar(128); nullable. Caller/connector retry key preventing duplicate ingestion</summary>
    [Column("idempotency_key", TypeName = "varchar(128)")]
    public string? IdempotencyKey { get; init; }

    /// <summary>Column: document_sha256; SQL: binary(32); nullable. Hash of the original statement bytes used for duplicate detection</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("document_sha256", TypeName = "binary(32)")]
    public byte[]? DocumentSha256 { get; init; }

    /// <summary>Column: period_start_date; SQL: date; nullable.</summary>
    [Column("period_start_date", TypeName = "date")]
    public DateOnly? PeriodStartDate { get; init; }

    /// <summary>Column: period_end_date; SQL: date; nullable.</summary>
    [Column("period_end_date", TypeName = "date")]
    public DateOnly? PeriodEndDate { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; not null. Currency of opening, closing, and available balances for this statement</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public required ulong CurrencyId { get; init; }

    /// <summary>Column: opening_balance; SQL: decimal(20,6); nullable. Opening booked balance in the configured account currency</summary>
    [Column("opening_balance", TypeName = "decimal(20,6)")]
    public decimal? OpeningBalance { get; init; }

    /// <summary>Column: closing_balance; SQL: decimal(20,6); nullable. Closing booked balance in the configured account currency</summary>
    [Column("closing_balance", TypeName = "decimal(20,6)")]
    public decimal? ClosingBalance { get; init; }

    /// <summary>Column: available_closing_balance; SQL: decimal(20,6); nullable. Optional available balance separately reported by the bank</summary>
    [Column("available_closing_balance", TypeName = "decimal(20,6)")]
    public decimal? AvailableClosingBalance { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null. RECEIVED, PARSING, IMPORTED, PARTIAL, FAILED, or REVERSED</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: raw_metadata_json; SQL: json; nullable. Non-secret message metadata and parsing diagnostics retained for reproducibility</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("raw_metadata_json", TypeName = "json")]
    public string? RawMetadataJson { get; init; }

    /// <summary>Column: failure_code; SQL: varchar(64); nullable.</summary>
    [Column("failure_code", TypeName = "varchar(64)")]
    public string? FailureCode { get; init; }

    /// <summary>Column: failure_message; SQL: varchar(1024); nullable.</summary>
    [Column("failure_message", TypeName = "varchar(1024)")]
    public string? FailureMessage { get; init; }

    /// <summary>Column: received_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("received_at_utc", TypeName = "datetime(6)")]
    public required DateTime ReceivedAtUtc { get; init; }

    /// <summary>Column: completed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("completed_at_utc", TypeName = "datetime(6)")]
    public DateTime? CompletedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; nullable. New Identity-module user ID or reserved connector identity</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public ulong? CreatedByUserId { get; init; }
}
