// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Warehouse;

/// <summary>Maps one row of the wh_handling_unit table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("wh_handling_unit")]
public sealed record HandlingUnit
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

    /// <summary>Column: tracking_code; SQL: varchar(128); not null.</summary>
    [Column("tracking_code", TypeName = "varchar(128)")]
    public required string TrackingCode { get; init; }

    /// <summary>Column: unit_type; SQL: varchar(24); not null.</summary>
    [Column("unit_type", TypeName = "varchar(24)")]
    public required string UnitType { get; init; }

    /// <summary>Column: tare_mass_kg; SQL: decimal(20,6); nullable.</summary>
    [Column("tare_mass_kg", TypeName = "decimal(20,6)")]
    public decimal? TareMassKg { get; init; }

    /// <summary>Column: owner_party_id; SQL: bigint unsigned; nullable.</summary>
    [Column("owner_party_id", TypeName = "bigint unsigned")]
    public ulong? OwnerPartyId { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }
}
