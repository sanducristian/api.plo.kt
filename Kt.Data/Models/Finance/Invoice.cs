// Source: PLO_Complete_Database_Project_Reference_2026-09-26.md, section 8.
// Database row snapshot, not an API contract or INSERT request. SQL defaults are not applied in C#.
#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kt.Data.Models.Finance;

/// <summary>Maps one row of the finance_invoice table. Includes all 60 physical columns.</summary>
/// <remarks>Explicit repository mapping is required; annotations do not execute SQL or enforce permissions.</remarks>
[Table("finance_invoice")]
public sealed record Invoice
{
    /// <summary>Column: id; SQL: bigint unsigned; not null.</summary>
    [Column("id", TypeName = "bigint unsigned")]
    public required ulong Id { get; init; }

    /// <summary>Column: public_id; SQL: binary(16); not null. Application-generated UUID stored in binary form</summary>
    /// <remarks>Raw database bytes; no UUID byte-order assumption. Array equality is reference-based.</remarks>
    [Column("public_id", TypeName = "binary(16)")]
    public required byte[] PublicId { get; init; }

    /// <summary>Column: accounting_legal_entity_id; SQL: bigint unsigned; not null. Legal entity whose books contain this invoice and its journal entry</summary>
    [Column("accounting_legal_entity_id", TypeName = "bigint unsigned")]
    public required ulong AccountingLegalEntityId { get; init; }

    /// <summary>Column: issuer_party_id; SQL: bigint unsigned; not null. Party legally issuing the invoice</summary>
    [Column("issuer_party_id", TypeName = "bigint unsigned")]
    public required ulong IssuerPartyId { get; init; }

    /// <summary>Column: recipient_party_id; SQL: bigint unsigned; not null. Party receiving the goods/services or named as legal recipient</summary>
    [Column("recipient_party_id", TypeName = "bigint unsigned")]
    public required ulong RecipientPartyId { get; init; }

    /// <summary>Column: invoice_recipient_party_id; SQL: bigint unsigned; not null. Party responsible for receiving/processing the invoice; may differ from recipient_party_id</summary>
    [Column("invoice_recipient_party_id", TypeName = "bigint unsigned")]
    public required ulong InvoiceRecipientPartyId { get; init; }

    /// <summary>Column: invoice_import_id; SQL: bigint unsigned; nullable. Capture/OCR/e-invoice intake record that created this draft</summary>
    [Column("invoice_import_id", TypeName = "bigint unsigned")]
    public ulong? InvoiceImportId { get; init; }

    /// <summary>Column: number_sequence_id; SQL: bigint unsigned; nullable. Sequence used for PLO-controlled numbering; required by policy for outbound documents</summary>
    [Column("number_sequence_id", TypeName = "bigint unsigned")]
    public ulong? NumberSequenceId { get; init; }

    /// <summary>Column: invoice_type; SQL: varchar(32); not null. Commercial direction and document kind, including purchase/sales credit notes</summary>
    [Column("invoice_type", TypeName = "varchar(32)")]
    public required string InvoiceType { get; init; }

    /// <summary>Column: document_number; SQL: varchar(128); not null. Legal invoice number exactly as printed, issued, or received</summary>
    [Column("document_number", TypeName = "varchar(128)")]
    public required string DocumentNumber { get; init; }

    /// <summary>Column: document_number_normalized; SQL: varchar(128); not null. Comparison form used for duplicate detection; the displayed number remains unchanged</summary>
    [Column("document_number_normalized", TypeName = "varchar(128)")]
    public required string DocumentNumberNormalized { get; init; }

    /// <summary>Column: duplicate_occurrence_no; SQL: smallint unsigned; not null. Values above 1 require an audited duplicate override</summary>
    [Column("duplicate_occurrence_no", TypeName = "smallint unsigned")]
    public required ushort DuplicateOccurrenceNo { get; init; }

    /// <summary>Column: internal_number; SQL: varchar(64); nullable. PLO controlled number assigned at posting when required</summary>
    [Column("internal_number", TypeName = "varchar(64)")]
    public string? InternalNumber { get; init; }

    /// <summary>Column: issue_date; SQL: date; not null.</summary>
    [Column("issue_date", TypeName = "date")]
    public required DateOnly IssueDate { get; init; }

    /// <summary>Column: received_date; SQL: date; nullable.</summary>
    [Column("received_date", TypeName = "date")]
    public DateOnly? ReceivedDate { get; init; }

    /// <summary>Column: accounting_date; SQL: date; nullable.</summary>
    [Column("accounting_date", TypeName = "date")]
    public DateOnly? AccountingDate { get; init; }

    /// <summary>Column: due_date; SQL: date; nullable.</summary>
    [Column("due_date", TypeName = "date")]
    public DateOnly? DueDate { get; init; }

