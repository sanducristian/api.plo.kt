// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_ownership table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_ownership")]
public sealed record DeviceOwnership
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: device_unit_id; SQL: bigint unsigned; not null.</summary>
    [Column("device_unit_id", TypeName = "bigint unsigned")]
    public required ulong DeviceUnitId { get; init; }

    /// <summary>Column: owner_workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("owner_workspace_id", TypeName = "bigint unsigned")]
    public required ulong OwnerWorkspaceId { get; init; }

    /// <summary>Column: ownership_type; SQL: varchar(24); not null.</summary>
    [Column("ownership_type", TypeName = "varchar(24)")]
    public required string OwnershipType { get; init; }

    /// <summary>Column: acquisition_source; SQL: varchar(24); not null.</summary>
    [Column("acquisition_source", TypeName = "varchar(24)")]
    public required string AcquisitionSource { get; init; }

    /// <summary>Column: source_reference; SQL: varchar(128); nullable.</summary>
    [Column("source_reference", TypeName = "varchar(128)")]
    public string? SourceReference { get; init; }

    /// <summary>Column: valid_from_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("valid_from_utc", TypeName = "datetime(6)")]
    public required DateTime ValidFromUtc { get; init; }

    /// <summary>Column: valid_to_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("valid_to_utc", TypeName = "datetime(6)")]
    public DateTime? ValidToUtc { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? CreatedByPrincipalId { get; init; }

    /// <summary>Column: active_device_unit_id; SQL: bigint unsigned; nullable.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_device_unit_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveDeviceUnitId { get; init; }
}
