// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_approval_event table. Includes all 9 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_approval_event")]
public sealed record InvoiceApprovalEvent
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceId { get; init; }

    /// <summary>Column: step_number; SQL: smallint unsigned; not null.</summary>
    [Column("step_number", TypeName = "smallint unsigned")]
    public required ushort StepNumber { get; init; }

    /// <summary>Column: action_code; SQL: varchar(24); not null.</summary>
    [Column("action_code", TypeName = "varchar(24)")]
    public required string ActionCode { get; init; }

    /// <summary>Column: actor_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("actor_user_id", TypeName = "bigint unsigned")]
    public ulong? ActorUserId { get; init; }

    /// <summary>Column: actor_role_code; SQL: varchar(64); nullable.</summary>
    [Column("actor_role_code", TypeName = "varchar(64)")]
    public string? ActorRoleCode { get; init; }

    /// <summary>Column: delegated_from_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("delegated_from_user_id", TypeName = "bigint unsigned")]
    public ulong? DelegatedFromUserId { get; init; }

    /// <summary>Column: comment; SQL: varchar(1024); nullable.</summary>
    [Column("comment", TypeName = "varchar(1024)")]
    public string? Comment { get; init; }

    /// <summary>Column: occurred_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("occurred_at_utc", TypeName = "datetime(6)")]
    public required DateTime OccurredAtUtc { get; init; }
}
