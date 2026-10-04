// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Legacy;

/// <summary>Maps one row of the legacy_order table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("legacy_order")]
public sealed record LegacyOrder
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: internalReference; SQL: varchar(64); nullable.</summary>
    [Column("internalReference", TypeName = "varchar(64)")]
    public string? InternalReference { get; init; }

    /// <summary>Column: internaleDate; SQL: date; nullable.</summary>
    [Column("internaleDate", TypeName = "date")]
    public DateOnly? InternaleDate { get; init; }

    /// <summary>Column: externalReference; SQL: varchar(64); nullable.</summary>
    [Column("externalReference", TypeName = "varchar(64)")]
    public string? ExternalReference { get; init; }

    /// <summary>Column: externalDate; SQL: date; nullable.</summary>
    [Column("externalDate", TypeName = "date")]
    public DateOnly? ExternalDate { get; init; }

    /// <summary>Column: dateRequested; SQL: date; nullable.</summary>
    [Column("dateRequested", TypeName = "date")]
    public DateOnly? DateRequested { get; init; }

    /// <summary>Column: dateEstimated; SQL: date; nullable.</summary>
    [Column("dateEstimated", TypeName = "date")]
    public DateOnly? DateEstimated { get; init; }

    /// <summary>Column: dateFinished; SQL: date; nullable.</summary>
    [Column("dateFinished", TypeName = "date")]
    public DateOnly? DateFinished { get; init; }

    /// <summary>Column: dateStart; SQL: date; nullable.</summary>
    [Column("dateStart", TypeName = "date")]
    public DateOnly? DateStart { get; init; }

    /// <summary>Column: status; SQL: varchar(64); nullable.</summary>
    [Column("status", TypeName = "varchar(64)")]
    public string? Status { get; init; }
}
