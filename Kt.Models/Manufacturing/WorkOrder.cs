// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Manufacturing;

/// <summary>Maps one row of the mfg_work_order table. Includes all 22 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("mfg_work_order")]
public sealed record WorkOrder
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: manufacturing_workspace_id; SQL: bigint unsigned; nullable.</summary>
    [Column("manufacturing_workspace_id", TypeName = "bigint unsigned")]
    public ulong? ManufacturingWorkspaceId { get; init; }

    /// <summary>Column: work_order_number; SQL: varchar(64); not null.</summary>
    [Column("work_order_number", TypeName = "varchar(64)")]
    public required string WorkOrderNumber { get; init; }

    /// <summary>Column: device_model_id; SQL: bigint unsigned; not null.</summary>
    [Column("device_model_id", TypeName = "bigint unsigned")]
    public required ulong DeviceModelId { get; init; }

    /// <summary>Column: bom_id; SQL: bigint unsigned; not null.</summary>
    [Column("bom_id", TypeName = "bigint unsigned")]
    public required ulong BomId { get; init; }

    /// <summary>Column: routing_id; SQL: bigint unsigned; nullable.</summary>
    [Column("routing_id", TypeName = "bigint unsigned")]
    public ulong? RoutingId { get; init; }

    /// <summary>Column: planned_quantity; SQL: decimal(20,6); not null.</summary>
    [Column("planned_quantity", TypeName = "decimal(20,6)")]
    public required decimal PlannedQuantity { get; init; }

    /// <summary>Column: completed_quantity; SQL: decimal(20,6); not null.</summary>
    [Column("completed_quantity", TypeName = "decimal(20,6)")]
    public required decimal CompletedQuantity { get; init; }

    /// <summary>Column: scrapped_quantity; SQL: decimal(20,6); not null.</summary>
    [Column("scrapped_quantity", TypeName = "decimal(20,6)")]
    public required decimal ScrappedQuantity { get; init; }

    /// <summary>Column: unit_id; SQL: bigint unsigned; nullable.</summary>
    [Column("unit_id", TypeName = "bigint unsigned")]
    public ulong? UnitId { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null.</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: priority_code; SQL: varchar(16); not null.</summary>
    [Column("priority_code", TypeName = "varchar(16)")]
    public required string PriorityCode { get; init; }

    /// <summary>Column: planned_start_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("planned_start_at_utc", TypeName = "datetime(6)")]
    public DateTime? PlannedStartAtUtc { get; init; }

    /// <summary>Column: planned_end_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("planned_end_at_utc", TypeName = "datetime(6)")]
    public DateTime? PlannedEndAtUtc { get; init; }

    /// <summary>Column: actual_start_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("actual_start_at_utc", TypeName = "datetime(6)")]
    public DateTime? ActualStartAtUtc { get; init; }

    /// <summary>Column: actual_end_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("actual_end_at_utc", TypeName = "datetime(6)")]
    public DateTime? ActualEndAtUtc { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? CreatedByPrincipalId { get; init; }

    /// <summary>Column: updated_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_at_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedAtUtc { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
