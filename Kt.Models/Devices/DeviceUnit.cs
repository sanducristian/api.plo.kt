// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_unit table. Includes all 16 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_unit")]
public sealed record DeviceUnit
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID exposed by APIs</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: module_ref; SQL: varchar(128); not null. Stable case-sensitive Device-module reference used by claim and transfer workflows</summary>
    [Column("module_ref", TypeName = "varchar(128)")]
    public required string ModuleRef { get; init; }

    /// <summary>Column: device_model_id; SQL: bigint unsigned; not null.</summary>
    [Column("device_model_id", TypeName = "bigint unsigned")]
    public required ulong DeviceModelId { get; init; }

    /// <summary>Column: internal_serial_number; SQL: varchar(128); nullable.</summary>
    [Column("internal_serial_number", TypeName = "varchar(128)")]
    public string? InternalSerialNumber { get; init; }

    /// <summary>Column: manufacturer_serial_number; SQL: varchar(128); nullable.</summary>
    [Column("manufacturer_serial_number", TypeName = "varchar(128)")]
    public string? ManufacturerSerialNumber { get; init; }

    /// <summary>Column: hardware_revision; SQL: varchar(64); nullable.</summary>
    [Column("hardware_revision", TypeName = "varchar(64)")]
    public string? HardwareRevision { get; init; }

    /// <summary>Column: manufactured_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("manufactured_at_utc", TypeName = "datetime(6)")]
    public DateTime? ManufacturedAtUtc { get; init; }

    /// <summary>Column: released_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("released_at_utc", TypeName = "datetime(6)")]
    public DateTime? ReleasedAtUtc { get; init; }

    /// <summary>Column: lifecycle_status; SQL: varchar(24); not null.</summary>
    [Column("lifecycle_status", TypeName = "varchar(24)")]
    public required string LifecycleStatus { get; init; }

    /// <summary>Column: configuration_json; SQL: json; nullable. Validated non-secret device configuration snapshot</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("configuration_json", TypeName = "json")]
    public string? ConfigurationJson { get; init; }

    /// <summary>Column: legacy_object_id; SQL: bigint unsigned; nullable.</summary>
    [Column("legacy_object_id", TypeName = "bigint unsigned")]
    public ulong? LegacyObjectId { get; init; }

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
