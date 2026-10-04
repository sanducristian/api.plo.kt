// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Fleet;

/// <summary>Maps one row of the fleet_vehicle table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("fleet_vehicle")]
public sealed record FleetVehicle
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Existing device_unit.id; one physical drone/boat/vehicle</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: fleet_code; SQL: varchar(64); not null.</summary>
    [Column("fleet_code", TypeName = "varchar(64)")]
    public required string FleetCode { get; init; }

    /// <summary>Column: operator_party_id; SQL: bigint unsigned; not null.</summary>
    [Column("operator_party_id", TypeName = "bigint unsigned")]
    public required ulong OperatorPartyId { get; init; }

    /// <summary>Column: home_location_id; SQL: bigint unsigned; nullable.</summary>
    [Column("home_location_id", TypeName = "bigint unsigned")]
    public ulong? HomeLocationId { get; init; }

    /// <summary>Column: cargo_hold_location_id; SQL: bigint unsigned; nullable. Mobile core_location used for in-transit stock</summary>
    [Column("cargo_hold_location_id", TypeName = "bigint unsigned")]
    public ulong? CargoHoldLocationId { get; init; }

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
