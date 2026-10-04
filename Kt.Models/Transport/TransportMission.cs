// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Transport;

/// <summary>Maps one row of the transport_mission table. Includes all 24 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("transport_mission")]
public sealed record TransportMission
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null.</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: workspace_id; SQL: bigint unsigned; not null.</summary>
    [Column("workspace_id", TypeName = "bigint unsigned")]
    public required ulong WorkspaceId { get; init; }

    /// <summary>Column: mission_code; SQL: varchar(64); not null.</summary>
    [Column("mission_code", TypeName = "varchar(64)")]
    public required string MissionCode { get; init; }

    /// <summary>Column: requester_party_id; SQL: bigint unsigned; not null. Company ordering the service</summary>
    [Column("requester_party_id", TypeName = "bigint unsigned")]
    public required ulong RequesterPartyId { get; init; }

    /// <summary>Column: sender_party_id; SQL: bigint unsigned; not null. Company C dispatching goods or passengers</summary>
    [Column("sender_party_id", TypeName = "bigint unsigned")]
    public required ulong SenderPartyId { get; init; }

    /// <summary>Column: recipient_party_id; SQL: bigint unsigned; not null. Company D receiving goods or passengers</summary>
    [Column("recipient_party_id", TypeName = "bigint unsigned")]
    public required ulong RecipientPartyId { get; init; }

    /// <summary>Column: carrier_party_id; SQL: bigint unsigned; not null. Company operating the transport</summary>
    [Column("carrier_party_id", TypeName = "bigint unsigned")]
    public required ulong CarrierPartyId { get; init; }

    /// <summary>Column: service_type; SQL: varchar(24); not null.</summary>
    [Column("service_type", TypeName = "varchar(24)")]
    public required string ServiceType { get; init; }

    /// <summary>Column: required_platform_type; SQL: varchar(24); nullable.</summary>
    [Column("required_platform_type", TypeName = "varchar(24)")]
    public string? RequiredPlatformType { get; init; }

    /// <summary>Column: requested_pickup_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("requested_pickup_at_utc", TypeName = "datetime(6)")]
    public DateTime? RequestedPickupAtUtc { get; init; }

    /// <summary>Column: requested_delivery_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("requested_delivery_at_utc", TypeName = "datetime(6)")]
    public DateTime? RequestedDeliveryAtUtc { get; init; }

    /// <summary>Column: planned_start_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("planned_start_at_utc", TypeName = "datetime(6)")]
    public required DateTime PlannedStartAtUtc { get; init; }

    /// <summary>Column: planned_end_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("planned_end_at_utc", TypeName = "datetime(6)")]
    public required DateTime PlannedEndAtUtc { get; init; }

    /// <summary>Column: actual_start_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("actual_start_at_utc", TypeName = "datetime(6)")]
    public DateTime? ActualStartAtUtc { get; init; }

    /// <summary>Column: actual_end_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("actual_end_at_utc", TypeName = "datetime(6)")]
    public DateTime? ActualEndAtUtc { get; init; }

    /// <summary>Column: passenger_count; SQL: smallint unsigned; not null.</summary>
    [Column("passenger_count", TypeName = "smallint unsigned")]
    public required ushort PassengerCount { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null.</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: priority_code; SQL: varchar(16); not null.</summary>
    [Column("priority_code", TypeName = "varchar(16)")]
    public required string PriorityCode { get; init; }

    /// <summary>Column: idempotency_key; SQL: varchar(128); not null.</summary>
    [Column("idempotency_key", TypeName = "varchar(128)")]
    public required string IdempotencyKey { get; init; }

    /// <summary>Column: instructions; SQL: text; nullable.</summary>
    [Column("instructions", TypeName = "text")]
    public string? Instructions { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_principal_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_principal_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByPrincipalId { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
