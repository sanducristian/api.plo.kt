// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_referral_code table. Includes all 14 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_referral_code")]
public sealed record PlatformUserReferralCode
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: referral_program_id; SQL: bigint unsigned; not null.</summary>
    [Column("referral_program_id", TypeName = "bigint unsigned")]
    public required ulong ReferralProgramId { get; init; }

    /// <summary>Column: referring_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("referring_user_id", TypeName = "bigint unsigned")]
    public ulong? ReferringUserId { get; init; }

    /// <summary>Column: referring_workspace_id; SQL: bigint unsigned; nullable.</summary>
    [Column("referring_workspace_id", TypeName = "bigint unsigned")]
    public ulong? ReferringWorkspaceId { get; init; }

    /// <summary>Column: code_hash; SQL: varchar(64); not null. Protected comparison value for the referral code; plaintext codes must not be logged.</summary>
    [Column("code_hash", TypeName = "varchar(64)")]
    public required string CodeHash { get; init; }

    /// <summary>Column: masked_display_code; SQL: varchar(32); not null.</summary>
    [Column("masked_display_code", TypeName = "varchar(32)")]
    public required string MaskedDisplayCode { get; init; }

    /// <summary>Column: max_uses; SQL: int unsigned; nullable.</summary>
    [Column("max_uses", TypeName = "int unsigned")]
    public uint? MaxUses { get; init; }

    /// <summary>Column: current_uses; SQL: int unsigned; not null.</summary>
    [Column("current_uses", TypeName = "int unsigned")]
    public required uint CurrentUses { get; init; }

    /// <summary>Column: is_disabled; SQL: tinyint(1); not null.</summary>
    [Column("is_disabled", TypeName = "tinyint(1)")]
    public required bool IsDisabled { get; init; }

    /// <summary>Column: valid_from_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("valid_from_utc", TypeName = "datetime(6)")]
    public required DateTime ValidFromUtc { get; init; }

    /// <summary>Column: valid_to_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("valid_to_utc", TypeName = "datetime(6)")]
    public DateTime? ValidToUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }
}
