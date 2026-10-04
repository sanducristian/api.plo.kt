// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Accounting;

/// <summary>Maps one row of the acc_accounting_period table. Includes all 8 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("acc_accounting_period")]
public sealed record AccountingPeriod {
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null.</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: period_code; SQL: varchar(16); not null.</summary>
    [Column("period_code", TypeName = "varchar(16)")]
    public required string PeriodCode { get; init; }

    /// <summary>Column: start_date; SQL: date; not null.</summary>
    [Column("start_date", TypeName = "date")]
    public required DateOnly StartDate { get; init; }

    /// <summary>Column: end_date; SQL: date; not null.</summary>
    [Column("end_date", TypeName = "date")]
    public required DateOnly EndDate { get; init; }

    /// <summary>Column: status_code; SQL: varchar(16); not null.</summary>
    [Column("status_code", TypeName = "varchar(16)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: closed_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("closed_at_utc", TypeName = "datetime(6)")]
    public DateTime? ClosedAtUtc { get; init; }

    /// <summary>Column: closed_by_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("closed_by_user_id", TypeName = "bigint unsigned")]
    public ulong? ClosedByUserId { get; init; }
}
