// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_payment_allocation table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_payment_allocation")]
public sealed record PaymentAllocation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: settlement_id; SQL: bigint unsigned; not null.</summary>
    [Column("settlement_id", TypeName = "bigint unsigned")]
    public required ulong SettlementId { get; init; }

    /// <summary>Column: open_item_id; SQL: bigint unsigned; not null.</summary>
    [Column("open_item_id", TypeName = "bigint unsigned")]
    public required ulong OpenItemId { get; init; }

    /// <summary>Column: settlement_amount; SQL: decimal(20,6); not null.</summary>
    [Column("settlement_amount", TypeName = "decimal(20,6)")]
    public required decimal SettlementAmount { get; init; }

    /// <summary>Column: open_item_amount; SQL: decimal(20,6); not null.</summary>
    [Column("open_item_amount", TypeName = "decimal(20,6)")]
    public required decimal OpenItemAmount { get; init; }

    /// <summary>Column: exchange_rate; SQL: decimal(20,10); nullable.</summary>
    [Column("exchange_rate", TypeName = "decimal(20,10)")]
    public decimal? ExchangeRate { get; init; }

    /// <summary>Column: allocation_status; SQL: varchar(16); not null.</summary>
    [Column("allocation_status", TypeName = "varchar(16)")]
    public required string AllocationStatus { get; init; }

    /// <summary>Column: reason_code; SQL: varchar(64); nullable.</summary>
    [Column("reason_code", TypeName = "varchar(64)")]
    public string? ReasonCode { get; init; }

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
