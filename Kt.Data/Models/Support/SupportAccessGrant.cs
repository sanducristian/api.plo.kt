// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Support;

/// <summary>Maps one row of the support_access_grant table. Includes all 12 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("support_access_grant")]
public sealed record SupportAccessGrant
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: grantee_principal_id; SQL: bigint unsigned; not null. Support user or service receiving temporary access.</summary>
    [Column("grantee_principal_id", TypeName = "bigint unsigned")]
    public required ulong GranteePrincipalId { get; init; }

    /// <summary>Column: granted_by_user_id; SQL: bigint unsigned; not null. Workspace user who explicitly approved the access.</summary>
    [Column("granted_by_user_id", TypeName = "bigint unsigned")]
    public required ulong GrantedByUserId { get; init; }

    /// <summary>Column: status; SQL: varchar(16); not null. ACTIVE, EXPIRED, REVOKED or CANCELLED.</summary>
    [Column("status", TypeName = "varchar(16)")]
    public required string Status { get; init; }

    /// <summary>Column: reason; SQL: varchar(1000); not null. Owner-visible business reason for the temporary access.</summary>
    [Column("reason", TypeName = "varchar(1000)")]
    public required string Reason { get; init; }

    /// <summary>Column: valid_from_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("valid_from_utc", TypeName = "datetime(6)")]
    public required DateTime ValidFromUtc { get; init; }

    /// <summary>Column: expires_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("expires_utc", TypeName = "datetime(6)")]
    public required DateTime ExpiresUtc { get; init; }

    /// <summary>Column: revoked_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("revoked_utc", TypeName = "datetime(6)")]
    public DateTime? RevokedUtc { get; init; }

    /// <summary>Column: revoked_by_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("revoked_by_user_id", TypeName = "bigint unsigned")]
    public ulong? RevokedByUserId { get; init; }

    /// <summary>Column: revocation_reason; SQL: varchar(1000); nullable.</summary>
    [Column("revocation_reason", TypeName = "varchar(1000)")]
    public string? RevocationReason { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }
}
