// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.System;

/// <summary>Maps one row of the database_migration table. Includes all 6 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("database_migration")]
public sealed record DatabaseMigration
{
    /// <summary>Column: migration_id; SQL: varchar(100); not null. Stable identifier of the migration; never reuse an identifier for different SQL.</summary>
    [Column("migration_id", TypeName = "varchar(100)")]
    public required string MigrationId { get; init; }

    /// <summary>Column: script_name; SQL: varchar(255); not null. Filename or deployment artifact that applied the migration.</summary>
    [Column("script_name", TypeName = "varchar(255)")]
    public required string ScriptName { get; init; }

    /// <summary>Column: checksum_sha256; SQL: char(64); nullable. Optional SHA-256 of the approved script, populated by the deployment pipeline.</summary>
    [Column("checksum_sha256", TypeName = "char(64)")]
    public string? ChecksumSha256 { get; init; }

    /// <summary>Column: applied_utc; SQL: datetime(6); not null. UTC date and time when the migration was recorded.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("applied_utc", TypeName = "datetime(6)")]
    public required DateTime AppliedUtc { get; init; }

    /// <summary>Column: applied_by; SQL: varchar(255); not null. MySQL account that recorded the migration.</summary>
    [Column("applied_by", TypeName = "varchar(255)")]
    public required string AppliedBy { get; init; }

    /// <summary>Column: notes; SQL: varchar(1000); nullable. Deployment notes that do not affect migration identity.</summary>
    [Column("notes", TypeName = "varchar(1000)")]
    public string? Notes { get; init; }
}
