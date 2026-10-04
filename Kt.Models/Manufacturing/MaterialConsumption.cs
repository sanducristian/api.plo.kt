// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Manufacturing;

/// <summary>Maps one row of the mfg_material_consumption table. Includes all 16 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("mfg_material_consumption")]
public sealed record MaterialConsumption
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: work_order_id; SQL: bigint unsigned; not null.</summary>
    [Column("work_order_id", TypeName = "bigint unsigned")]
    public required ulong WorkOrderId { get; init; }

    /// <summary>Column: work_order_operation_id; SQL: bigint unsigned; nullable.</summary>
    [Column("work_order_operation_id", TypeName = "bigint unsigned")]
    public ulong? WorkOrderOperationId { get; init; }

    /// <summary>Column: bom_item_id; SQL: bigint unsigned; nullable.</summary>
    [Column("bom_item_id", TypeName = "bigint unsigned")]
    public ulong? BomItemId { get; init; }

    /// <summary>Column: component_id; SQL: bigint unsigned; not null. References modern comp_component or current component</summary>
    [Column("component_id", TypeName = "bigint unsigned")]
    public required ulong ComponentId { get; init; }

    /// <summary>Column: component_unit_id; SQL: bigint unsigned; nullable. Required by application for SERIAL traceability</summary>
    [Column("component_unit_id", TypeName = "bigint unsigned")]
    public ulong? ComponentUnitId { get; init; }

    /// <summary>Column: device_unit_id; SQL: bigint unsigned; nullable. Specific produced unit receiving this material when known</summary>
    [Column("device_unit_id", TypeName = "bigint unsigned")]
    public ulong? DeviceUnitId { get; init; }

    /// <summary>Column: event_type; SQL: varchar(16); not null.</summary>
    [Column("event_type", TypeName = "varchar(16)")]
    public required string EventType { get; init; }

    /// <summary>Column: quantity; SQL: decimal(20,6); not null.</summary>
    [Column("quantity", TypeName = "decimal(20,6)")]
    public required decimal Quantity { get; init; }

    /// <summary>Column: unit_id; SQL: bigint unsigned; nullable.</summary>
    [Column("unit_id", TypeName = "bigint unsigned")]
    public ulong? UnitId { get; init; }

    /// <summary>Column: lot_reference; SQL: varchar(128); nullable.</summary>
    [Column("lot_reference", TypeName = "varchar(128)")]
    public string? LotReference { get; init; }

    /// <summary>Column: occurred_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_at_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredAtUtc { get; init; }

    /// <summary>Column: recorded_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("recorded_at_utc", TypeName = "datetime(6)")]
    public required DateTime RecordedAtUtc { get; init; }

    /// <summary>Column: actor_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("actor_principal_id", TypeName = "bigint unsigned")]
    public ulong? ActorPrincipalId { get; init; }

    /// <summary>Column: reason_code; SQL: varchar(64); nullable.</summary>
    [Column("reason_code", TypeName = "varchar(64)")]
    public string? ReasonCode { get; init; }
}
