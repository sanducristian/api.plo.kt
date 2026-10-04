// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.System;

/// <summary>Maps one row of the sys_setting table. Includes all 5 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("sys_setting")]
public sealed record SystemSetting
{
    /// <summary>Column: id; SQL: bigint unsigned; not null. Global internal identity and primary key for the setting record</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: class; SQL: varchar(64); not null. Application class or domain category grouping for the setting</summary>
    [Column("class", TypeName = "varchar(64)")]
    public required string Class { get; init; }

    /// <summary>Column: key; SQL: varchar(64); not null. Unique configuration key identifier within the section/class</summary>
    [Column("key", TypeName = "varchar(64)")]
    public required string Key { get; init; }

    /// <summary>Column: section; SQL: varchar(64); not null. Sub-section or logical grouping for configuration parameters</summary>
    [Column("section", TypeName = "varchar(64)")]
    public required string Section { get; init; }

    /// <summary>Column: value; SQL: varchar(128); not null. Configured setting value stored as a string</summary>
    [Column("value", TypeName = "varchar(128)")]
    public required string Value { get; init; }
}
