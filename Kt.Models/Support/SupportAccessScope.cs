// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Support;

/// <summary>Maps one row of the support_access_scope table. Includes all 5 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("support_access_scope")]
public sealed record SupportAccessScope
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: support_access_grant_id; SQL: bigint unsigned; not null.</summary>
    [Column("support_access_grant_id", TypeName = "bigint unsigned")]
    public required ulong SupportAccessGrantId { get; init; }

    /// <summary>Column: resource_type; SQL: varchar(64); not null. Module-defined resource type such as WORKSPACE or DEVICE.</summary>
    [Column("resource_type", TypeName = "varchar(64)")]
    public required string ResourceType { get; init; }

    /// <summary>Column: resource_id; SQL: varchar(191); nullable. Module resource identifier; NULL means the allowed workspace-wide scope.</summary>
    [Column("resource_id", TypeName = "varchar(191)")]
    public string? ResourceId { get; init; }

    /// <summary>Column: permission_code; SQL: varchar(160); not null. Exact approved permission; grants do not imply unrestricted support access.</summary>
    [Column("permission_code", TypeName = "varchar(160)")]
    public required string PermissionCode { get; init; }
}
