// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Legacy;

/// <summary>Maps one row of the legacy_user table. Includes all 3 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("legacy_user")]
public sealed record LegacyUser
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: username; SQL: varchar(128); not null.</summary>
    [Column("username", TypeName = "varchar(128)")]
    public required string Username { get; init; }

    /// <summary>Column: active; SQL: tinyint(1); not null.</summary>
    [Column("active", TypeName = "tinyint(1)")]
    public required bool Active { get; init; }
}
