// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the security_db_script_actor table. Includes all 2 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("security_db_script_actor")]
public sealed record SecurityDbScriptActor
{
    /// <summary>Column: session_login; SQL: varchar(288); not null. Exact SESSION_USER() value for an approved maintenance connection.</summary>
    [Column("session_login", TypeName = "varchar(288)")]
    public required string SessionLogin { get; init; }

    /// <summary>Column: principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("principal_id", TypeName = "bigint unsigned")]
    public required ulong PrincipalId { get; init; }
}
