// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_document table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_document")]
public sealed record DeviceDocument
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: device_unit_id; SQL: bigint unsigned; not null.</summary>
    [Column("device_unit_id", TypeName = "bigint unsigned")]
    public required ulong DeviceUnitId { get; init; }

    /// <summary>Column: file_id; SQL: bigint unsigned; not null.</summary>
    [Column("file_id", TypeName = "bigint unsigned")]
    public required ulong FileId { get; init; }

    /// <summary>Column: document_role; SQL: varchar(32); not null.</summary>
    [Column("document_role", TypeName = "varchar(32)")]
    public required string DocumentRole { get; init; }

    /// <summary>Column: document_version; SQL: varchar(64); nullable.</summary>
    [Column("document_version", TypeName = "varchar(64)")]
    public string? DocumentVersion { get; init; }

    /// <summary>Column: is_current; SQL: tinyint(1); not null.</summary>
    [Column("is_current", TypeName = "tinyint(1)")]
    public required bool IsCurrent { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? CreatedByPrincipalId { get; init; }
}
