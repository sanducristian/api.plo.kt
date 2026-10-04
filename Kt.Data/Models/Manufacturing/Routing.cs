// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Manufacturing;

/// <summary>Maps one row of the mfg_routing table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("mfg_routing")]
public sealed record Routing
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: device_model_id; SQL: bigint unsigned; not null.</summary>
    [Column("device_model_id", TypeName = "bigint unsigned")]
    public required ulong DeviceModelId { get; init; }

    /// <summary>Column: routing_code; SQL: varchar(64); not null.</summary>
    [Column("routing_code", TypeName = "varchar(64)")]
    public required string RoutingCode { get; init; }

    /// <summary>Column: revision_code; SQL: varchar(32); not null.</summary>
    [Column("revision_code", TypeName = "varchar(32)")]
    public required string RevisionCode { get; init; }

    /// <summary>Column: status_code; SQL: varchar(16); not null.</summary>
    [Column("status_code", TypeName = "varchar(16)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: valid_from; SQL: date; nullable.</summary>
    [Column("valid_from", TypeName = "date")]
    public DateOnly? ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable.</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? CreatedByPrincipalId { get; init; }

    /// <summary>Column: released_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("released_at_utc", TypeName = "datetime(6)")]
    public DateTime? ReleasedAtUtc { get; init; }

    /// <summary>Column: released_by_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("released_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? ReleasedByPrincipalId { get; init; }
}
