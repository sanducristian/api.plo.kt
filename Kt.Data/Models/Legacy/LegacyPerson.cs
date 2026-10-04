// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Legacy;

/// <summary>Maps one row of the legacy_person table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("legacy_person")]
public sealed record LegacyPerson
{
    /// <summary>Column: id; SQL: bigint; not null.</summary>
    [Column("id", TypeName = "bigint")]
    public required long Id { get; init; }

    /// <summary>Column: name; SQL: varchar(64); nullable.</summary>
    [Column("name", TypeName = "varchar(64)")]
    public string? Name { get; init; }

    /// <summary>Column: surname; SQL: varchar(64); nullable.</summary>
    [Column("surname", TypeName = "varchar(64)")]
    public string? Surname { get; init; }

    /// <summary>Column: title; SQL: varchar(64); nullable.</summary>
    [Column("title", TypeName = "varchar(64)")]
    public string? Title { get; init; }

    /// <summary>Column: initial; SQL: varchar(64); nullable.</summary>
    [Column("initial", TypeName = "varchar(64)")]
    public string? Initial { get; init; }

    /// <summary>Column: fatherName; SQL: varchar(64); nullable.</summary>
    [Column("fatherName", TypeName = "varchar(64)")]
    public string? FatherName { get; init; }

    /// <summary>Column: motherName; SQL: varchar(64); nullable.</summary>
    [Column("motherName", TypeName = "varchar(64)")]
    public string? MotherName { get; init; }

    /// <summary>Column: personcol; SQL: varchar(64); nullable.</summary>
    [Column("personcol", TypeName = "varchar(64)")]
    public string? Personcol { get; init; }

    /// <summary>Column: gender; SQL: varchar(45); nullable.</summary>
    [Column("gender", TypeName = "varchar(45)")]
    public string? Gender { get; init; }
}
