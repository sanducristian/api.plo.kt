// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Transport;

/// <summary>Maps one row of the transport_vehicle_assignment table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("transport_vehicle_assignment")]
public sealed record TransportVehicleAssignment
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: mission_id; SQL: bigint unsigned; not null.</summary>
    [Column("mission_id", TypeName = "bigint unsigned")]
    public required ulong MissionId { get; init; }

    /// <summary>Column: vehicle_id; SQL: bigint unsigned; not null.</summary>
    [Column("vehicle_id", TypeName = "bigint unsigned")]
    public required ulong VehicleId { get; init; }

    /// <summary>Column: planned_start_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("planned_start_at_utc", TypeName = "datetime(6)")]
    public required DateTime PlannedStartAtUtc { get; init; }

    /// <summary>Column: planned_end_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("planned_end_at_utc", TypeName = "datetime(6)")]
    public required DateTime PlannedEndAtUtc { get; init; }

    /// <summary>Column: activated_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("activated_at_utc", TypeName = "datetime(6)")]
    public DateTime? ActivatedAtUtc { get; init; }

    /// <summary>Column: ended_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("ended_at_utc", TypeName = "datetime(6)")]
    public DateTime? EndedAtUtc { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null.</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }

    /// <summary>Column: active_vehicle_id; SQL: bigint unsigned; nullable.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_vehicle_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveVehicleId { get; init; }

    /// <summary>Column: active_mission_id; SQL: bigint unsigned; nullable.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_mission_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveMissionId { get; init; }
}
