// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the security_principal table. Includes all 7 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("security_principal")]
public sealed record SecurityPrincipal
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Internal identifier used as the actor in modern audit and authorization records.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: principal_code; SQL: varchar(128); not null. Stable machine-readable code; system codes are never translated or reused.</summary>
    [Column("principal_code", TypeName = "varchar(128)")]
    public required string PrincipalCode { get; init; }

    /// <summary>Column: principal_type; SQL: varchar(32); not null. USER, SYSTEM, MIGRATION, INTEGRATION, BACKGROUND_JOB or SUPPORT.</summary>
    [Column("principal_type", TypeName = "varchar(32)")]
    public required string PrincipalType { get; init; }

    /// <summary>Column: display_name; SQL: varchar(255); not null. Human-readable principal name; not used as an authentication key.</summary>
    [Column("display_name", TypeName = "varchar(255)")]
    public required string DisplayName { get; init; }

    /// <summary>Column: status; SQL: varchar(16); not null. ACTIVE or DISABLED.</summary>
    [Column("status", TypeName = "varchar(16)")]
    public required string Status { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null. UTC creation date and time.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: disabled_utc; SQL: datetime(6); nullable. UTC date and time when the principal was disabled, if applicable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("disabled_utc", TypeName = "datetime(6)")]
    public DateTime? DisabledUtc { get; init; }
}
