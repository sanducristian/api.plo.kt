// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Customs;

/// <summary>Maps one row of the customs_declaration table. Includes all 24 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("customs_declaration")]
public sealed record Declaration
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID for API and integration use</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: legal_entity_id; SQL: bigint unsigned; not null. PLO entity responsible for or affected by the declaration</summary>
    [Column("legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong LegalEntityId { get; init; }

    /// <summary>Column: declaration_type; SQL: varchar(24); not null. IMPORT, EXPORT, TRANSIT, TEMPORARY_STORAGE, or OTHER</summary>
    [Column("declaration_type", TypeName = "varchar(24)")]
    public required string DeclarationType { get; init; }

    /// <summary>Column: mrn; SQL: varchar(32); nullable. Master Reference Number assigned when an electronic declaration is accepted</summary>
    [Column("mrn", TypeName = "varchar(32)")]
    public string? Mrn { get; init; }

    /// <summary>Column: local_reference_number; SQL: varchar(64); nullable. Declarant local reference used before or alongside the MRN</summary>
    [Column("local_reference_number", TypeName = "varchar(64)")]
    public string? LocalReferenceNumber { get; init; }

    /// <summary>Column: customs_office_id; SQL: bigint unsigned; nullable. Office receiving, supervising, or accepting the declaration</summary>
    [Column("customs_office_id", TypeName = "bigint unsigned")]
    public ulong? CustomsOfficeId { get; init; }

    /// <summary>Column: declarant_registration_id; SQL: bigint unsigned; nullable. EORI or equivalent registration of the declarant</summary>
    [Column("declarant_registration_id", TypeName = "bigint unsigned")]
    public ulong? DeclarantRegistrationId { get; init; }

    /// <summary>Column: importer_registration_id; SQL: bigint unsigned; nullable. EORI or equivalent registration of the importer</summary>
    [Column("importer_registration_id", TypeName = "bigint unsigned")]
    public ulong? ImporterRegistrationId { get; init; }

    /// <summary>Column: exporter_registration_id; SQL: bigint unsigned; nullable. EORI or equivalent registration of the exporter</summary>
    [Column("exporter_registration_id", TypeName = "bigint unsigned")]
    public ulong? ExporterRegistrationId { get; init; }

    /// <summary>Column: customs_procedure_code; SQL: varchar(32); nullable. Procedure/requested-and-previous-procedure code used by the customs declaration</summary>
    [Column("customs_procedure_code", TypeName = "varchar(32)")]
    public string? CustomsProcedureCode { get; init; }

    /// <summary>Column: acceptance_date; SQL: date; nullable. Legal acceptance date used to determine applicable duties and measures</summary>
    [Column("acceptance_date", TypeName = "date")]
    public DateOnly? AcceptanceDate { get; init; }

    /// <summary>Column: release_date; SQL: date; nullable. Date customs released the goods for the declared procedure</summary>
    [Column("release_date", TypeName = "date")]
    public DateOnly? ReleaseDate { get; init; }

    /// <summary>Column: declaration_currency_id; SQL: bigint unsigned; nullable. Currency in which declaration totals are expressed</summary>
    [Column("declaration_currency_id", TypeName = "bigint unsigned")]
    public ulong? DeclarationCurrencyId { get; init; }

    /// <summary>Column: total_customs_value; SQL: decimal(20,6); nullable. Total customs value before duties and import taxes</summary>
    [Column("total_customs_value", TypeName = "decimal(20,6)")]
    public decimal? TotalCustomsValue { get; init; }

    /// <summary>Column: total_duty_amount; SQL: decimal(20,6); nullable. Total assessed customs duties excluding import VAT unless local practice requires otherwise</summary>
    [Column("total_duty_amount", TypeName = "decimal(20,6)")]
    public decimal? TotalDutyAmount { get; init; }

    /// <summary>Column: total_import_tax_amount; SQL: decimal(20,6); nullable. Total import VAT, GST, excise, and similar import taxes</summary>
    [Column("total_import_tax_amount", TypeName = "decimal(20,6)")]
    public decimal? TotalImportTaxAmount { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null. DRAFT, SUBMITTED, ACCEPTED, RELEASED, REJECTED, CANCELLED, or AMENDED</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: external_system; SQL: varchar(64); nullable. Customs, broker, or logistics system that owns the external declaration record</summary>
    [Column("external_system", TypeName = "varchar(64)")]
    public string? ExternalSystem { get; init; }

    /// <summary>Column: external_reference; SQL: varchar(256); nullable. Stable external declaration identifier other than MRN/LRN</summary>
    [Column("external_reference", TypeName = "varchar(256)")]
    public string? ExternalReference { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }

    /// <summary>Column: updated_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_at_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedAtUtc { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null. Incremented by the application for optimistic concurrency</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
