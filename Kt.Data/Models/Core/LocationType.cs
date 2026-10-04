// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_location_type table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_location_type")]
public sealed record LocationType
{
    /// <summary>Column: code; SQL: varchar(32); not null.</summary>
    [Column("code", TypeName = "varchar(32)")]
    public required string Code { get; init; }

    /// <summary>Column: name; SQL: varchar(128); not null.</summary>
    [Column("name", TypeName = "varchar(128)")]
    public required string Name { get; init; }

    /// <summary>Column: description; SQL: varchar(255); nullable.</summary>
    [Column("description", TypeName = "varchar(255)")]
    public string? Description { get; init; }

    /// <summary>Column: is_active; SQL: int; nullable.</summary>
    [Column("is_active", TypeName = "int")]
    public int? IsActive { get; init; }
}
