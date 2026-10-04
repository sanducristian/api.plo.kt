// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_referral_program table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_referral_program")]
public sealed record PlatformUserReferralProgram
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: program_code; SQL: varchar(64); not null.</summary>
    [Column("program_code", TypeName = "varchar(64)")]
    public required string ProgramCode { get; init; }

    /// <summary>Column: scope_workspace_id; SQL: bigint unsigned; nullable.</summary>
    [Column("scope_workspace_id", TypeName = "bigint unsigned")]
    public ulong? ScopeWorkspaceId { get; init; }

    /// <summary>Column: policy_version; SQL: varchar(32); not null.</summary>
    [Column("policy_version", TypeName = "varchar(32)")]
    public required string PolicyVersion { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null.</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

    /// <summary>Column: start_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("start_utc", TypeName = "datetime(6)")]
    public required DateTime StartUtc { get; init; }

    /// <summary>Column: end_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("end_utc", TypeName = "datetime(6)")]
    public DateTime? EndUtc { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
