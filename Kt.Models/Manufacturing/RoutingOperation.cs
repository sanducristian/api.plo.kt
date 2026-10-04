// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Manufacturing;

/// <summary>Maps one row of the mfg_routing_operation table. Includes all 10 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("mfg_routing_operation")]
public sealed record RoutingOperation
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: routing_id; SQL: bigint unsigned; not null.</summary>
    [Column("routing_id", TypeName = "bigint unsigned")]
    public required ulong RoutingId { get; init; }

    /// <summary>Column: sequence_number; SQL: int unsigned; not null.</summary>
    [Column("sequence_number", TypeName = "int unsigned")]
    public required uint SequenceNumber { get; init; }

    /// <summary>Column: operation_code; SQL: varchar(64); not null.</summary>
    [Column("operation_code", TypeName = "varchar(64)")]
    public required string OperationCode { get; init; }

    /// <summary>Column: operation_name; SQL: varchar(256); not null.</summary>
    [Column("operation_name", TypeName = "varchar(256)")]
    public required string OperationName { get; init; }

    /// <summary>Column: work_center_ref; SQL: varchar(128); nullable. Stable reference until the work-center master is introduced</summary>
    [Column("work_center_ref", TypeName = "varchar(128)")]
    public string? WorkCenterRef { get; init; }

    /// <summary>Column: setup_duration_minutes; SQL: decimal(12,3); not null.</summary>
    [Column("setup_duration_minutes", TypeName = "decimal(12,3)")]
    public required decimal SetupDurationMinutes { get; init; }

    /// <summary>Column: run_duration_minutes; SQL: decimal(12,3); not null.</summary>
    [Column("run_duration_minutes", TypeName = "decimal(12,3)")]
    public required decimal RunDurationMinutes { get; init; }

    /// <summary>Column: inspection_required; SQL: tinyint(1); not null.</summary>
    [Column("inspection_required", TypeName = "tinyint(1)")]
    public required bool InspectionRequired { get; init; }

    /// <summary>Column: instructions_json; SQL: json; nullable.</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("instructions_json", TypeName = "json")]
    public string? InstructionsJson { get; init; }
}
