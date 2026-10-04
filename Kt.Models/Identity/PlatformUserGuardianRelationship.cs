// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_guardian_relationship table. Includes all 15 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_guardian_relationship")]
public sealed record PlatformUserGuardianRelationship
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: minor_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("minor_user_id", TypeName = "bigint unsigned")]
    public required ulong MinorUserId { get; init; }

    /// <summary>Column: guardian_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("guardian_user_id", TypeName = "bigint unsigned")]
    public required ulong GuardianUserId { get; init; }

    /// <summary>Column: relationship_type; SQL: varchar(32); not null.</summary>
    [Column("relationship_type", TypeName = "varchar(32)")]
    public required string RelationshipType { get; init; }

    /// <summary>Column: status; SQL: varchar(32); not null.</summary>
    [Column("status", TypeName = "varchar(32)")]
    public required string Status { get; init; }

    /// <summary>Column: verification_method; SQL: varchar(64); nullable.</summary>
    [Column("verification_method", TypeName = "varchar(64)")]
    public string? VerificationMethod { get; init; }

    /// <summary>Column: verification_ref; SQL: varchar(128); nullable.</summary>
    [Column("verification_ref", TypeName = "varchar(128)")]
    public string? VerificationRef { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: requested_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("requested_utc", TypeName = "datetime(6)")]
    public required DateTime RequestedUtc { get; init; }

    /// <summary>Column: confirmed_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("confirmed_utc", TypeName = "datetime(6)")]
    public DateTime? ConfirmedUtc { get; init; }

    /// <summary>Column: rejected_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("rejected_utc", TypeName = "datetime(6)")]
    public DateTime? RejectedUtc { get; init; }

    /// <summary>Column: ended_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("ended_utc", TypeName = "datetime(6)")]
    public DateTime? EndedUtc { get; init; }

    /// <summary>Column: active_minor_user_id; SQL: bigint unsigned; nullable.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_minor_user_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveMinorUserId { get; init; }

    /// <summary>Column: active_guardian_user_id; SQL: bigint unsigned; nullable.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_guardian_user_id", TypeName = "bigint unsigned")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public ulong? ActiveGuardianUserId { get; init; }
}
