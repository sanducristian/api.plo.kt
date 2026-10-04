// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.System;

/// <summary>Maps one row of the sys_version table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_version")]
public sealed record SystemVersion
{
    /// <summary>Column: id; SQL: bigint; not null. Primary key identifier for the version record</summary>
    [Column("id", TypeName = "bigint")]
    public required long Id { get; init; }

    /// <summary>Column: version_date; SQL: date; nullable. Date when the database and application version was released or applied</summary>
    [Column("version_date", TypeName = "date")]
    public DateOnly? VersionDate { get; init; }

    /// <summary>Column: version_db_no; SQL: varchar(64); nullable. Database version identifier number</summary>
    [Column("version_db_no", TypeName = "varchar(64)")]
    public string? VersionDbNo { get; init; }

    /// <summary>Column: version_app_no; SQL: varchar(64); nullable. Application version identifier number</summary>
    [Column("version_app_no", TypeName = "varchar(64)")]
    public string? VersionAppNo { get; init; }
}
