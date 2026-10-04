// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects.Dynamic;

/// <summary>Maps one row of the obj_dyn table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_dyn")]
public sealed record ObjectDynReference {
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: masterId; SQL: bigint unsigned; not null.</summary>
    [Column("masterId", TypeName = "bigint unsigned")]
    public required ulong MasterId { get; init; }

    /// <summary>Column: slaveId; SQL: bigint unsigned; not null.</summary>
    [Column("slaveId", TypeName = "bigint unsigned")]
    public required ulong SlaveId { get; init; }

    /// <summary>Column: typeId; SQL: bigint unsigned; not null.</summary>
    [Column("typeId", TypeName = "bigint unsigned")]
    public required ulong TypeId { get; init; }
}
