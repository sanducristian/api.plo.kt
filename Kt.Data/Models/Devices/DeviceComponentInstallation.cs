// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_component_installation table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_component_installation")]
public sealed record DeviceComponentInstallation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: device_unit_id; SQL: bigint unsigned; not null.</summary>
    [Column("device_unit_id", TypeName = "bigint unsigned")]
    public required ulong DeviceUnitId { get; init; }

    /// <summary>Column: component_unit_id; SQL: bigint unsigned; not null. References modern comp_component_unit or current componentinitem</summary>
    [Column("component_unit_id", TypeName = "bigint unsigned")]
    public required ulong ComponentUnitId { get; init; }

    /// <summary>Column: source_consumption_id; SQL: bigint unsigned; nullable.</summary>
    [Column("source_consumption_id", TypeName = "bigint unsigned")]
    public ulong? SourceConsumptionId { get; init; }

    /// <summary>Column: position_code; SQL: varchar(64); nullable.</summary>
    [Column("position_code", TypeName = "varchar(64)")]
    public string? PositionCode { get; init; }

    /// <summary>Column: installed_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("installed_at_utc", TypeName = "datetime(6)")]
    public required DateTime InstalledAtUtc { get; init; }

    /// <summary>Column: removed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("removed_at_utc", TypeName = "datetime(6)")]
    public DateTime? RemovedAtUtc { get; init; }

    /// <summary>Column: installed_by_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("installed_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? InstalledByPrincipalId { get; init; }

    /// <summary>Column: removed_by_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("removed_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? RemovedByPrincipalId { get; init; }

    /// <summary>Column: removal_reason_code; SQL: varchar(64); nullable.</summary>
    [Column("removal_reason_code", TypeName = "varchar(64)")]
    public string? RemovalReasonCode { get; init; }

    /// <summary>Column: active_component_unit_id; SQL: bigint unsigned; nullable.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_component_unit_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveComponentUnitId { get; init; }
}
