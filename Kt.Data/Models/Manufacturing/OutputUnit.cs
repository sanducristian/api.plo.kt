// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Manufacturing;

/// <summary>Maps one row of the mfg_output_unit table. Includes all 7 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("mfg_output_unit")]
public sealed record OutputUnit
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: work_order_id; SQL: bigint unsigned; not null.</summary>
    [Column("work_order_id", TypeName = "bigint unsigned")]
    public required ulong WorkOrderId { get; init; }

    /// <summary>Column: device_unit_id; SQL: bigint unsigned; not null.</summary>
    [Column("device_unit_id", TypeName = "bigint unsigned")]
    public required ulong DeviceUnitId { get; init; }

    /// <summary>Column: output_sequence; SQL: int unsigned; not null.</summary>
    [Column("output_sequence", TypeName = "int unsigned")]
    public required uint OutputSequence { get; init; }

    /// <summary>Column: output_status; SQL: varchar(20); not null.</summary>
    [Column("output_status", TypeName = "varchar(20)")]
    public required string OutputStatus { get; init; }

    /// <summary>Column: completed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("completed_at_utc", TypeName = "datetime(6)")]
    public DateTime? CompletedAtUtc { get; init; }

    /// <summary>Column: released_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("released_at_utc", TypeName = "datetime(6)")]
    public DateTime? ReleasedAtUtc { get; init; }
}
