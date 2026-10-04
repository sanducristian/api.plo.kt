// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Devices;

/// <summary>Maps one row of the device_ownership_transfer table. Includes all 19 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("device_ownership_transfer")]
public sealed record DeviceOwnershipTransfer
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: uuid; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("uuid", TypeName = "binary(16)")]
    public required byte[] Uuid { get; init; }

    /// <summary>Column: device_module_ref; SQL: varchar(128); not null. Stable case-sensitive reference owned by the Device module.</summary>
    [Column("device_module_ref", TypeName = "varchar(128)")]
    public required string DeviceModuleRef { get; init; }

    /// <summary>Column: current_owner_workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("current_owner_workspace_id", TypeName = "bigint unsigned")]
    public required ulong CurrentOwnerWorkspaceId { get; init; }

    /// <summary>Column: proposed_owner_workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("proposed_owner_workspace_id", TypeName = "bigint unsigned")]
    public required ulong ProposedOwnerWorkspaceId { get; init; }

    /// <summary>Column: initiator_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("initiator_principal_id", TypeName = "bigint unsigned")]
    public required ulong InitiatorPrincipalId { get; init; }

    /// <summary>Column: status; SQL: varchar(32); not null.</summary>
    [Column("status", TypeName = "varchar(32)")]
    public required string Status { get; init; }

    /// <summary>Column: transfer_reason; SQL: varchar(255); nullable.</summary>
    [Column("transfer_reason", TypeName = "varchar(255)")]
    public string? TransferReason { get; init; }

    /// <summary>Column: prior_ownership_snapshot_ref; SQL: varchar(128); nullable.</summary>
    [Column("prior_ownership_snapshot_ref", TypeName = "varchar(128)")]
    public string? PriorOwnershipSnapshotRef { get; init; }

    /// <summary>Column: post_ownership_snapshot_ref; SQL: varchar(128); nullable.</summary>
    [Column("post_ownership_snapshot_ref", TypeName = "varchar(128)")]
    public string? PostOwnershipSnapshotRef { get; init; }

    /// <summary>Column: correlation_id; SQL: varchar(128); nullable.</summary>
    [Column("correlation_id", TypeName = "varchar(128)")]
    public string? CorrelationId { get; init; }

    /// <summary>Column: requested_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("requested_utc", TypeName = "datetime(6)")]
    public required DateTime RequestedUtc { get; init; }

    /// <summary>Column: approved_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("approved_utc", TypeName = "datetime(6)")]
    public DateTime? ApprovedUtc { get; init; }

    /// <summary>Column: rejected_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("rejected_utc", TypeName = "datetime(6)")]
    public DateTime? RejectedUtc { get; init; }

    /// <summary>Column: cancelled_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("cancelled_utc", TypeName = "datetime(6)")]
    public DateTime? CancelledUtc { get; init; }

    /// <summary>Column: expired_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("expired_utc", TypeName = "datetime(6)")]
    public DateTime? ExpiredUtc { get; init; }

    /// <summary>Column: completed_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("completed_utc", TypeName = "datetime(6)")]
    public DateTime? CompletedUtc { get; init; }

    /// <summary>Column: approved_by_principal_id; SQL: bigint unsigned; nullable. Principal that approved the ownership transfer under the applicable policy.</summary>
    [Column("approved_by_principal_id", TypeName = "bigint unsigned")]
    public ulong? ApprovedByPrincipalId { get; init; }

    /// <summary>Column: active_device_module_ref; SQL: varchar(128); nullable. Generated key allowing only one active transfer workflow for a device.</summary>
    /// <remarks>Database-generated value; exclude from INSERT and UPDATE statements.</remarks>
    [Column("active_device_module_ref", TypeName = "varchar(128)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string? ActiveDeviceModuleRef { get; init; }
}