    /// <summary>Column: payment_term_id; SQL: bigint unsigned; nullable.</summary>
    [Column("payment_term_id", TypeName = "bigint unsigned")]
    public ulong? PaymentTermId { get; init; }

    /// <summary>Column: currency_id; SQL: bigint unsigned; not null.</summary>
    [Column("currency_id", TypeName = "bigint unsigned")]
    public required ulong CurrencyId { get; init; }

    /// <summary>Column: exchange_rate_to_book_currency; SQL: decimal(20,10); nullable. Multiplier from invoice currency to the legal entity book currency</summary>
    [Column("exchange_rate_to_book_currency", TypeName = "decimal(20,10)")]
    public decimal? ExchangeRateToBookCurrency { get; init; }

    /// <summary>Column: exchange_rate_date; SQL: date; nullable. Date whose official or approved exchange rate was used</summary>
    [Column("exchange_rate_date", TypeName = "date")]
    public DateOnly? ExchangeRateDate { get; init; }

    /// <summary>Column: purchase_order_reference; SQL: varchar(128); nullable.</summary>
    [Column("purchase_order_reference", TypeName = "varchar(128)")]
    public string? PurchaseOrderReference { get; init; }

    /// <summary>Column: customer_reference; SQL: varchar(128); nullable.</summary>
    [Column("customer_reference", TypeName = "varchar(128)")]
    public string? CustomerReference { get; init; }

    /// <summary>Column: contract_reference; SQL: varchar(128); nullable.</summary>
    [Column("contract_reference", TypeName = "varchar(128)")]
    public string? ContractReference { get; init; }

    /// <summary>Column: issuer_name_snapshot; SQL: varchar(256); not null. Issuer name preserved exactly for the posted legal record</summary>
    [Column("issuer_name_snapshot", TypeName = "varchar(256)")]
    public required string IssuerNameSnapshot { get; init; }

    /// <summary>Column: issuer_tax_id_snapshot; SQL: varchar(64); nullable. Issuer tax identifier printed on the document</summary>
    [Column("issuer_tax_id_snapshot", TypeName = "varchar(64)")]
    public string? IssuerTaxIdSnapshot { get; init; }

    /// <summary>Column: issuer_address_snapshot_json; SQL: json; nullable. Structured issuer address preserved independently of later partner-master changes</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("issuer_address_snapshot_json", TypeName = "json")]
    public string? IssuerAddressSnapshotJson { get; init; }

    /// <summary>Column: recipient_name_snapshot; SQL: varchar(256); not null. Recipient name preserved exactly for the posted legal record</summary>
    [Column("recipient_name_snapshot", TypeName = "varchar(256)")]
    public required string RecipientNameSnapshot { get; init; }

    /// <summary>Column: recipient_tax_id_snapshot; SQL: varchar(64); nullable. Recipient tax identifier printed on the document</summary>
    [Column("recipient_tax_id_snapshot", TypeName = "varchar(64)")]
    public string? RecipientTaxIdSnapshot { get; init; }

    /// <summary>Column: recipient_address_snapshot_json; SQL: json; nullable. Structured recipient address preserved independently of later master-data changes</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("recipient_address_snapshot_json", TypeName = "json")]
    public string? RecipientAddressSnapshotJson { get; init; }

    /// <summary>Column: invoice_recipient_name_snapshot; SQL: varchar(256); not null. Invoice-processing/bill-to party name preserved on the legal record</summary>
    [Column("invoice_recipient_name_snapshot", TypeName = "varchar(256)")]
    public required string InvoiceRecipientNameSnapshot { get; init; }

    /// <summary>Column: invoice_recipient_tax_id_snapshot; SQL: varchar(64); nullable. Invoice-recipient tax identifier printed on the document</summary>
    [Column("invoice_recipient_tax_id_snapshot", TypeName = "varchar(64)")]
    public string? InvoiceRecipientTaxIdSnapshot { get; init; }

    /// <summary>Column: invoice_recipient_address_snapshot_json; SQL: json; nullable. Bill-to/invoice-recipient address preserved independently of master-data changes</summary>
    /// <remarks>JSON text; parse explicitly when needed.</remarks>
    [Column("invoice_recipient_address_snapshot_json", TypeName = "json")]
    public string? InvoiceRecipientAddressSnapshotJson { get; init; }

    /// <summary>Column: subtotal_amount; SQL: decimal(20,6); not null.</summary>
    [Column("subtotal_amount", TypeName = "decimal(20,6)")]
    public required decimal SubtotalAmount { get; init; }

    /// <summary>Column: discount_amount; SQL: decimal(20,6); not null.</summary>
    [Column("discount_amount", TypeName = "decimal(20,6)")]
    public required decimal DiscountAmount { get; init; }

