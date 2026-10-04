// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_settlement table. Includes all 14 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_settlement")]
public sealed record Settlement
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

    /// <summary>Column: bank_statement_line_id; SQL: bigint unsigned; nullable.</summary>
    [Column("bank_statement_line_id", TypeName = "bigint unsigned")]
    public ulong? BankStatementLineId { get; init; }

    /// <summary>Column: source_type; SQL: varchar(24); not null.</summary>
    [Column("source_type", TypeName = "varchar(24)")]
    public required string SourceType { get; init; }

    /// <summary>Column: settlement_date; SQL: date; not null.</summary>
    [Column("settlement_date", TypeName = "date")]
    public required DateOnly SettlementDate { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; not null.</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public required ulong CurrencyId { get; init; }

    /// <summary>Column: amount; SQL: decimal(20,6); not null.</summary>
    [Column("amount", TypeName = "decimal(20,6)")]
    public required decimal Amount { get; init; }

    /// <summary>Column: status_code; SQL: varchar(16); not null.</summary>
    [Column("status_code", TypeName = "varchar(16)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: external_reference; SQL: varchar(256); nullable.</summary>
    [Column("external_reference", TypeName = "varchar(256)")]
    public string? ExternalReference { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }

    /// <summary>Column: confirmed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("confirmed_at_utc", TypeName = "datetime(6)")]
    public DateTime? ConfirmedAtUtc { get; init; }

    /// <summary>Column: confirmed_by_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("confirmed_by_user_id", TypeName = "bigint unsigned")]
    public ulong? ConfirmedByUserId { get; init; }
}
