// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_cost_allocation table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_cost_allocation")]
public sealed record CostAllocation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: tariff_assessment_id; SQL: bigint unsigned; not null.</summary>
    [Column("tariff_assessment_id", TypeName = "bigint unsigned")]
    public required ulong TariffAssessmentId { get; init; }

    /// <summary>Column: goods_receipt_line_id; SQL: bigint unsigned; nullable. Receipt line whose landed cost receives this allocation</summary>
    [Column("goods_receipt_line_id", TypeName = "bigint unsigned")]
    public ulong? GoodsReceiptLineId { get; init; }

    /// <summary>Column: invoice_line_id; SQL: bigint unsigned; nullable. Commercial invoice line used as an allocation target or basis</summary>
    [Column("invoice_line_id", TypeName = "bigint unsigned")]
    public ulong? InvoiceLineId { get; init; }

    /// <summary>Column: tracked_item_id; SQL: bigint unsigned; nullable. Optional cross-module physical-unit ID for unit-level landed cost</summary>
    [Column("tracked_item_id", TypeName = "bigint unsigned")]
    public ulong? TrackedItemId { get; init; }

    /// <summary>Column: journal_entry_line_id; SQL: bigint unsigned; nullable. Posted accounting line recognizing the allocated duty/tax/cost</summary>
    [Column("journal_entry_line_id", TypeName = "bigint unsigned")]
    public ulong? JournalEntryLineId { get; init; }

    /// <summary>Column: allocation_treatment; SQL: varchar(32); not null. INVENTORY_COST, EXPENSE, RECOVERABLE_TAX, NONRECOVERABLE_TAX, or MEMO_ONLY</summary>
    [Column("allocation_treatment", TypeName = "varchar(32)")]
    public required string AllocationTreatment { get; init; }

    /// <summary>Column: allocation_basis; SQL: varchar(24); not null. VALUE, QUANTITY, WEIGHT, VOLUME, MANUAL, or OTHER</summary>
    [Column("allocation_basis", TypeName = "varchar(24)")]
    public required string AllocationBasis { get; init; }

    /// <summary>Column: allocated_amount; SQL: decimal(20,6); not null. Portion of the customs assessment assigned to this target</summary>
    [Column("allocated_amount", TypeName = "decimal(20,6)")]
    public required decimal AllocatedAmount { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; not null.</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public required ulong CurrencyId { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null. New Identity-module user ID</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }
}