    /// <summary>Column: charge_amount; SQL: decimal(20,6); not null.</summary>
    [Column("charge_amount", TypeName = "decimal(20,6)")]
    public required decimal ChargeAmount { get; init; }

    /// <summary>Column: tax_amount; SQL: decimal(20,6); not null.</summary>
    [Column("tax_amount", TypeName = "decimal(20,6)")]
    public required decimal TaxAmount { get; init; }

    /// <summary>Column: rounding_amount; SQL: decimal(20,6); not null.</summary>
    [Column("rounding_amount", TypeName = "decimal(20,6)")]
    public required decimal RoundingAmount { get; init; }

    /// <summary>Column: total_amount; SQL: decimal(20,6); not null.</summary>
    [Column("total_amount", TypeName = "decimal(20,6)")]
    public required decimal TotalAmount { get; init; }

    /// <summary>Column: prepaid_amount; SQL: decimal(20,6); not null.</summary>
    [Column("prepaid_amount", TypeName = "decimal(20,6)")]
    public required decimal PrepaidAmount { get; init; }

    /// <summary>Column: payable_amount; SQL: decimal(20,6); not null.</summary>
    [Column("payable_amount", TypeName = "decimal(20,6)")]
    public required decimal PayableAmount { get; init; }

    /// <summary>Column: workflow_status; SQL: varchar(24); not null. Human review and approval state; independent from settlement and posting</summary>
    [Column("workflow_status", TypeName = "varchar(24)")]
    public required string WorkflowStatus { get; init; }

    /// <summary>Column: matching_status; SQL: varchar(24); not null. PO/receipt/tracked-item matching state</summary>
    [Column("matching_status", TypeName = "varchar(24)")]
    public required string MatchingStatus { get; init; }

    /// <summary>Column: settlement_status; SQL: varchar(24); not null. Open-item state for future bank/payment reconciliation; no payment execution is implemented in this version</summary>
    [Column("settlement_status", TypeName = "varchar(24)")]
    public required string SettlementStatus { get; init; }

    /// <summary>Column: posting_status; SQL: varchar(24); not null. Accounting posting state; posted data is immutable</summary>
    [Column("posting_status", TypeName = "varchar(24)")]
    public required string PostingStatus { get; init; }

    /// <summary>Column: posting_journal_entry_id; SQL: bigint unsigned; nullable. Balanced journal entry created when this invoice is posted</summary>
    [Column("posting_journal_entry_id", TypeName = "bigint unsigned")]
    public ulong? PostingJournalEntryId { get; init; }

    /// <summary>Column: is_disputed; SQL: tinyint(1); not null.</summary>
    [Column("is_disputed", TypeName = "tinyint(1)")]
    public required bool IsDisputed { get; init; }

    /// <summary>Column: dispute_reason_code; SQL: varchar(64); nullable.</summary>
    [Column("dispute_reason_code", TypeName = "varchar(64)")]
    public string? DisputeReasonCode { get; init; }

    /// <summary>Column: notes; SQL: text; nullable.</summary>
    [Column("notes", TypeName = "text")]
    public string? Notes { get; init; }

    /// <summary>Column: posted_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("posted_at_utc", TypeName = "datetime(6)")]
    public DateTime? PostedAtUtc { get; init; }

    /// <summary>Column: posted_by_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("posted_by_user_id", TypeName = "bigint unsigned")]
    public ulong? PostedByUserId { get; init; }

    /// <summary>Column: voided_at_utc; SQL: datetime(6); nullable.</summary>
    /// <remarks>Preserves database date/time; apply the column's documented timezone convention in the mapper.</remarks>
    [Column("voided_at_utc", TypeName = "datetime(6)")]
    public DateTime? VoidedAtUtc { get; init; }

    /// <summary>Column: voided_by_user_id; SQL: bigint unsigned; nullable.</summary>
    [Column("voided_by_user_id", TypeName = "bigint unsigned")]
    public ulong? VoidedByUserId { get; init; }

    /// <summary>Column: void_reason; SQL: varchar(512); nullable.</summary>
    [Column("void_reason", TypeName = "varchar(512)")]
    public string? VoidReason { get; init; }

    /// <summary>Column: legacy_invoice_id; SQL: bigint unsigned; nullable.</summary>
    [Column("legacy_invoice_id", TypeName = "bigint unsigned")]
    public ulong? LegacyInvoiceId { get; init; }

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

    /// <summary>Column: updated_by_user_id; SQL: bigint unsigned; not null.</summary>
    [Column("updated_by_user_id", TypeName = "bigint unsigned")]
    public required ulong UpdatedByUserId { get; init; }

    /// <summary>Column: row_version; SQL: bigint unsigned; not null.</summary>
    [Column("row_version", TypeName = "bigint unsigned")]
    public required ulong RowVersion { get; init; }
}
