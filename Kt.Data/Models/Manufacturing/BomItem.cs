// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Manufacturing;

/// <summary>Maps one row of the mfg_bom_item table. Includes all 15 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("mfg_bom_item")]
public sealed record BomItem
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: bom_id; SQL: bigint unsigned; not null.</summary>
    [Column("bom_id", TypeName = "bigint unsigned")]
    public required ulong BomId { get; init; }

    /// <summary>Column: line_number; SQL: int unsigned; not null.</summary>
    [Column("line_number", TypeName = "int unsigned")]
    public required uint LineNumber { get; init; }

    /// <summary>Column: parent_bom_item_id; SQL: bigint unsigned; nullable.</summary>
    [Column("parent_bom_item_id", TypeName = "bigint unsigned")]
    public ulong? ParentBomItemId { get; init; }

    /// <summary>Column: item_type; SQL: varchar(16); not null.</summary>
    [Column("item_type", TypeName = "varchar(16)")]
    public required string ItemType { get; init; }

    /// <summary>Column: component_id; SQL: bigint unsigned; nullable. References modern comp_component or current component</summary>
    [Column("component_id", TypeName = "bigint unsigned")]
    public ulong? ComponentId { get; init; }

    /// <summary>Column: subassembly_device_model_id; SQL: bigint unsigned; nullable.</summary>
    [Column("subassembly_device_model_id", TypeName = "bigint unsigned")]
    public ulong? SubassemblyDeviceModelId { get; init; }

    /// <summary>Column: quantity_required; SQL: decimal(20,6); not null.</summary>
    [Column("quantity_required", TypeName = "decimal(20,6)")]
    public required decimal QuantityRequired { get; init; }

    /// <summary>Column: unit_id; SQL: bigint unsigned; nullable.</summary>
    [Column("unit_id", TypeName = "bigint unsigned")]
    public ulong? UnitId { get; init; }

    /// <summary>Column: scrap_factor_percent; SQL: decimal(9,6); not null.</summary>
    [Column("scrap_factor_percent", TypeName = "decimal(9,6)")]
    public required decimal ScrapFactorPercent { get; init; }

    /// <summary>Column: traceability_level; SQL: varchar(16); not null.</summary>
    [Column("traceability_level", TypeName = "varchar(16)")]
    public required string TraceabilityLevel { get; init; }

    /// <summary>Column: position_code; SQL: varchar(64); nullable.</summary>
    [Column("position_code", TypeName = "varchar(64)")]
    public string? PositionCode { get; init; }

    /// <summary>Column: substitute_group_code; SQL: varchar(64); nullable.</summary>
    [Column("substitute_group_code", TypeName = "varchar(64)")]
    public string? SubstituteGroupCode { get; init; }

    /// <summary>Column: is_optional; SQL: tinyint(1); not null.</summary>
    [Column("is_optional", TypeName = "tinyint(1)")]
    public required bool IsOptional { get; init; }

    /// <summary>Column: notes; SQL: varchar(1024); nullable.</summary>
    [Column("notes", TypeName = "varchar(1024)")]
    public string? Notes { get; init; }
}
