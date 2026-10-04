// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_identifier table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_identifier")]
public sealed record DeviceIdentifier
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: device_unit_id; SQL: bigint unsigned; not null.</summary>
    [Column("device_unit_id", TypeName = "bigint unsigned")]
    public required ulong DeviceUnitId { get; init; }

    /// <summary>Column: identifier_type; SQL: varchar(32); not null.</summary>
    [Column("identifier_type", TypeName = "varchar(32)")]
    public required string IdentifierType { get; init; }

    /// <summary>Column: identifier_value; SQL: varchar(256); nullable. Plain value only for identifiers approved for plaintext storage</summary>
    [Column("identifier_value", TypeName = "varchar(256)")]
    public string? IdentifierValue { get; init; }

    /// <summary>Column: protected_lookup_hash; SQL: binary(32); nullable.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("protected_lookup_hash", TypeName = "binary(32)")]
    public byte[]? ProtectedLookupHash { get; init; }

    /// <summary>Column: masked_value; SQL: varchar(256); nullable.</summary>
    [Column("masked_value", TypeName = "varchar(256)")]
    public string? MaskedValue { get; init; }

    /// <summary>Column: is_primary; SQL: tinyint(1); not null.</summary>
    [Column("is_primary", TypeName = "tinyint(1)")]
    public required bool IsPrimary { get; init; }

    /// <summary>Column: valid_from; SQL: date; nullable.</summary>
    [Column("valid_from", TypeName = "date")]
    public DateOnly? ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable.</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }
}
