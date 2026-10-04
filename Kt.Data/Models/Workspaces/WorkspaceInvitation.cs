// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Workspaces;

/// <summary>Maps one row of the workspace_invitation table. Includes all 17 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("workspace_invitation")]
public sealed record WorkspaceInvitation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: inviter_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("inviter_principal_id", TypeName = "bigint unsigned")]
    public required ulong InviterPrincipalId { get; init; }

    /// <summary>Column: intended_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("intended_user_id", TypeName = "bigint unsigned")]
    public ulong? IntendedUserId { get; init; }

    /// <summary>Column: protected_target_contact_hash; SQL: varchar(64); nullable.</summary>
    [Column("protected_target_contact_hash", TypeName = "varchar(64)")]
    public string? ProtectedTargetContactHash { get; init; }

    /// <summary>Column: invitation_profile; SQL: varchar(64); not null.</summary>
    [Column("invitation_profile", TypeName = "varchar(64)")]
    public required string InvitationProfile { get; init; }

    /// <summary>Column: status; SQL: varchar(32); not null.</summary>
    [Column("status", TypeName = "varchar(32)")]
    public required string Status { get; init; }

    /// <summary>Column: identity_service_ref; SQL: varchar(128); nullable.</summary>
    [Column("identity_service_ref", TypeName = "varchar(128)")]
    public string? IdentityServiceRef { get; init; }

    /// <summary>Column: resulting_membership_id; SQL: bigint unsigned; nullable.</summary>
    [Column("resulting_membership_id", TypeName = "bigint unsigned")]
    public ulong? ResultingMembershipId { get; init; }

    /// <summary>Column: acceptance_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("acceptance_principal_id", TypeName = "bigint unsigned")]
    public ulong? AcceptancePrincipalId { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: issued_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("issued_utc", TypeName = "datetime(6)")]
    public required DateTime IssuedUtc { get; init; }

    /// <summary>Column: expires_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("expires_utc", TypeName = "datetime(6)")]
    public required DateTime ExpiresUtc { get; init; }

    /// <summary>Column: accepted_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("accepted_utc", TypeName = "datetime(6)")]
    public DateTime? AcceptedUtc { get; init; }

    /// <summary>Column: cancelled_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("cancelled_utc", TypeName = "datetime(6)")]
    public DateTime? CancelledUtc { get; init; }

    /// <summary>Column: revoked_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("revoked_utc", TypeName = "datetime(6)")]
    public DateTime? RevokedUtc { get; init; }
}
