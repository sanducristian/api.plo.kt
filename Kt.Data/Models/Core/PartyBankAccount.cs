// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Core;

/// <summary>Maps one row of the core_party_bank_account table. Includes all 29 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("core_party_bank_account")]
public sealed record PartyBankAccount
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID exposed by APIs</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: party_id; SQL: bigint unsigned; not null. Company, supplier, customer, or person that owns the account</summary>
    [Column("party_id", TypeName = "bigint unsigned")]
    public required ulong PartyId { get; init; }

    /// <summary>Column: financial_institution_id; SQL: bigint unsigned; nullable. Matched institution; snapshots remain on invoices and statement imports</summary>
    [Column("financial_institution_id", TypeName = "bigint unsigned")]
    public ulong? FinancialInstitutionId { get; init; }

    /// <summary>Column: account_holder_name; SQL: varchar(256); not null. Account-holder name as verified with the bank or supplied evidence</summary>
    [Column("account_holder_name", TypeName = "varchar(256)")]
    public required string AccountHolderName { get; init; }

    /// <summary>Column: account_country_code; SQL: char(2); nullable. ISO country governing the account identifier and clearing rules</summary>
    [Column("account_country_code", TypeName = "char(2)")]
    public string? AccountCountryCode { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; nullable. Fixed account currency; NULL only for a documented multi-currency account</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public ulong? CurrencyId { get; init; }

    /// <summary>Column: account_type; SQL: varchar(24); not null. CURRENT, SAVINGS, CARD, WALLET, ESCROW, VIRTUAL, or OTHER</summary>
    [Column("account_type", TypeName = "varchar(24)")]
    public required string AccountType { get; init; }

    /// <summary>Column: usage_code; SQL: varchar(24); not null. COLLECTION, DISBURSEMENT, BOTH, or INFORMATION_ONLY</summary>
    [Column("usage_code", TypeName = "varchar(24)")]
    public required string UsageCode { get; init; }

    /// <summary>Column: iban_ciphertext; SQL: varbinary(512); nullable. Encrypted IBAN; encryption keys are held outside the database</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("iban_ciphertext", TypeName = "varbinary(512)")]
    public byte[]? IbanCiphertext { get; init; }

    /// <summary>Column: iban_hash; SQL: binary(32); nullable. Keyed SHA-256/HMAC comparison value for duplicate detection without decrypting the IBAN</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("iban_hash", TypeName = "binary(32)")]
    public byte[]? IbanHash { get; init; }

    /// <summary>Column: iban_masked; SQL: varchar(64); nullable. Safe display form such as FR76...1234; never use it to execute a transfer</summary>
    [Column("iban_masked", TypeName = "varchar(64)")]
    public string? IbanMasked { get; init; }

    /// <summary>Column: local_account_ciphertext; SQL: varbinary(512); nullable. Encrypted non-IBAN account number or wallet identifier</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("local_account_ciphertext", TypeName = "varbinary(512)")]
    public byte[]? LocalAccountCiphertext { get; init; }

    /// <summary>Column: local_account_hash; SQL: binary(32); nullable. Keyed comparison value for a normalized local account identifier</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("local_account_hash", TypeName = "binary(32)")]
    public byte[]? LocalAccountHash { get; init; }

    /// <summary>Column: local_account_masked; SQL: varchar(64); nullable. Safe display form of the local account identifier</summary>
    [Column("local_account_masked", TypeName = "varchar(64)")]
    public string? LocalAccountMasked { get; init; }

    /// <summary>Column: bic_snapshot; SQL: varchar(11); nullable. BIC/SWIFT supplied with this account even when no institution record is matched</summary>
    [Column("bic_snapshot", TypeName = "varchar(11)")]
    public string? BicSnapshot { get; init; }

    /// <summary>Column: routing_code_ciphertext; SQL: varbinary(256); nullable. Encrypted routing/ABA/sort/branch code when it is sensitive in the relevant jurisdiction</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("routing_code_ciphertext", TypeName = "varbinary(256)")]
    public byte[]? RoutingCodeCiphertext { get; init; }

    /// <summary>Column: routing_code_masked; SQL: varchar(64); nullable. Safe display form of the routing code</summary>
    [Column("routing_code_masked", TypeName = "varchar(64)")]
    public string? RoutingCodeMasked { get; init; }

    /// <summary>Column: status_code; SQL: varchar(24); not null. UNVERIFIED, PENDING_VERIFICATION, VERIFIED, REJECTED, SUSPENDED, or CLOSED</summary>
    [Column("status_code", TypeName = "varchar(24)")]
    public required string StatusCode { get; init; }

    /// <summary>Column: verification_method; SQL: varchar(32); nullable. BANK_DOCUMENT, OPEN_BANKING, MICRO_DEPOSIT, MANUAL_CONTROL, IMPORTED, or OTHER</summary>
    [Column("verification_method", TypeName = "varchar(32)")]
    public string? VerificationMethod { get; init; }

    /// <summary>Column: verification_reference; SQL: varchar(512); nullable. Stable evidence/document/API reference; never a credential or access token</summary>
    [Column("verification_reference", TypeName = "varchar(512)")]
    public string? VerificationReference { get; init; }

    /// <summary>Column: verified_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("verified_at_utc", TypeName = "datetime(6)")]
    public DateTime? VerifiedAtUtc { get; init; }

    /// <summary>Column: valid_from; SQL: date; nullable.</summary>
    [Column("valid_from", TypeName = "date")]
    public DateOnly? ValidFrom { get; init; }

    /// <summary>Column: valid_to; SQL: date; nullable.</summary>
    [Column("valid_to", TypeName = "date")]
    public DateOnly? ValidTo { get; init; }

    /// <summary>Column: is_default; SQL: tinyint(1); not null. Preferred account for this party/usage/currency; uniqueness is enforced by the application</summary>
    [Column("is_default", TypeName = "tinyint(1)")]
    public required bool IsDefault { get; init; }

    /// <summary>Column: created_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("created_at_utc", TypeName = "datetime(6)")]
    public required DateTime CreatedAtUtc { get; init; }

    /// <summary>Column: created_by_user_id; SQL: bigint unsigned; not null. New Identity-module user ID</summary>
    [Column("created_by_user_id", TypeName = "bigint unsigned")]
    public required ulong CreatedByUserId { get; init; }

    /// <summary>Column: updated_at_utc; SQL: datetime(6); not null.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("updated_at_utc", TypeName = "datetime(6)")]
    public required DateTime UpdatedAtUtc { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
