// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_document table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_document")]
public sealed record InvoiceDocument
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceId { get; init; }

    /// <summary>Column: file_id; SQL: bigint unsigned; not null. Reference to objfile, which owns the FileDrive/API locator and checksum</summary>
    [Column("file_id", TypeName = "bigint unsigned")]
    public required ulong FileId { get; init; }

    /// <summary>Column: document_role; SQL: varchar(24); not null. Business purpose of this file in the invoice workflow</summary>
    [Column("document_role", TypeName = "varchar(24)")]
    public required string DocumentRole { get; init; }

    /// <summary>Column: is_primary; SQL: tinyint(1); not null. Primary human-readable legal invoice document</summary>
    [Column("is_primary", TypeName = "tinyint(1)")]
    public required bool IsPrimary { get; init; }

    /// <summary>Column: external_document_id; SQL: varchar(128); nullable. Identifier assigned by an e-invoice network, provider, or source system</summary>
    [Column("external_document_id", TypeName = "varchar(128)")]
    public string? ExternalDocumentId { get; init; }

    /// <summary>Column: template_version; SQL: varchar(64); nullable. Template/version used when PLO generated this document</summary>
    [Column("template_version", TypeName = "varchar(64)")]
    public string? TemplateVersion { get; init; }

    /// <summary>Column: added_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("added_at_utc", TypeName = "datetime(6)")]
    public required DateTime AddedAtUtc { get; init; }

    /// <summary>Column: added_by_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("added_by_user_id", TypeName = "bigint unsigned")]
    public required ulong AddedByUserId { get; init; }
}
