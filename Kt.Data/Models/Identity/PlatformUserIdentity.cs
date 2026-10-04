// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_user_identity table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_user_identity")]
public sealed record PlatformUserIdentity
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: platform_user_id; SQL: bigint unsigned; not null. Modern user linked to the validated identity.</summary>
    [Column("platform_user_id", TypeName = "bigint unsigned")]
    public required ulong PlatformUserId { get; init; }

    /// <summary>Column: provider_code; SQL: varchar(32); not null. LOCAL, ENTRA_ID, GOOGLE or another approved provider code.</summary>
    [Column("provider_code", TypeName = "varchar(32)")]
    public required string ProviderCode { get; init; }

    /// <summary>Column: issuer; SQL: varchar(255); not null. Exact token issuer identifier; comparisons are case-sensitive.</summary>
    [Column("issuer", TypeName = "varchar(255)")]
    public required string Issuer { get; init; }

    /// <summary>Column: subject; SQL: varchar(255); not null. Stable provider subject; never substitute an email address for this value.</summary>
    [Column("subject", TypeName = "varchar(255)")]
    public required string Subject { get; init; }

    /// <summary>Column: linked_utc; SQL: datetime(6); not null. UTC time when the identity was securely linked.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("linked_utc", TypeName = "datetime(6)")]
    public required DateTime LinkedUtc { get; init; }

    /// <summary>Column: last_used_utc; SQL: datetime(6); nullable. UTC time of the latest successful authentication with this identity.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("last_used_utc", TypeName = "datetime(6)")]
    public DateTime? LastUsedUtc { get; init; }

    /// <summary>Column: disabled_utc; SQL: datetime(6); nullable. UTC time when this identity link was disabled without deleting history.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("disabled_utc", TypeName = "datetime(6)")]
    public DateTime? DisabledUtc { get; init; }
}
