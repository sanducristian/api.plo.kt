// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Compatibility;

/// <summary>Maps one row of the companyagency table. Includes all 3 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("companyagency")]
public sealed record CompanyAgency
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: VAT; SQL: varchar(64); nullable.</summary>
    [Column("VAT", TypeName = "varchar(64)")]
    public string? VAT { get; init; }

    /// <summary>Column: RC; SQL: varchar(64); nullable.</summary>
    [Column("RC", TypeName = "varchar(64)")]
    public string? RC { get; init; }
}
