// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_business_partner table. Includes all 11 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_business_partner")]
public sealed record BusinessPartner
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: party_id; SQL: bigint unsigned; not null. External or internal party acting as supplier, customer, or both</summary>
    [Column("party_id", TypeName = "bigint unsigned")]
    public required ulong PartyId { get; init; }

    /// <summary>Column: partner_code; SQL: varchar(64); not null. Stable operational code used in procurement, sales, and integrations</summary>
    [Column("partner_code", TypeName = "varchar(64)")]
    public required string PartnerCode { get; init; }

    /// <summary>Column: default_currency_id; SQL: bigint unsigned; nullable.</summary>
    [Column("default_currency_id", TypeName = "bigint unsigned")]
    public ulong? DefaultCurrencyId { get; init; }

    /// <summary>Column: default_payment_term_id; SQL: bigint unsigned; nullable. Added as a foreign key after acc_payment_term is created</summary>
    [Column("default_payment_term_id", TypeName = "bigint unsigned")]
    public ulong? DefaultPaymentTermId { get; init; }

    /// <summary>Column: is_supplier; SQL: tinyint(1); not null.</summary>
    [Column("is_supplier", TypeName = "tinyint(1)")]
    public required bool IsSupplier { get; init; }

    /// <summary>Column: is_customer; SQL: tinyint(1); not null.</summary>
    [Column("is_customer", TypeName = "tinyint(1)")]
    public required bool IsCustomer { get; init; }

    /// <summary>Column: is_active; SQL: tinyint(1); not null. Partner relationship state; the underlying party may remain active in other workspaces</summary>
    [Column("is_active", TypeName = "tinyint(1)")]
    public required bool IsActive { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: updated_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_at_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedAtUtc { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
