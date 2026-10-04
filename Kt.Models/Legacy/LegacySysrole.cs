// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Legacy;

/// <summary>Maps one row of the legacy_sysrole table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("legacy_sysrole")]
public sealed record LegacySysrole
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: guid; SQL: varchar(36); not null. The GUID number for the unit</summary>
    [Column("guid", TypeName = "varchar(36)")]
    public required string Guid { get; init; }

    /// <summary>Column: name; SQL: varchar(250); not null. The name of the current node</summary>
    [Column("name", TypeName = "varchar(250)")]
    public required string Name { get; init; }

    /// <summary>Column: singleton; SQL: tinyint(1); not null. If set, a single reference per object must be made ex: - sys.Name --&gt; a single reference must exist - user.group --&gt; a user can be in several group</summary>
    [Column("singleton", TypeName = "tinyint(1)")]
    public required bool Singleton { get; init; }
}
