// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_document_sequence table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_document_sequence")]
public sealed record DocumentSequence
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: country_pack_id; SQL: bigint unsigned; nullable. Jurisdiction policy governing this sequence, when applicable</summary>
    [Column("country_pack_id", TypeName = "bigint unsigned")]
    public ulong? CountryPackId { get; init; }

    /// <summary>Column: sequence_code; SQL: varchar(64); not null. Stable application code for the sequence</summary>
    [Column("sequence_code", TypeName = "varchar(64)")]
    public required string SequenceCode { get; init; }

    /// <summary>Column: document_type; SQL: varchar(32); not null. PURCHASE_INVOICE, PURCHASE_CREDIT_NOTE, SALES_INVOICE, or SALES_CREDIT_NOTE</summary>
    [Column("document_type", TypeName = "varchar(32)")]
    public required string DocumentType { get; init; }

    /// <summary>Column: series_prefix; SQL: varchar(32); nullable. Optional legal series/prefix included in formatted numbers</summary>
    [Column("series_prefix", TypeName = "varchar(32)")]
    public string? SeriesPrefix { get; init; }

    /// <summary>Column: period_key; SQL: varchar(16); not null. Calendar/fiscal bucket such as 2026 or 2026-09 used when sequences reset</summary>
    [Column("period_key", TypeName = "varchar(16)")]
    public required string PeriodKey { get; init; }

    /// <summary>Column: next_number; SQL: bigint unsigned; not null. Allocated atomically only when the relevant workflow commits</summary>
    [Column("next_number", TypeName = "bigint unsigned")]
    public required ulong NextNumber { get; init; }

    /// <summary>Column: minimum_width; SQL: tinyint unsigned; not null. Zero-padding width for the numeric portion</summary>
    [Column("minimum_width", TypeName = "tinyint unsigned")]
    public required byte MinimumWidth { get; init; }

    /// <summary>Column: valid_from; SQL: date; not null.</summary>
    [Column("valid_from", TypeName = "date")]
    public required DateOnly ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable.</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }
}
