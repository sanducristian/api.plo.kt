// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Identity;

/// <summary>Maps one row of the platform_phone_number table. Includes all 4 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("platform_phone_number")]
public sealed record PlatformPhoneNumber
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: phone_e164; SQL: varchar(20); not null. Canonical international number in E.164 format, including the leading plus sign.</summary>
    [Column("phone_e164", TypeName = "varchar(20)")]
    public required string PhoneE164 { get; init; }

    /// <summary>Column: masked_display; SQL: varchar(32); nullable. Optional pre-masked representation suitable for ordinary UI display.</summary>
    [Column("masked_display", TypeName = "varchar(32)")]
    public string? MaskedDisplay { get; init; }

    /// <summary>Column: created_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedUtc { get; init; }
}
