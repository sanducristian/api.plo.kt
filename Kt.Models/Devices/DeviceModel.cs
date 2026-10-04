// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_model table. Includes all 14 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_model")]
public sealed record DeviceModel
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID exposed by APIs</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: model_code; SQL: varchar(64); not null.</summary>
    [Column("model_code", TypeName = "varchar(64)")]
    public required string ModelCode { get; init; }

    /// <summary>Column: model_name; SQL: varchar(256); not null.</summary>
    [Column("model_name", TypeName = "varchar(256)")]
    public required string ModelName { get; init; }

    /// <summary>Column: manufacturer_party_id; SQL: bigint unsigned; nullable.</summary>
    [Column("manufacturer_party_id", TypeName = "bigint unsigned")]
    public ulong? ManufacturerPartyId { get; init; }

    /// <summary>Column: model_version; SQL: varchar(64); nullable.</summary>
    [Column("model_version", TypeName = "varchar(64)")]
    public string? ModelVersion { get; init; }

    /// <summary>Column: description; SQL: text; nullable.</summary>
    [Column("description", TypeName = "text")]
    public string? Description { get; init; }

    /// <summary>Column: configuration_schema_json; SQL: json; nullable. Allowed non-secret model-specific configuration fields</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("configuration_schema_json", TypeName = "json")]
    public string? ConfigurationSchemaJson { get; init; }

    /// <summary>Column: lifecycle_status; SQL: varchar(24); not null.</summary>
    [Column("lifecycle_status", TypeName = "varchar(24)")]
    public required string LifecycleStatus { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

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
