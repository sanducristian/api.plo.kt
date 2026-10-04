// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Fleet;

/// <summary>Maps one row of the fleet_battery table. Includes all 14 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("fleet_battery")]
public sealed record FleetBattery
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

    /// <summary>Column: serial_number; SQL: varchar(128); not null.</summary>
    [Column("serial_number", TypeName = "varchar(128)")]
    public required string SerialNumber { get; init; }

    /// <summary>Column: manufacturer_party_id; SQL: bigint unsigned; nullable.</summary>
    [Column("manufacturer_party_id", TypeName = "bigint unsigned")]
    public ulong? ManufacturerPartyId { get; init; }

    /// <summary>Column: model_name; SQL: varchar(128); nullable.</summary>
    [Column("model_name", TypeName = "varchar(128)")]
    public string? ModelName { get; init; }

    /// <summary>Column: chemistry_code; SQL: varchar(32); nullable.</summary>
    [Column("chemistry_code", TypeName = "varchar(32)")]
    public string? ChemistryCode { get; init; }

    /// <summary>Column: nominal_capacity_wh; SQL: decimal(20,6); not null.</summary>
    [Column("nominal_capacity_wh", TypeName = "decimal(20,6)")]
    public required decimal NominalCapacityWh { get; init; }

    /// <summary>Column: nominal_voltage_v; SQL: decimal(12,6); nullable.</summary>
    [Column("nominal_voltage_v", TypeName = "decimal(12,6)")]
    public decimal? NominalVoltageV { get; init; }

    /// <summary>Column: nominal_capacity_ah; SQL: decimal(20,6); nullable.</summary>
    [Column("nominal_capacity_ah", TypeName = "decimal(20,6)")]
    public decimal? NominalCapacityAh { get; init; }

    /// <summary>Column: commissioned_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("commissioned_at_utc", TypeName = "datetime(6)")]
    public DateTime? CommissionedAtUtc { get; init; }

    /// <summary>Column: retired_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("retired_at_utc", TypeName = "datetime(6)")]
    public DateTime? RetiredAtUtc { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }
}
