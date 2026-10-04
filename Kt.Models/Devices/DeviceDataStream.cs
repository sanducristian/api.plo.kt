// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_data_stream table. Includes all 17 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_data_stream")]
public sealed record DeviceDataStream
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

    /// <summary>Column: device_unit_id; SQL: bigint unsigned; not null.</summary>
    [Column("device_unit_id", TypeName = "bigint unsigned")]
    public required ulong DeviceUnitId { get; init; }

    /// <summary>Column: stream_code; SQL: varchar(64); not null.</summary>
    [Column("stream_code", TypeName = "varchar(64)")]
    public required string StreamCode { get; init; }

    /// <summary>Column: stream_kind; SQL: varchar(24); not null.</summary>
    [Column("stream_kind", TypeName = "varchar(24)")]
    public required string StreamKind { get; init; }

    /// <summary>Column: backend_code; SQL: varchar(64); not null. Configured backend alias; never a password or credential-bearing URL</summary>
    [Column("backend_code", TypeName = "varchar(64)")]
    public required string BackendCode { get; init; }

    /// <summary>Column: external_stream_key; SQL: varchar(255); not null.</summary>
    [Column("external_stream_key", TypeName = "varchar(255)")]
    public required string ExternalStreamKey { get; init; }

    /// <summary>Column: schema_version; SQL: varchar(32); not null.</summary>
    [Column("schema_version", TypeName = "varchar(32)")]
    public required string SchemaVersion { get; init; }

    /// <summary>Column: schema_json; SQL: json; not null.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("schema_json", TypeName = "json")]
    public required string SchemaJson { get; init; }

    /// <summary>Column: expected_sample_interval_ms; SQL: int unsigned; nullable.</summary>
    [Column("expected_sample_interval_ms", TypeName = "int unsigned")]
    public uint? ExpectedSampleIntervalMs { get; init; }

    /// <summary>Column: raw_retention_days; SQL: int unsigned; nullable.</summary>
    [Column("raw_retention_days", TypeName = "int unsigned")]
    public uint? RawRetentionDays { get; init; }

    /// <summary>Column: aggregate_retention_days; SQL: int unsigned; nullable.</summary>
    [Column("aggregate_retention_days", TypeName = "int unsigned")]
    public uint? AggregateRetentionDays { get; init; }

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

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }
}
