// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_open_item table. Includes all 15 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_open_item")]
public sealed record OpenItem
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID exposed by APIs</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: party_id; SQL: bigint unsigned; not null.</summary>
    [Column("party_id", TypeName = "bigint unsigned")]
    public required ulong PartyId { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceId { get; init; }

    /// <summary>Column: account_class; SQL: varchar(16); not null. RECEIVABLE or PAYABLE</summary>
    [Column("account_class", TypeName = "varchar(16)")]
    public required string AccountClass { get; init; }

    /// <summary>Column: document_side; SQL: varchar(8); not null. DEBIT or CREDIT within the account class</summary>
    [Column("document_side", TypeName = "varchar(8)")]
    public required string DocumentSide { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; not null.</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public required ulong CurrencyId { get; init; }

    /// <summary>Column: original_amount; SQL: decimal(20,6); not null.</summary>
    [Column("original_amount", TypeName = "decimal(20,6)")]
    public required decimal OriginalAmount { get; init; }

    /// <summary>Column: open_amount; SQL: decimal(20,6); not null.</summary>
    [Column("open_amount", TypeName = "decimal(20,6)")]
    public required decimal OpenAmount { get; init; }

    /// <summary>Column: due_date; SQL: date; nullable.</summary>
    [Column("due_date", TypeName = "date")]
    public DateOnly? DueDate { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null.</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: opened_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("opened_at_utc", TypeName = "datetime(6)")]
    public required DateTime OpenedAtUtc { get; init; }

    /// <summary>Column: closed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("closed_at_utc", TypeName = "datetime(6)")]
    public DateTime? ClosedAtUtc { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
