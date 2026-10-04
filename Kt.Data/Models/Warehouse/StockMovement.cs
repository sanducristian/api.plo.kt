// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Warehouse;

/// <summary>Maps one row of the wh_stock_movement table. Includes all 20 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("wh_stock_movement")]
public sealed record StockMovement
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: product_id; SQL: bigint unsigned; not null. Current component master; modern product migration can replace the FK later</summary>
    [Column("product_id", TypeName = "bigint unsigned")]
    public required ulong ProductId { get; init; }

    /// <summary>Column: tracked_item_id; SQL: bigint unsigned; nullable.</summary>
    [Column("tracked_item_id", TypeName = "bigint unsigned")]
    public ulong? TrackedItemId { get; init; }

    /// <summary>Column: lot_reference; SQL: varchar(128); nullable.</summary>
    [Column("lot_reference", TypeName = "varchar(128)")]
    public string? LotReference { get; init; }

    /// <summary>Column: handling_unit_id; SQL: bigint unsigned; nullable.</summary>
    [Column("handling_unit_id", TypeName = "bigint unsigned")]
    public ulong? HandlingUnitId { get; init; }

    /// <summary>Column: owner_party_id; SQL: bigint unsigned; not null.</summary>
    [Column("owner_party_id", TypeName = "bigint unsigned")]
    public required ulong OwnerPartyId { get; init; }

    /// <summary>Column: from_location_id; SQL: bigint unsigned; nullable.</summary>
    [Column("from_location_id", TypeName = "bigint unsigned")]
    public ulong? FromLocationId { get; init; }

    /// <summary>Column: to_location_id; SQL: bigint unsigned; nullable.</summary>
    [Column("to_location_id", TypeName = "bigint unsigned")]
    public ulong? ToLocationId { get; init; }

    /// <summary>Column: quantity; SQL: decimal(20,6); not null.</summary>
    [Column("quantity", TypeName = "decimal(20,6)")]
    public required decimal Quantity { get; init; }

    /// <summary>Column: unit_id; SQL: bigint unsigned; not null.</summary>
    [Column("unit_id", TypeName = "bigint unsigned")]
    public required ulong UnitId { get; init; }

    /// <summary>Column: movement_type; SQL: varchar(24); not null.</summary>
    [Column("movement_type", TypeName = "varchar(24)")]
    public required string MovementType { get; init; }

    /// <summary>Column: occurred_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_at_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredAtUtc { get; init; }

    /// <summary>Column: source_event_key; SQL: varchar(128); not null.</summary>
    [Column("source_event_key", TypeName = "varchar(128)")]
    public required string SourceEventKey { get; init; }

    /// <summary>Column: transport_event_id; SQL: bigint unsigned; nullable.</summary>
    [Column("transport_event_id", TypeName = "bigint unsigned")]
    public ulong? TransportEventId { get; init; }

    /// <summary>Column: reverses_movement_id; SQL: bigint unsigned; nullable.</summary>
    [Column("reverses_movement_id", TypeName = "bigint unsigned")]
    public ulong? ReversesMovementId { get; init; }

    /// <summary>Column: reason; SQL: varchar(255); nullable.</summary>
    [Column("reason", TypeName = "varchar(255)")]
    public string? Reason { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }
}
