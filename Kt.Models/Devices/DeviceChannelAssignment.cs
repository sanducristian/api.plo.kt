// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_channel_assignment table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_channel_assignment")]
public sealed record DeviceChannelAssignment
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Internal primary key ID</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null. Application UUID stored as binary for safe API exposure</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: device_channel_id; SQL: bigint unsigned; not null. Assigned measurement channel</summary>
    [Column("device_channel_id", TypeName = "bigint unsigned")]
    public required ulong DeviceChannelId { get; init; }

    /// <summary>Column: location_id; SQL: bigint unsigned; nullable. Target physical location in core_location hierarchy</summary>
    [Column("location_id", TypeName = "bigint unsigned")]
    public ulong? LocationId { get; init; }

    /// <summary>Column: target_entity_type; SQL: varchar(64); not null. Measured object category: PIPE, ELECTRICAL_CIRCUIT, EQUIPMENT, BUILDING_SECTOR</summary>
    [Column("target_entity_type", TypeName = "varchar(64)")]
    public required string TargetEntityType { get; init; }

    /// <summary>Column: target_entity_ref; SQL: varchar(128); not null. Target infrastructure reference code or ID</summary>
    [Column("target_entity_ref", TypeName = "varchar(128)")]
    public required string TargetEntityRef { get; init; }

    /// <summary>Column: valid_from_utc; SQL: datetime(6); not null. UTC start timestamp of active measurement assignment</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("valid_from_utc", TypeName = "datetime(6)")]
    public required DateTime ValidFromUtc { get; init; }

    /// <summary>Column: valid_to_utc; SQL: datetime(6); nullable. UTC end timestamp of assignment (NULL if currently active)</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("valid_to_utc", TypeName = "datetime(6)")]
    public DateTime? ValidToUtc { get; init; }

    /// <summary>Column: assigned_by_principal_id; SQL: bigint unsigned; not null. Security principal performing the assignment</summary>
    [Column("assigned_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong AssignedByPrincipalId { get; init; }

    /// <summary>Column: notes; SQL: varchar(255); nullable. Optional operational or installation notes</summary>
    [Column("notes", TypeName = "varchar(255)")]
    public string? Notes { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. Microsecond UTC creation timestamp</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }
}
