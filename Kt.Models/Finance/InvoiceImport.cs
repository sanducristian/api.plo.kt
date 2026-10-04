// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_import table. Includes all 18 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_import")]
public sealed record InvoiceImport
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: source_channel; SQL: varchar(24); not null. MANUAL, UPLOAD, EMAIL, SCAN, API, E_INVOICE, or LEGACY_IMPORT</summary>
    [Column("source_channel", TypeName = "varchar(24)")]
    public required string SourceChannel { get; init; }

    /// <summary>Column: source_reference; SQL: varchar(256); nullable. Mailbox message, scanner job, network envelope, or external source identifier</summary>
    [Column("source_reference", TypeName = "varchar(256)")]
    public string? SourceReference { get; init; }

    /// <summary>Column: source_file_id; SQL: bigint unsigned; nullable. Original captured PDF/image/XML in objfile when a file exists</summary>
    [Column("source_file_id", TypeName = "bigint unsigned")]
    public ulong? SourceFileId { get; init; }

    /// <summary>Column: idempotency_key; SQL: varchar(128); nullable. Caller-supplied retry key that prevents the same intake request being processed twice</summary>
    [Column("idempotency_key", TypeName = "varchar(128)")]
    public string? IdempotencyKey { get; init; }

    /// <summary>Column: document_sha256; SQL: binary(32); nullable. Binary SHA-256 used for probable duplicate detection even before an invoice exists</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("document_sha256", TypeName = "binary(32)")]
    public byte[]? DocumentSha256 { get; init; }

    /// <summary>Column: mime_type; SQL: varchar(128); nullable. Source media type as received; objfile remains authoritative when source_file_id is set</summary>
    [Column("mime_type", TypeName = "varchar(128)")]
    public string? MimeType { get; init; }

    /// <summary>Column: original_file_name; SQL: varchar(256); nullable. Original client/source filename preserved for traceability</summary>
    [Column("original_file_name", TypeName = "varchar(256)")]
    public string? OriginalFileName { get; init; }

    /// <summary>Column: electronic_format; SQL: varchar(32); nullable. Examples: PDF, UBL, CII, FACTUR_X, API_JSON</summary>
    [Column("electronic_format", TypeName = "varchar(32)")]
    public string? ElectronicFormat { get; init; }

    /// <summary>Column: extraction_status; SQL: varchar(24); not null. Machine extraction lifecycle independent from invoice workflow status</summary>
    [Column("extraction_status", TypeName = "varchar(24)")]
    public required string ExtractionStatus { get; init; }

    /// <summary>Column: extracted_data_json; SQL: json; nullable. Raw normalized OCR/e-invoice candidate fields before human confirmation</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("extracted_data_json", TypeName = "json")]
    public string? ExtractedDataJson { get; init; }

    /// <summary>Column: confidence_json; SQL: json; nullable. Per-field confidence and source-location evidence from the extraction engine</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("confidence_json", TypeName = "json")]
    public string? ConfidenceJson { get; init; }

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

    /// <summary>Column: processed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("processed_at_utc", TypeName = "datetime(6)")]
    public DateTime? ProcessedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public ulong? CreatedByUserId { get; init; }
}
