// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_location table. Includes all 23 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_location")]
public sealed record Location
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global obj_id identity; allocate explicitly in the same transaction</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: parent_id; SQL: bigint unsigned; nullable. Self-referential parent location ID for arbitrary hierarchy depth</summary>
    [Column("parent_id", TypeName = "bigint unsigned")]
    public ulong? ParentId { get; init; }

    /// <summary>Column: type_code; SQL: varchar(32); not null. SITE, BUILDING, ZONE, ROW, LEVEL, STORAGE_BIN, PRODUCTION_STATION, etc.</summary>
    [Column("type_code", TypeName = "varchar(32)")]
    public required string TypeCode { get; init; }

    /// <summary>Column: code; SQL: varchar(64); not null.</summary>
    [Column("code", TypeName = "varchar(64)")]
    public required string Code { get; init; }

    /// <summary>Column: name; SQL: varchar(255); not null.</summary>
    [Column("name", TypeName = "varchar(255)")]
    public required string Name { get; init; }

    /// <summary>Column: responsible_party_id; SQL: bigint unsigned; nullable.</summary>
    [Column("responsible_party_id", TypeName = "bigint unsigned")]
    public ulong? ResponsiblePartyId { get; init; }

    /// <summary>Column: address_object_id; SQL: bigint unsigned; nullable. Optional existing obj_dyn_location address</summary>
    [Column("address_object_id", TypeName = "bigint unsigned")]
    public ulong? AddressObjectId { get; init; }

    /// <summary>Column: address_snapshot_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("address_snapshot_json", TypeName = "json")]
    public string? AddressSnapshotJson { get; init; }

    /// <summary>Column: time_zone_id; SQL: varchar(64); not null.</summary>
    [Column("time_zone_id", TypeName = "varchar(64)")]
    public required string TimeZoneId { get; init; }

    /// <summary>Column: latitude; SQL: decimal(10,8); nullable. WGS 84 Latitude in decimal degrees</summary>
    [Column("latitude", TypeName = "decimal(10,8)")]
    public decimal? Latitude { get; init; }

    /// <summary>Column: longitude; SQL: decimal(11,8); nullable. WGS 84 Longitude in decimal degrees</summary>
    [Column("longitude", TypeName = "decimal(11,8)")]
    public decimal? Longitude { get; init; }

    /// <summary>Column: altitude_meters; SQL: decimal(8,3); nullable. Altitude in meters above sea level</summary>
    [Column("altitude_meters", TypeName = "decimal(8,3)")]
    public decimal? AltitudeMeters { get; init; }

    /// <summary>Column: local_frame_location_id; SQL: bigint unsigned; nullable. Reference location acting as the local (0,0,0) origin frame</summary>
    [Column("local_frame_location_id", TypeName = "bigint unsigned")]
    public ulong? LocalFrameLocationId { get; init; }

    /// <summary>Column: pos_x_meters; SQL: decimal(8,3); nullable. Local offset X in meters relative to local frame</summary>
    [Column("pos_x_meters", TypeName = "decimal(8,3)")]
    public decimal? PosXMeters { get; init; }

    /// <summary>Column: pos_y_meters; SQL: decimal(8,3); nullable. Local offset Y in meters relative to local frame</summary>
    [Column("pos_y_meters", TypeName = "decimal(8,3)")]
    public decimal? PosYMeters { get; init; }

    /// <summary>Column: pos_z_meters; SQL: decimal(8,3); nullable. Local offset Z in meters relative to local frame</summary>
    [Column("pos_z_meters", TypeName = "decimal(8,3)")]
    public decimal? PosZMeters { get; init; }

    /// <summary>Column: is_active; SQL: tinyint unsigned; not null.</summary>
    [Column("is_active", TypeName = "tinyint unsigned")]
    public required byte IsActive { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
