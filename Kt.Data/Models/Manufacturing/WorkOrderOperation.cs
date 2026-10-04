// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Manufacturing;

/// <summary>Maps one row of the mfg_work_order_operation table. Includes all 13 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("mfg_work_order_operation")]
public sealed record WorkOrderOperation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: work_order_id; SQL: bigint unsigned; not null.</summary>
    [Column("work_order_id", TypeName = "bigint unsigned")]
    public required ulong WorkOrderId { get; init; }

    /// <summary>Column: routing_operation_id; SQL: bigint unsigned; nullable.</summary>
    [Column("routing_operation_id", TypeName = "bigint unsigned")]
    public ulong? RoutingOperationId { get; init; }

    /// <summary>Column: sequence_number; SQL: int unsigned; not null.</summary>
    [Column("sequence_number", TypeName = "int unsigned")]
    public required uint SequenceNumber { get; init; }

    /// <summary>Column: operation_code; SQL: varchar(64); not null.</summary>
    [Column("operation_code", TypeName = "varchar(64)")]
    public required string OperationCode { get; init; }

    /// <summary>Column: operation_name_snapshot; SQL: varchar(256); not null.</summary>
    [Column("operation_name_snapshot", TypeName = "varchar(256)")]
    public required string OperationNameSnapshot { get; init; }

    /// <summary>Column: work_center_ref; SQL: varchar(128); nullable.</summary>
    [Column("work_center_ref", TypeName = "varchar(128)")]
    public string? WorkCenterRef { get; init; }

    /// <summary>Column: status_code; SQL: varchar(20); not null.</summary>
    [Column("status_code", TypeName = "varchar(20)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: started_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("started_at_utc", TypeName = "datetime(6)")]
    public DateTime? StartedAtUtc { get; init; }

    /// <summary>Column: completed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("completed_at_utc", TypeName = "datetime(6)")]
    public DateTime? CompletedAtUtc { get; init; }

    /// <summary>Column: started_by_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("started_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? StartedByPrincipalId { get; init; }

    /// <summary>Column: completed_by_principal_id; SQL: bigint unsigned; nullable.</summary>
    [Column("completed_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? CompletedByPrincipalId { get; init; }

    /// <summary>Column: result_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("result_json", TypeName = "json")]
    public string? ResultJson { get; init; }
}
