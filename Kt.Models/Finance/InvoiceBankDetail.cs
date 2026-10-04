// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice_bank_detail table. Includes all 25 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice_bank_detail")]
public sealed record InvoiceBankDetail
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: invoice_id; SQL: bigint unsigned; not null.</summary>
    [Column("invoice_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceId { get; init; }

    /// <summary>Column: account_owner_party_id; SQL: bigint unsigned; nullable. Party believed to own the account; normally the issuer for a supplier invoice</summary>
    [Column("account_owner_party_id", TypeName = "bigint unsigned")]
    public ulong? AccountOwnerPartyId { get; init; }

    /// <summary>Column: party_bank_account_id; SQL: bigint unsigned; nullable. Verified master account when the captured details match; NULL keeps unverified invoice evidence separate</summary>
    [Column("party_bank_account_id", TypeName = "bigint unsigned")]
    public ulong? PartyBankAccountId { get; init; }

    /// <summary>Column: bank_detail_role; SQL: varchar(32); not null. ISSUER_COLLECTION, RECIPIENT_ACCOUNT, REFUND_ACCOUNT, or INFORMATION_ONLY</summary>
    [Column("bank_detail_role", TypeName = "varchar(32)")]
    public required string BankDetailRole { get; init; }

    /// <summary>Column: account_holder_name_snapshot; SQL: varchar(256); nullable. Account-holder name exactly as printed or transmitted with the invoice</summary>
    [Column("account_holder_name_snapshot", TypeName = "varchar(256)")]
    public string? AccountHolderNameSnapshot { get; init; }

    /// <summary>Column: bank_name_snapshot; SQL: varchar(256); nullable. Financial-institution name exactly as provided by the invoice</summary>
    [Column("bank_name_snapshot", TypeName = "varchar(256)")]
    public string? BankNameSnapshot { get; init; }

    /// <summary>Column: account_country_code; SQL: char(2); nullable. ISO country inferred from the identifier or supplied by the document</summary>
    [Column("account_country_code", TypeName = "char(2)")]
    public string? AccountCountryCode { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; nullable. Currency indicated for this account; NULL when the invoice does not specify one</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public ulong? CurrencyId { get; init; }

    /// <summary>Column: iban_ciphertext; SQL: varbinary(512); nullable. Encrypted IBAN captured from the invoice; never store plaintext bank identifiers</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("iban_ciphertext", TypeName = "varbinary(512)")]
    public byte[]? IbanCiphertext { get; init; }

    /// <summary>Column: iban_hash; SQL: binary(32); nullable. Keyed normalized comparison value used to match a verified party bank account</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("iban_hash", TypeName = "binary(32)")]
    public byte[]? IbanHash { get; init; }

    /// <summary>Column: iban_masked; SQL: varchar(64); nullable. Safe display form such as RO49...1234</summary>
    [Column("iban_masked", TypeName = "varchar(64)")]
    public string? IbanMasked { get; init; }

    /// <summary>Column: local_account_ciphertext; SQL: varbinary(512); nullable. Encrypted non-IBAN account or wallet identifier captured from the invoice</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("local_account_ciphertext", TypeName = "varbinary(512)")]
    public byte[]? LocalAccountCiphertext { get; init; }

    /// <summary>Column: local_account_hash; SQL: binary(32); nullable. Keyed normalized comparison value for the local account identifier</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("local_account_hash", TypeName = "binary(32)")]
    public byte[]? LocalAccountHash { get; init; }

    /// <summary>Column: local_account_masked; SQL: varchar(64); nullable. Safe display form of the local account identifier</summary>
    [Column("local_account_masked", TypeName = "varchar(64)")]
    public string? LocalAccountMasked { get; init; }

    /// <summary>Column: bic_snapshot; SQL: varchar(11); nullable. BIC/SWIFT printed or transmitted with the invoice</summary>
    [Column("bic_snapshot", TypeName = "varchar(11)")]
    public string? BicSnapshot { get; init; }

    /// <summary>Column: routing_code_ciphertext; SQL: varbinary(256); nullable. Encrypted routing/ABA/sort/branch code captured from the invoice</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("routing_code_ciphertext", TypeName = "varbinary(256)")]
    public byte[]? RoutingCodeCiphertext { get; init; }

    /// <summary>Column: routing_code_masked; SQL: varchar(64); nullable.</summary>
    [Column("routing_code_masked", TypeName = "varchar(64)")]
    public string? RoutingCodeMasked { get; init; }

    /// <summary>Column: source_code; SQL: varchar(24); not null. PDF_OCR, E_INVOICE, MANUAL, API, or LEGACY_IMPORT</summary>
    [Column("source_code", TypeName = "varchar(24)")]
    public required string SourceCode { get; init; }

    /// <summary>Column: source_locator_json; SQL: json; nullable. OCR page/bounding box or structured-document path proving where each value came from</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("source_locator_json", TypeName = "json")]
    public string? SourceLocatorJson { get; init; }

    /// <summary>Column: confidence_score; SQL: decimal(5,4); nullable. Extraction/matching confidence from 0.0000 to 1.0000</summary>
    [Column("confidence_score", TypeName = "decimal(5,4)")]
    public decimal? ConfidenceScore { get; init; }

    /// <summary>Column: validation_status; SQL: varchar(24); not null. UNVERIFIED, MATCHED, VERIFIED, MISMATCH, REJECTED, or HISTORICAL</summary>
    [Column("validation_status", TypeName = "varchar(24)")]
    public required string ValidationStatus { get; init; }

    /// <summary>Column: is_primary; SQL: tinyint(1); not null. Preferred account displayed for this invoice; several bank-detail rows remain allowed</summary>
    [Column("is_primary", TypeName = "tinyint(1)")]
    public required bool IsPrimary { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null. New Identity-module user ID or reserved system identity</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }
}
