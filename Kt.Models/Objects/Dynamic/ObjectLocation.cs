// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Objects.Dynamic;

/// <summary>Maps one row of the obj_dyn_location table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("obj_dyn_location")]
public sealed record ObjectDynLocation {
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: country; SQL: varchar(128); nullable.</summary>
    [Column("country", TypeName = "varchar(128)")]
    public string? Country { get; init; }

    /// <summary>Column: county; SQL: varchar(128); nullable.</summary>
    [Column("county", TypeName = "varchar(128)")]
    public string? County { get; init; }

    /// <summary>Column: department; SQL: varchar(128); nullable.</summary>
    [Column("department", TypeName = "varchar(128)")]
    public string? Department { get; init; }

    /// <summary>Column: city; SQL: varchar(128); nullable.</summary>
    [Column("city", TypeName = "varchar(128)")]
    public string? City { get; init; }

    /// <summary>Column: street; SQL: varchar(128); nullable.</summary>
    [Column("street", TypeName = "varchar(128)")]
    public string? Street { get; init; }

    /// <summary>Column: building; SQL: varchar(128); nullable.</summary>
    [Column("building", TypeName = "varchar(128)")]
    public string? Building { get; init; }

    /// <summary>Column: zipcode; SQL: varchar(64); nullable.</summary>
    [Column("zipcode", TypeName = "varchar(64)")]
    public string? Zipcode { get; init; }
}
