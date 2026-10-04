// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_line_allocation table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_line_allocation")]
public sealed record InvoiceLineAllocation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_line_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_line_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceLineId { get; init; }

    /// <summary>Column: gl_account_id; SQL: bigint unsigned; not null.</summary>
    [Column("gl_account_id", TypeName = "bigint unsigned")]
    public required ulong GlAccountId { get; init; }

    /// <summary>Column: cost_center_id; SQL: bigint unsigned; nullable.</summary>
    [Column("cost_center_id", TypeName = "bigint unsigned")]
    public ulong? CostCenterId { get; init; }

    /// <summary>Column: project_id; SQL: bigint unsigned; nullable.</summary>
    [Column("project_id", TypeName = "bigint unsigned")]
    public ulong? ProjectId { get; init; }

    /// <summary>Column: allocation_percent; SQL: decimal(9,6); nullable.</summary>
    [Column("allocation_percent", TypeName = "decimal(9,6)")]
    public decimal? AllocationPercent { get; init; }

    /// <summary>Column: allocation_amount; SQL: decimal(20,6); not null.</summary>
    [Column("allocation_amount", TypeName = "decimal(20,6)")]
    public required decimal AllocationAmount { get; init; }

    /// <summary>Column: description; SQL: varchar(256); nullable.</summary>
    [Column("description", TypeName = "varchar(256)")]
    public string? Description { get; init; }
}
