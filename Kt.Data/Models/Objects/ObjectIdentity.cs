// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects;

/// <summary>Maps one row of the obj_id table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_id")]
public sealed record ObjectIdentity
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Globally unique internal object ID, shared by the corresponding record in its concrete table.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: table_id; SQL: bigint unsigned; not null. Concrete table identifier resolved through the active object table catalogue. Reserved values follow kernel catalogue conventions.</summary>
    [Column("table_id", TypeName = "bigint unsigned")]
    public required ulong TableId { get; init; }

    /// <summary>Column: invalid; SQL: tinyint(1); not null. Logical invalidation flag: 0 = active; 1 = invalidated or logically deleted. Does not imply physical deletion.</summary>
    [Column("invalid", TypeName = "tinyint(1)")]
    public required bool Invalid { get; init; }

    /// <summary>Column: original_id; SQL: bigint unsigned; not null. Object ID referenced for version lineage when a revised object is created. The kernel versioning rules determine the referenced version.</summary>
    [Column("original_id", TypeName = "bigint unsigned")]
    public required ulong OriginalId { get; init; }

    /// <summary>Column: last_update; SQL: timestamp; not null. Creation or latest modification timestamp of this registry row; does not automatically track changes to the concrete object record.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("last_update", TypeName = "timestamp")]
    public required DateTime LastUpdate { get; init; }

    /// <summary>Column: user_id; SQL: bigint unsigned; nullable. Internal PLO user ID attributed to this registry entry by kernel routines or triggers. Not automatically updated by the timestamp mechanism.</summary>
    [Column("user_id", TypeName = "bigint unsigned")]
    public ulong? UserId { get; init; }

    /// <summary>Column: master_id; SQL: bigint unsigned; not null. Global object ID of the owning or parent object; for a dynamic property, identifies the object described by that property.</summary>
    [Column("master_id", TypeName = "bigint unsigned")]
    public required ulong MasterId { get; init; }

    /// <summary>Column: role_id; SQL: bigint unsigned; not null. Semantic role identifier resolved through the active role catalogue; defines the meaning of this object or its relationship to its master. Not an access-control role.</summary>
    [Column("role_id", TypeName = "bigint unsigned")]
    public required ulong RoleId { get; init; }
}
