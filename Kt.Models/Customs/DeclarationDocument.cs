// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_declaration_document table. Includes all 6 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_declaration_document")]
public sealed record DeclarationDocument
{
    /// <summary>Column: customs_declaration_id; SQL: bigint unsigned; not null.</summary>
    [Column("customs_declaration_id", TypeName = "bigint unsigned")]
    public required ulong CustomsDeclarationId { get; init; }

    /// <summary>Column: file_id; SQL: bigint unsigned; not null. Reference to the stored declaration PDF/XML, assessment, certificate, or broker evidence</summary>
    [Column("file_id", TypeName = "bigint unsigned")]
    public required ulong FileId { get; init; }

    /// <summary>Column: document_role; SQL: varchar(32); not null. DECLARATION, ASSESSMENT, ORIGIN_CERTIFICATE, CUSTOMS_RECEIPT, RELEASE_NOTICE, BROKER_DOCUMENT, or OTHER</summary>
    [Column("document_role", TypeName = "varchar(32)")]
    public required string DocumentRole { get; init; }

    /// <summary>Column: is_primary; SQL: tinyint(1); not null. Primary declaration file for users and exports</summary>
    [Column("is_primary", TypeName = "tinyint(1)")]
    public required bool IsPrimary { get; init; }

    /// <summary>Column: added_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("added_at_utc", TypeName = "datetime(6)")]
    public required DateTime AddedAtUtc { get; init; }

    /// <summary>Column: added_by_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("added_by_user_id", TypeName = "bigint unsigned")]
    public required ulong AddedByUserId { get; init; }
}
