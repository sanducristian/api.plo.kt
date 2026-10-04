// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Companies;

/// <summary>Maps one row of the company_workspace table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("company_workspace")]
public sealed record CompanyWorkspace
{
    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null. Workspace that represents the company operating boundary.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: core_party_id; SQL: bigint unsigned; not null. Authoritative organization party represented by the workspace.</summary>
    [Column("core_party_id", TypeName = "bigint unsigned")]
    public required ulong CorePartyId { get; init; }

    /// <summary>Column: primary_legal_entity_id; SQL: bigint unsigned; nullable. Optional primary accounting legal entity; additional entities remain module relationships.</summary>
    [Column("primary_legal_entity_id", TypeName = "bigint unsigned")]
    public ulong? PrimaryLegalEntityId { get; init; }

    /// <summary>Column: status; SQL: varchar(24); not null. PENDING_VALIDATION, ACTIVE, SUSPENDED or CLOSED.</summary>
    [Column("status", TypeName = "varchar(24)")]
    public required string Status { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }

    /// <summary>Column: activated_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("activated_utc", TypeName = "datetime(6)")]
    public DateTime? ActivatedUtc { get; init; }

    /// <summary>Column: suspended_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("suspended_utc", TypeName = "datetime(6)")]
    public DateTime? SuspendedUtc { get; init; }

    /// <summary>Column: closed_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("closed_utc", TypeName = "datetime(6)")]
    public DateTime? ClosedUtc { get; init; }

    /// <summary>Column: updated_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedUtc { get; init; }
}
