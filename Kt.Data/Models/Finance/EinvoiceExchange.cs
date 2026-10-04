// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_einvoice_exchange table. Includes all 18 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_einvoice_exchange")]
public sealed record EinvoiceExchange
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceId { get; init; }

    /// <summary>Column: einvoice_profile_id; SQL: bigint unsigned; not null. Country/network/profile version used for this exchange attempt</summary>
    [Column("einvoice_profile_id", TypeName = "bigint unsigned")]
    public required ulong EinvoiceProfileId { get; init; }

    /// <summary>Column: direction_code; SQL: varchar(16); not null. INBOUND or OUTBOUND</summary>
    [Column("direction_code", TypeName = "varchar(16)")]
    public required string DirectionCode { get; init; }

    /// <summary>Column: structured_file_id; SQL: bigint unsigned; not null. Original or generated structured legal invoice file in objfile</summary>
    [Column("structured_file_id", TypeName = "bigint unsigned")]
    public required ulong StructuredFileId { get; init; }

    /// <summary>Column: rendered_file_id; SQL: bigint unsigned; nullable. Optional human-readable PDF rendering stored separately from structured data</summary>
    [Column("rendered_file_id", TypeName = "bigint unsigned")]
    public ulong? RenderedFileId { get; init; }

    /// <summary>Column: external_envelope_id; SQL: varchar(256); nullable. Network/platform message, envelope, or transmission identifier</summary>
    [Column("external_envelope_id", TypeName = "varchar(256)")]
    public string? ExternalEnvelopeId { get; init; }

    /// <summary>Column: external_document_id; SQL: varchar(256); nullable. Invoice identifier assigned by the country platform or provider</summary>
    [Column("external_document_id", TypeName = "varchar(256)")]
    public string? ExternalDocumentId { get; init; }

    /// <summary>Column: status_code; SQL: varchar(32); not null. CREATED, VALIDATING, READY, SUBMITTED, DELIVERED, ACCEPTED, REJECTED, CANCELLED, or FAILED</summary>
    [Column("status_code", TypeName = "varchar(32)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: submitted_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("submitted_at_utc", TypeName = "datetime(6)")]
    public DateTime? SubmittedAtUtc { get; init; }

    /// <summary>Column: delivered_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("delivered_at_utc", TypeName = "datetime(6)")]
    public DateTime? DeliveredAtUtc { get; init; }

    /// <summary>Column: acknowledged_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("acknowledged_at_utc", TypeName = "datetime(6)")]
    public DateTime? AcknowledgedAtUtc { get; init; }

    /// <summary>Column: response_code; SQL: varchar(128); nullable. Country platform/provider acknowledgement or rejection code</summary>
    [Column("response_code", TypeName = "varchar(128)")]
    public string? ResponseCode { get; init; }

    /// <summary>Column: response_message; SQL: varchar(2048); nullable.</summary>
    [Column("response_message", TypeName = "varchar(2048)")]
    public string? ResponseMessage { get; init; }

    /// <summary>Column: response_file_id; SQL: bigint unsigned; nullable. Machine-readable acknowledgement, rejection, or validation report</summary>
    [Column("response_file_id", TypeName = "bigint unsigned")]
    public ulong? ResponseFileId { get; init; }

    /// <summary>Column: api_reference; SQL: varchar(512); nullable. Stable external API resource identifier; never a bearer token or expiring URL</summary>
    [Column("api_reference", TypeName = "varchar(512)")]
    public string? ApiReference { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null. New Identity-module user ID or reserved system identity</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }
}
