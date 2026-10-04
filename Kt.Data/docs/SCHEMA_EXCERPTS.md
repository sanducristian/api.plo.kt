# Source schema excerpts

Verbatim table definitions from the supplied 2026-09-26 project reference. Historical snapshot, not a migration script.

```sql
CREATE TABLE `audit_event` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `workspace_id` bigint unsigned DEFAULT NULL COMMENT 'Workspace in which the action occurred; NULL only for platform-level events.',
  `actor_principal_id` bigint unsigned NOT NULL COMMENT 'Human, system, integration or migration actor responsible for the event.',
  `event_type` varchar(160) CHARACTER SET ascii COLLATE ascii_bin NOT NULL COMMENT 'Invariant machine-readable event code.',
  `entity_type` varchar(128) CHARACTER SET ascii COLLATE ascii_bin DEFAULT NULL COMMENT 'Module-defined entity type affected by the event.',
  `entity_id` varchar(191) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL COMMENT 'Entity identifier rendered as invariant text so modules may use different ID formats.',
  `occurred_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `correlation_id` char(36) CHARACTER SET ascii COLLATE ascii_bin DEFAULT NULL COMMENT 'Request or workflow correlation identifier.',
  `authentication_method` varchar(32) CHARACTER SET ascii COLLATE ascii_bin DEFAULT NULL COMMENT 'LOCAL, ENTRA_ID, GOOGLE, SYSTEM or another validated method.',
  `outcome` varchar(16) CHARACTER SET ascii COLLATE ascii_bin NOT NULL DEFAULT 'SUCCESS' COMMENT 'SUCCESS, FAILURE or DENIED.',
  `source_ip` varbinary(16) DEFAULT NULL COMMENT 'IPv4 or IPv6 address in packed binary form when collection is permitted.',
  `user_agent` varchar(1000) DEFAULT NULL,
  `details_json` json DEFAULT NULL COMMENT 'Structured event details; secrets and unnecessary personal data are forbidden.',
  PRIMARY KEY (`id`),
  KEY `idx_audit_event_workspace_time` (`workspace_id`,`occurred_utc`),
  KEY `idx_audit_event_actor_time` (`actor_principal_id`,`occurred_utc`),
  KEY `idx_audit_event_entity` (`entity_type`,`entity_id`,`occurred_utc`),
  KEY `idx_audit_event_correlation` (`correlation_id`),
  CONSTRAINT `fk_audit_event_actor` FOREIGN KEY (`actor_principal_id`) REFERENCES `security_principal` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_audit_event_workspace` FOREIGN KEY (`workspace_id`) REFERENCES `workspace` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ck_audit_event_outcome` CHECK ((`outcome` in (_utf8mb4'SUCCESS',_utf8mb4'FAILURE',_utf8mb4'DENIED')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Append-oriented security and business audit events for modern PLO modules.';
```

```sql
CREATE TABLE `company_workspace` (
  `workspace_id` bigint unsigned NOT NULL COMMENT 'Workspace that represents the company operating boundary.',
  `core_party_id` bigint unsigned NOT NULL COMMENT 'Authoritative organization party represented by the workspace.',
  `primary_legal_entity_id` bigint unsigned DEFAULT NULL COMMENT 'Optional primary accounting legal entity; additional entities remain module relationships.',
  `status` varchar(24) CHARACTER SET ascii COLLATE ascii_bin NOT NULL DEFAULT 'PENDING_VALIDATION' COMMENT 'PENDING_VALIDATION, ACTIVE, SUSPENDED or CLOSED.',
  `created_by_principal_id` bigint unsigned NOT NULL,
  `created_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `activated_utc` datetime(6) DEFAULT NULL,
  `suspended_utc` datetime(6) DEFAULT NULL,
  `closed_utc` datetime(6) DEFAULT NULL,
  `updated_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY (`workspace_id`),
  KEY `idx_company_workspace_party_status` (`core_party_id`,`status`),
  KEY `idx_company_workspace_legal_entity` (`primary_legal_entity_id`),
  KEY `idx_company_workspace_creator` (`created_by_principal_id`),
  KEY `fk_company_workspace_legal_entity_party` (`primary_legal_entity_id`,`core_party_id`),
  CONSTRAINT `fk_company_workspace_creator` FOREIGN KEY (`created_by_principal_id`) REFERENCES `security_principal` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_company_workspace_legal_entity_party` FOREIGN KEY (`primary_legal_entity_id`, `core_party_id`) REFERENCES `core_legal_entity` (`id`, `party_id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_company_workspace_party` FOREIGN KEY (`core_party_id`) REFERENCES `core_party` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_company_workspace_workspace` FOREIGN KEY (`workspace_id`) REFERENCES `workspace` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ck_company_workspace_status` CHECK ((`status` in (_utf8mb4'PENDING_VALIDATION',_utf8mb4'ACTIVE',_utf8mb4'SUSPENDED',_utf8mb4'CLOSED')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Permanent mapping between a company workspace and its organization/legal-entity master records.';
```

```sql
CREATE TABLE `core_business_partner` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `party_id` bigint unsigned NOT NULL COMMENT 'External or internal party acting as supplier, customer, or both',
  `partner_code` varchar(64) NOT NULL COMMENT 'Stable operational code used in procurement, sales, and integrations',
  `default_currency_id` bigint unsigned DEFAULT NULL,
  `default_payment_term_id` bigint unsigned DEFAULT NULL COMMENT 'Added as a foreign key after acc_payment_term is created',
  `is_supplier` tinyint(1) NOT NULL DEFAULT '0',
  `is_customer` tinyint(1) NOT NULL DEFAULT '0',
  `is_active` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Partner relationship state; the underlying party may remain active in other workspaces',
  `created_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `updated_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `row_version` bigint unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_core_business_partner_party` (`party_id`),
  UNIQUE KEY `uq_core_business_partner_code` (`partner_code`),
  KEY `fk_core_business_partner_currency` (`default_currency_id`),
  KEY `fk_core_business_partner_payment_term` (`default_payment_term_id`),
  CONSTRAINT `fk_core_business_partner_currency` FOREIGN KEY (`default_currency_id`) REFERENCES `sys_currency` (`id`),
  CONSTRAINT `fk_core_business_partner_party` FOREIGN KEY (`party_id`) REFERENCES `core_party` (`id`),
  CONSTRAINT `fk_core_business_partner_payment_term` FOREIGN KEY (`default_payment_term_id`) REFERENCES `finance_payment_term` (`id`),
  CONSTRAINT `ck_core_business_partner_role` CHECK (((`is_supplier` = true) or (`is_customer` = true)))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Suppliers and customers shared by procurement, sales, warehouse, and accounting';
```

```sql
CREATE TABLE `core_legal_entity` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `party_id` bigint unsigned NOT NULL COMMENT 'Party whose accounting books and legal obligations are represented',
  `code` varchar(32) NOT NULL COMMENT 'Stable short code used in numbering, permissions, and integrations',
  `default_currency_id` bigint unsigned NOT NULL,
  `time_zone_id` varchar(64) NOT NULL DEFAULT 'UTC' COMMENT 'IANA time-zone identifier',
  `is_book_active` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Whether new accounting transactions may be posted to this entity',
  `created_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `updated_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `row_version` bigint unsigned NOT NULL DEFAULT '1' COMMENT 'Incremented by the application for optimistic concurrency',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_core_legal_entity_party` (`party_id`),
  UNIQUE KEY `uq_core_legal_entity_code` (`code`),
  UNIQUE KEY `uq_core_legal_entity_id_party` (`id`,`party_id`),
  KEY `fk_core_legal_entity_currency` (`default_currency_id`),
  CONSTRAINT `fk_core_legal_entity_currency` FOREIGN KEY (`default_currency_id`) REFERENCES `sys_currency` (`id`),
  CONSTRAINT `fk_core_legal_entity_party` FOREIGN KEY (`party_id`) REFERENCES `core_party` (`id`),
  CONSTRAINT `ck_core_legal_entity_party_distinct` CHECK ((`party_id` > 0))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Companies that own accounting books and issue or receive invoices';
```

```sql
CREATE TABLE `core_location` (
  `id` bigint unsigned NOT NULL COMMENT 'Global obj_id identity; allocate explicitly in the same transaction',
  `public_id` binary(16) NOT NULL,
  `workspace_id` bigint unsigned NOT NULL,
  `parent_id` bigint unsigned DEFAULT NULL COMMENT 'Self-referential parent location ID for arbitrary hierarchy depth',
  `type_code` varchar(32) CHARACTER SET ascii COLLATE ascii_bin NOT NULL COMMENT 'SITE, BUILDING, ZONE, ROW, LEVEL, STORAGE_BIN, PRODUCTION_STATION, etc.',
  `code` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `name` varchar(255) NOT NULL,
  `responsible_party_id` bigint unsigned DEFAULT NULL,
  `address_object_id` bigint unsigned DEFAULT NULL COMMENT 'Optional existing obj_dyn_location address',
  `address_snapshot_json` json DEFAULT NULL,
  `time_zone_id` varchar(64) NOT NULL DEFAULT 'UTC',
  `latitude` decimal(10,8) DEFAULT NULL COMMENT 'WGS 84 Latitude in decimal degrees',
  `longitude` decimal(11,8) DEFAULT NULL COMMENT 'WGS 84 Longitude in decimal degrees',
  `altitude_meters` decimal(8,3) DEFAULT NULL COMMENT 'Altitude in meters above sea level',
  `local_frame_location_id` bigint unsigned DEFAULT NULL COMMENT 'Reference location acting as the local (0,0,0) origin frame',
  `pos_x_meters` decimal(8,3) DEFAULT NULL COMMENT 'Local offset X in meters relative to local frame',
  `pos_y_meters` decimal(8,3) DEFAULT NULL COMMENT 'Local offset Y in meters relative to local frame',
  `pos_z_meters` decimal(8,3) DEFAULT NULL COMMENT 'Local offset Z in meters relative to local frame',
  `is_active` tinyint unsigned NOT NULL DEFAULT '1',
  `created_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `created_by_principal_id` bigint unsigned NOT NULL,
  `updated_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `row_version` bigint unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_loc_scope` (`id`,`workspace_id`),
  UNIQUE KEY `uq_loc_public` (`public_id`),
  UNIQUE KEY `uq_loc_code` (`workspace_id`,`code`),
  KEY `fk_loc_actor` (`created_by_principal_id`),
  KEY `fk_loc_party` (`responsible_party_id`),
  KEY `fk_loc_address` (`address_object_id`),
  KEY `idx_cl_parent` (`parent_id`,`workspace_id`),
  KEY `idx_cl_type` (`type_code`),
  KEY `idx_cl_local_frame` (`local_frame_location_id`),
  CONSTRAINT `fk_cl_local_frame` FOREIGN KEY (`local_frame_location_id`) REFERENCES `core_location` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_cl_parent` FOREIGN KEY (`parent_id`, `workspace_id`) REFERENCES `core_location` (`id`, `workspace_id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_cl_type` FOREIGN KEY (`type_code`) REFERENCES `core_location_type` (`code`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_loc_actor` FOREIGN KEY (`created_by_principal_id`) REFERENCES `security_principal` (`id`),
  CONSTRAINT `fk_loc_address` FOREIGN KEY (`address_object_id`) REFERENCES `obj_dyn_location` (`id`),
  CONSTRAINT `fk_loc_obj` FOREIGN KEY (`id`) REFERENCES `obj_id` (`id`),
  CONSTRAINT `fk_loc_party` FOREIGN KEY (`responsible_party_id`) REFERENCES `core_party` (`id`),
  CONSTRAINT `fk_loc_ws` FOREIGN KEY (`workspace_id`) REFERENCES `workspace` (`id`),
  CONSTRAINT `chk_cl_prevent_self_parent` CHECK (((`parent_id` is null) or (`parent_id` <> `id`))),
  CONSTRAINT `ck_loc_active` CHECK ((`is_active` in (0,1)))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Tenant-scoped location tree;
```

```sql
CREATE TABLE `core_party` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `public_id` binary(16) NOT NULL COMMENT 'Application-generated UUID exposed through APIs instead of the database key',
  `party_type` varchar(24) NOT NULL COMMENT 'ORGANIZATION or INDIVIDUAL; both may buy, sell, own devices, or receive invoices',
  `legal_name` varchar(256) NOT NULL COMMENT 'Registered organization name or official individual name',
  `trade_name` varchar(256) DEFAULT NULL COMMENT 'Trading or display name when different from legal_name',
  `country_code` char(2) DEFAULT NULL COMMENT 'ISO 3166-1 alpha-2 country of establishment or residence',
  `registration_identifier` varchar(64) DEFAULT NULL COMMENT 'Convenience display identifier; authoritative registrations are stored in core_party_registration',
  `address_json` json DEFAULT NULL COMMENT 'Current structured primary address; legal document snapshots remain on each invoice',
  `legacy_company_id` bigint unsigned DEFAULT NULL COMMENT 'Populated only by the later legacy-client migration',
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `created_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `updated_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `row_version` bigint unsigned NOT NULL DEFAULT '1' COMMENT 'Incremented by the application for optimistic concurrency',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_core_party_public_id` (`public_id`),
  UNIQUE KEY `uq_core_party_legacy_company` (`legacy_company_id`),
  KEY `ix_core_party_name` (`legal_name`),
  KEY `ix_core_party_country` (`country_code`),
  CONSTRAINT `ck_core_party_country` CHECK (((`country_code` is null) or regexp_like(`country_code`,_utf8mb4'^[A-Z]{2}$'))),
  CONSTRAINT `ck_core_party_type` CHECK ((`party_type` in (_utf8mb4'ORGANIZATION',_utf8mb4'INDIVIDUAL')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Unified identity for internal entities, suppliers, customers, and individual consumers';
```

```sql
CREATE TABLE `device_unit` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `public_id` binary(16) NOT NULL COMMENT 'Application-generated UUID exposed by APIs',
  `module_ref` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL COMMENT 'Stable case-sensitive Device-module reference used by claim and transfer workflows',
  `device_model_id` bigint unsigned NOT NULL,
  `internal_serial_number` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL,
  `manufacturer_serial_number` varchar(128) DEFAULT NULL,
  `hardware_revision` varchar(64) DEFAULT NULL,
  `manufactured_at_utc` datetime(6) DEFAULT NULL,
  `released_at_utc` datetime(6) DEFAULT NULL,
  `lifecycle_status` varchar(24) NOT NULL DEFAULT 'REGISTERED',
  `configuration_json` json DEFAULT NULL COMMENT 'Validated non-secret device configuration snapshot',
  `legacy_object_id` bigint unsigned DEFAULT NULL,
  `created_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `created_by_principal_id` bigint unsigned DEFAULT NULL,
  `updated_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `row_version` bigint unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_device_unit_public_id` (`public_id`),
  UNIQUE KEY `uq_device_unit_module_ref` (`module_ref`),
  UNIQUE KEY `uq_device_unit_internal_serial` (`internal_serial_number`),
  UNIQUE KEY `uq_device_unit_model_manufacturer_serial` (`device_model_id`,`manufacturer_serial_number`),
  UNIQUE KEY `uq_device_unit_legacy_object` (`legacy_object_id`),
  KEY `ix_device_unit_model_status` (`device_model_id`,`lifecycle_status`),
  KEY `fk_device_unit_creator` (`created_by_principal_id`),
  CONSTRAINT `fk_device_unit_creator` FOREIGN KEY (`created_by_principal_id`) REFERENCES `security_principal` (`id`),
  CONSTRAINT `fk_device_unit_model` FOREIGN KEY (`device_model_id`) REFERENCES `device_model` (`id`),
  CONSTRAINT `ck_device_unit_release_date` CHECK (((`released_at_utc` is null) or (`manufactured_at_utc` is null) or (`released_at_utc` >= `manufactured_at_utc`))),
  CONSTRAINT `ck_device_unit_status` CHECK ((`lifecycle_status` in (_utf8mb4'REGISTERED',_utf8mb4'IN_PRODUCTION',_utf8mb4'QUALITY_HOLD',_utf8mb4'RELEASED',_utf8mb4'IN_SERVICE',_utf8mb4'SUSPENDED',_utf8mb4'RETIRED',_utf8mb4'SCRAPPED',_utf8mb4'LOST')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='One serialized or individually tracked completed physical device';
```

```sql
CREATE TABLE `finance_invoice` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `public_id` binary(16) NOT NULL COMMENT 'Application-generated UUID stored in binary form',
  `accounting_legal_entity_id` bigint unsigned NOT NULL COMMENT 'Legal entity whose books contain this invoice and its journal entry',
  `issuer_party_id` bigint unsigned NOT NULL COMMENT 'Party legally issuing the invoice',
  `recipient_party_id` bigint unsigned NOT NULL COMMENT 'Party receiving the goods/services or named as legal recipient',
  `invoice_recipient_party_id` bigint unsigned NOT NULL COMMENT 'Party responsible for receiving/processing the invoice; may differ from recipient_party_id',
  `invoice_import_id` bigint unsigned DEFAULT NULL COMMENT 'Capture/OCR/e-invoice intake record that created this draft',
  `number_sequence_id` bigint unsigned DEFAULT NULL COMMENT 'Sequence used for PLO-controlled numbering; required by policy for outbound documents',
  `invoice_type` varchar(32) NOT NULL COMMENT 'Commercial direction and document kind, including purchase/sales credit notes',
  `document_number` varchar(128) NOT NULL COMMENT 'Legal invoice number exactly as printed, issued, or received',
  `document_number_normalized` varchar(128) NOT NULL COMMENT 'Comparison form used for duplicate detection; the displayed number remains unchanged',
  `duplicate_occurrence_no` smallint unsigned NOT NULL DEFAULT '1' COMMENT 'Values above 1 require an audited duplicate override',
  `internal_number` varchar(64) DEFAULT NULL COMMENT 'PLO controlled number assigned at posting when required',
  `issue_date` date NOT NULL,
  `received_date` date DEFAULT NULL,
  `accounting_date` date DEFAULT NULL,
  `due_date` date DEFAULT NULL,
  `payment_term_id` bigint unsigned DEFAULT NULL,
  `currency_id` bigint unsigned NOT NULL,
  `exchange_rate_to_book_currency` decimal(20,10) DEFAULT NULL COMMENT 'Multiplier from invoice currency to the legal entity book currency',
  `exchange_rate_date` date DEFAULT NULL COMMENT 'Date whose official or approved exchange rate was used',
  `purchase_order_reference` varchar(128) DEFAULT NULL,
  `customer_reference` varchar(128) DEFAULT NULL,
  `contract_reference` varchar(128) DEFAULT NULL,
  `issuer_name_snapshot` varchar(256) NOT NULL COMMENT 'Issuer name preserved exactly for the posted legal record',
  `issuer_tax_id_snapshot` varchar(64) DEFAULT NULL COMMENT 'Issuer tax identifier printed on the document',
  `issuer_address_snapshot_json` json DEFAULT NULL COMMENT 'Structured issuer address preserved independently of later partner-master changes',
  `recipient_name_snapshot` varchar(256) NOT NULL COMMENT 'Recipient name preserved exactly for the posted legal record',
  `recipient_tax_id_snapshot` varchar(64) DEFAULT NULL COMMENT 'Recipient tax identifier printed on the document',
  `recipient_address_snapshot_json` json DEFAULT NULL COMMENT 'Structured recipient address preserved independently of later master-data changes',
  `invoice_recipient_name_snapshot` varchar(256) NOT NULL COMMENT 'Invoice-processing/bill-to party name preserved on the legal record',
  `invoice_recipient_tax_id_snapshot` varchar(64) DEFAULT NULL COMMENT 'Invoice-recipient tax identifier printed on the document',
  `invoice_recipient_address_snapshot_json` json DEFAULT NULL COMMENT 'Bill-to/invoice-recipient address preserved independently of master-data changes',
  `subtotal_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `discount_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `charge_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `tax_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `rounding_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `total_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `prepaid_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `payable_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `workflow_status` varchar(24) NOT NULL DEFAULT 'DRAFT' COMMENT 'Human review and approval state; independent from settlement and posting',
  `matching_status` varchar(24) NOT NULL DEFAULT 'NOT_APPLICABLE' COMMENT 'PO/receipt/tracked-item matching state',
  `settlement_status` varchar(24) NOT NULL DEFAULT 'OPEN' COMMENT 'Open-item state for future bank/payment reconciliation; no payment execution is implemented in this version',
  `posting_status` varchar(24) NOT NULL DEFAULT 'NOT_POSTED' COMMENT 'Accounting posting state; posted data is immutable',
  `posting_journal_entry_id` bigint unsigned DEFAULT NULL COMMENT 'Balanced journal entry created when this invoice is posted',
  `is_disputed` tinyint(1) NOT NULL DEFAULT '0',
  `dispute_reason_code` varchar(64) DEFAULT NULL,
  `notes` text,
  `posted_at_utc` datetime(6) DEFAULT NULL,
  `posted_by_user_id` bigint unsigned DEFAULT NULL,
  `voided_at_utc` datetime(6) DEFAULT NULL,
  `voided_by_user_id` bigint unsigned DEFAULT NULL,
  `void_reason` varchar(512) DEFAULT NULL,
  `legacy_invoice_id` bigint unsigned DEFAULT NULL,
  `created_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `created_by_user_id` bigint unsigned NOT NULL,
  `updated_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `updated_by_user_id` bigint unsigned NOT NULL,
  `row_version` bigint unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_acc_invoice_public_id` (`public_id`),
  UNIQUE KEY `uq_acc_invoice_document_number` (`accounting_legal_entity_id`,`issuer_party_id`,`invoice_type`,`document_number_normalized`,`duplicate_occurrence_no`),
  UNIQUE KEY `uq_acc_invoice_internal_number` (`accounting_legal_entity_id`,`internal_number`),
  UNIQUE KEY `uq_acc_invoice_legacy` (`legacy_invoice_id`),
  UNIQUE KEY `uq_acc_invoice_posting_entry` (`posting_journal_entry_id`),
  KEY `ix_acc_invoice_work_queue` (`accounting_legal_entity_id`,`workflow_status`,`issue_date`),
  KEY `ix_acc_invoice_settlement_queue` (`accounting_legal_entity_id`,`settlement_status`,`due_date`),
  KEY `ix_acc_invoice_issuer_date` (`issuer_party_id`,`issue_date`),
  KEY `ix_acc_invoice_recipient_date` (`recipient_party_id`,`issue_date`),
  KEY `ix_acc_invoice_accounting_date` (`accounting_legal_entity_id`,`accounting_date`),
  KEY `fk_acc_invoice_invoice_recipient` (`invoice_recipient_party_id`),
  KEY `fk_acc_invoice_import` (`invoice_import_id`),
  KEY `fk_acc_invoice_number_sequence` (`number_sequence_id`),
  KEY `fk_acc_invoice_payment_term` (`payment_term_id`),
  KEY `fk_acc_invoice_currency` (`currency_id`),
  CONSTRAINT `fk_acc_invoice_currency` FOREIGN KEY (`currency_id`) REFERENCES `sys_currency` (`id`),
  CONSTRAINT `fk_acc_invoice_import` FOREIGN KEY (`invoice_import_id`) REFERENCES `finance_invoice_import` (`id`),
  CONSTRAINT `fk_acc_invoice_invoice_recipient` FOREIGN KEY (`invoice_recipient_party_id`) REFERENCES `core_party` (`id`),
  CONSTRAINT `fk_acc_invoice_issuer` FOREIGN KEY (`issuer_party_id`) REFERENCES `core_party` (`id`),
  CONSTRAINT `fk_acc_invoice_legal_entity` FOREIGN KEY (`accounting_legal_entity_id`) REFERENCES `core_legal_entity` (`id`),
  CONSTRAINT `fk_acc_invoice_number_sequence` FOREIGN KEY (`number_sequence_id`) REFERENCES `finance_document_sequence` (`id`),
  CONSTRAINT `fk_acc_invoice_payment_term` FOREIGN KEY (`payment_term_id`) REFERENCES `finance_payment_term` (`id`),
  CONSTRAINT `fk_acc_invoice_posting_entry` FOREIGN KEY (`posting_journal_entry_id`) REFERENCES `acc_journal_entry` (`id`),
  CONSTRAINT `fk_acc_invoice_recipient` FOREIGN KEY (`recipient_party_id`) REFERENCES `core_party` (`id`),
  CONSTRAINT `ck_acc_invoice_dispute` CHECK (((`is_disputed` = false) or (`dispute_reason_code` is not null))),
  CONSTRAINT `ck_acc_invoice_due_date` CHECK (((`due_date` is null) or (`due_date` >= `issue_date`))),
  CONSTRAINT `ck_acc_invoice_duplicate_occurrence` CHECK ((`duplicate_occurrence_no` >= 1)),
  CONSTRAINT `ck_acc_invoice_exchange_rate` CHECK (((`exchange_rate_to_book_currency` is null) or (`exchange_rate_to_book_currency` > 0))),
  CONSTRAINT `ck_acc_invoice_matching_status` CHECK ((`matching_status` in (_utf8mb4'NOT_APPLICABLE',_utf8mb4'UNMATCHED',_utf8mb4'PARTIAL',_utf8mb4'MATCHED',_utf8mb4'EXCEPTION'))),
  CONSTRAINT `ck_acc_invoice_posting_status` CHECK ((`posting_status` in (_utf8mb4'NOT_POSTED',_utf8mb4'POSTING',_utf8mb4'POSTED',_utf8mb4'REVERSED',_utf8mb4'FAILED'))),
  CONSTRAINT `ck_acc_invoice_settlement_status` CHECK ((`settlement_status` in (_utf8mb4'OPEN',_utf8mb4'PARTIALLY_SETTLED',_utf8mb4'SETTLED',_utf8mb4'WRITTEN_OFF',_utf8mb4'NOT_APPLICABLE'))),
  CONSTRAINT `ck_acc_invoice_type` CHECK ((`invoice_type` in (_utf8mb4'PURCHASE_INVOICE',_utf8mb4'PURCHASE_CREDIT_NOTE',_utf8mb4'SALES_INVOICE',_utf8mb4'SALES_CREDIT_NOTE'))),
  CONSTRAINT `ck_acc_invoice_workflow_status` CHECK ((`workflow_status` in (_utf8mb4'DRAFT',_utf8mb4'UNDER_REVIEW',_utf8mb4'APPROVAL_PENDING',_utf8mb4'APPROVED',_utf8mb4'POSTED',_utf8mb4'REJECTED',_utf8mb4'VOID')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Modern immutable-after-posting invoice header';
```

```sql
CREATE TABLE `finance_invoice_line` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `invoice_id` bigint unsigned NOT NULL,
  `line_number` int unsigned NOT NULL,
  `line_type` varchar(24) NOT NULL DEFAULT 'ITEM',
  `product_id` bigint unsigned DEFAULT NULL COMMENT 'Stable modern product/component ID; formal FK added with the Product module',
  `product_code_snapshot` varchar(128) DEFAULT NULL,
  `description` varchar(1024) NOT NULL,
  `quantity` decimal(20,6) NOT NULL DEFAULT '1.000000',
  `unit_of_measure_code` varchar(32) DEFAULT NULL,
  `packaging_configuration_id` bigint unsigned DEFAULT NULL COMMENT 'Cross-module reference to the product packaging configuration',
  `unit_price` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `discount_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `charge_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `net_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `tax_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `gross_amount` decimal(20,6) NOT NULL DEFAULT '0.000000',
  `service_period_start` date DEFAULT NULL,
  `service_period_end` date DEFAULT NULL,
  `country_of_origin_code` char(2) DEFAULT NULL COMMENT 'ISO 3166-1 alpha-2 non-preferential or declared origin for this invoice line',
  `tariff_classification_id` bigint unsigned DEFAULT NULL COMMENT 'Versioned classification selected for the invoice date/jurisdiction',
  `customs_tariff_code_snapshot` varchar(32) DEFAULT NULL COMMENT 'Tariff code preserved exactly as used on the invoice/customs evidence',
  `created_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `updated_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `row_version` bigint unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_acc_invoice_line_number` (`invoice_id`,`line_number`),
  KEY `ix_acc_invoice_line_product` (`product_id`),
  KEY `ix_acc_invoice_line_packaging` (`packaging_configuration_id`),
  KEY `ix_acc_invoice_line_tariff` (`tariff_classification_id`),
  CONSTRAINT `fk_acc_invoice_line_invoice` FOREIGN KEY (`invoice_id`) REFERENCES `finance_invoice` (`id`),
  CONSTRAINT `fk_acc_invoice_line_tariff` FOREIGN KEY (`tariff_classification_id`) REFERENCES `customs_tariff_classification` (`id`),
  CONSTRAINT `ck_acc_invoice_line_period` CHECK (((`service_period_end` is null) or (`service_period_start` is null) or (`service_period_end` >= `service_period_start`))),
  CONSTRAINT `ck_acc_invoice_line_quantity` CHECK ((`quantity` <> 0)),
  CONSTRAINT `ck_acc_invoice_line_type` CHECK ((`line_type` in (_utf8mb4'ITEM',_utf8mb4'SERVICE',_utf8mb4'SHIPPING',_utf8mb4'CHARGE',_utf8mb4'DISCOUNT',_utf8mb4'ROUNDING')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Invoice lines with exact quantities, pricing, and product snapshots';
```

```sql
CREATE TABLE `finance_invoice_status_event` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `invoice_id` bigint unsigned NOT NULL,
  `status_dimension` varchar(24) NOT NULL,
  `from_status_code` varchar(32) DEFAULT NULL,
  `to_status_code` varchar(32) NOT NULL,
  `reason_code` varchar(64) DEFAULT NULL,
  `comment` varchar(1024) DEFAULT NULL,
  `occurred_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `actor_user_id` bigint unsigned DEFAULT NULL,
  `correlation_id` binary(16) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `ix_acc_invoice_status_event_invoice` (`invoice_id`,`occurred_at_utc`),
  CONSTRAINT `fk_acc_invoice_status_event_invoice` FOREIGN KEY (`invoice_id`) REFERENCES `finance_invoice` (`id`),
  CONSTRAINT `ck_acc_invoice_status_event_dimension` CHECK ((`status_dimension` in (_utf8mb4'WORKFLOW',_utf8mb4'MATCHING',_utf8mb4'SETTLEMENT',_utf8mb4'POSTING',_utf8mb4'DISPUTE')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Append-only invoice state transition history';
```

```sql
CREATE TABLE `legacy_component` (
  `id` bigint unsigned NOT NULL,
  `manufacturerId` bigint unsigned DEFAULT NULL,
  `weight` double DEFAULT NULL COMMENT 'Weight in grams',
  `douaneId` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Stores component master records, including manufacturer reference, weight, and customs classification data.';
```

```sql
CREATE TABLE `obj_dyn` (
  `id` bigint unsigned NOT NULL,
  `masterId` bigint unsigned NOT NULL,
  `slaveId` bigint unsigned NOT NULL,
  `typeId` bigint unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores explicit typed references between a master object and a slave object.';
```

```sql
CREATE TABLE `obj_dyn_file` (
  `id` bigint unsigned NOT NULL,
  `fileName` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci NOT NULL COMMENT 'The original filename',
  `fileSize` bigint unsigned NOT NULL COMMENT 'The file size',
  `fileChecksum` varchar(64) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci NOT NULL COMMENT 'The file checksum',
  `checksumAlgorithm` varchar(16) COLLATE utf8mb3_unicode_ci NOT NULL DEFAULT 'SHA256' COMMENT 'Algorithm used by fileChecksum; existing 64-character values should be verified as SHA-256',
  `fileTypename` varchar(64) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL COMMENT 'The file type name as referenced by a browser',
  `storageProvider` varchar(32) COLLATE utf8mb3_unicode_ci DEFAULT NULL COMMENT 'FileDrive, S3, AzureBlob, Local, SharePoint, or another configured provider',
  `storageKey` varchar(512) COLLATE utf8mb3_unicode_ci DEFAULT NULL COMMENT 'Provider-specific immutable object, drive item, or path identifier',
  `contentUrl` varchar(2048) COLLATE utf8mb3_unicode_ci DEFAULT NULL COMMENT 'Stable canonical URL; never store an expiring signed download URL',
  `apiReference` varchar(512) COLLATE utf8mb3_unicode_ci DEFAULT NULL COMMENT 'Stable API route or opaque external API identifier used to resolve the file',
  `accessMode` varchar(24) COLLATE utf8mb3_unicode_ci NOT NULL DEFAULT 'PRIVATE' COMMENT 'How content is obtained: PRIVATE, AUTHENTICATED_URL, PUBLIC_URL, or API_ONLY',
  `malwareScanStatus` varchar(24) COLLATE utf8mb3_unicode_ci NOT NULL DEFAULT 'NOT_SCANNED' COMMENT 'NOT_SCANNED, PENDING, CLEAN, REJECTED, or FAILED',
  `metadataJson` json DEFAULT NULL COMMENT 'Provider-specific metadata that is not used for accounting decisions',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_objfile_storage_locator` (`storageProvider`,`storageKey`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores file metadata, including original name, byte size, checksum, and file or MIME type.';
```

```sql
CREATE TABLE `obj_dyn_geo_pose` (
  `id` bigint unsigned NOT NULL COMMENT 'Global obj_id; master_id must equal owner_object_id and role_id the registered geo.pose role',
  `workspace_id` bigint unsigned NOT NULL,
  `owner_object_id` bigint unsigned NOT NULL,
  `pose_role_code` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL DEFAULT 'STATION_REFERENCE',
  `latitude_deg` decimal(12,9) NOT NULL COMMENT 'Geodetic latitude in degrees',
  `longitude_deg` decimal(12,9) NOT NULL COMMENT 'Geodetic longitude in degrees',
  `horizontal_crs_code` varchar(64) NOT NULL DEFAULT 'EPSG:4326',
  `reference_frame_name` varchar(128) DEFAULT NULL COMMENT 'Exact RTK frame realization when known',
  `coordinate_epoch_year` decimal(9,5) DEFAULT NULL,
  `altitude_m` decimal(14,6) DEFAULT NULL,
  `altitude_reference` varchar(24) NOT NULL DEFAULT 'UNKNOWN' COMMENT 'ELLIPSOIDAL, ORTHOMETRIC, LOCAL or UNKNOWN',
  `vertical_crs_code` varchar(128) DEFAULT NULL COMMENT 'Vertical datum/geoid model or named local datum',
  `heading_deg` decimal(9,6) DEFAULT NULL COMMENT 'Clockwise from true north; not magnetic heading or GNSS course',
  `pitch_deg` decimal(9,6) DEFAULT NULL COMMENT 'Positive nose-up',
  `roll_deg` decimal(9,6) DEFAULT NULL COMMENT 'Positive right-side-down',
  `attitude_convention` varchar(48) NOT NULL DEFAULT 'NED_FRD_YAW_PITCH_ROLL',
  `horizontal_accuracy_m` decimal(12,6) DEFAULT NULL,
  `vertical_accuracy_m` decimal(12,6) DEFAULT NULL,
  `attitude_accuracy_deg` decimal(9,6) DEFAULT NULL,
  `fix_type` varchar(24) NOT NULL DEFAULT 'UNKNOWN',
  `rtk_correction_age_s` decimal(12,3) DEFAULT NULL,
  `rtk_base_reference` varchar(128) DEFAULT NULL,
  `antenna_reference_point` varchar(128) DEFAULT NULL COMMENT 'Physical point to which coordinates refer; record lever arm externally if needed',
  `observed_at_utc` datetime(6) NOT NULL,
  `valid_from_utc` datetime(6) NOT NULL,
  `valid_to_utc` datetime(6) DEFAULT NULL,
  `survey_evidence_file_id` bigint unsigned DEFAULT NULL,
  `created_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `created_by_principal_id` bigint unsigned NOT NULL,
  `active_owner_id` bigint unsigned GENERATED ALWAYS AS ((case when (`valid_to_utc` is null) then `owner_object_id` else NULL end)) STORED,
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_geo_scope` (`id`,`workspace_id`),
  UNIQUE KEY `uq_geo_current` (`workspace_id`,`active_owner_id`,`pose_role_code`),
  KEY `ix_geo_owner_time` (`workspace_id`,`owner_object_id`,`observed_at_utc`),
  KEY `fk_geo_actor` (`created_by_principal_id`),
  KEY `fk_geo_owner` (`owner_object_id`),
  KEY `fk_geo_evidence` (`survey_evidence_file_id`),
  CONSTRAINT `fk_geo_actor` FOREIGN KEY (`created_by_principal_id`) REFERENCES `security_principal` (`id`),
  CONSTRAINT `fk_geo_evidence` FOREIGN KEY (`survey_evidence_file_id`) REFERENCES `obj_dyn_file` (`id`),
  CONSTRAINT `fk_geo_obj` FOREIGN KEY (`id`) REFERENCES `obj_id` (`id`),
  CONSTRAINT `fk_geo_owner` FOREIGN KEY (`owner_object_id`) REFERENCES `obj_id` (`id`),
  CONSTRAINT `fk_geo_ws` FOREIGN KEY (`workspace_id`) REFERENCES `workspace` (`id`),
  CONSTRAINT `ck_geo_accuracy` CHECK ((((`horizontal_accuracy_m` is null) or (`horizontal_accuracy_m` >= 0)) and ((`vertical_accuracy_m` is null) or (`vertical_accuracy_m` >= 0)) and ((`attitude_accuracy_deg` is null) or (`attitude_accuracy_deg` between 0 and 180)) and ((`rtk_correction_age_s` is null) or (`rtk_correction_age_s` >= 0)))),
  CONSTRAINT `ck_geo_dates` CHECK (((`valid_to_utc` is null) or (`valid_to_utc` > `valid_from_utc`))),
  CONSTRAINT `ck_geo_fix` CHECK ((`fix_type` in (_utf8mb4'UNKNOWN',_utf8mb4'SURVEYED',_utf8mb4'STANDALONE',_utf8mb4'DGPS',_utf8mb4'RTK_FLOAT',_utf8mb4'RTK_FIXED',_utf8mb4'MANUAL'))),
  CONSTRAINT `ck_geo_heading` CHECK (((`heading_deg` is null) or ((`heading_deg` >= 0) and (`heading_deg` < 360)))),
  CONSTRAINT `ck_geo_lat` CHECK ((`latitude_deg` between -(90) and 90)),
  CONSTRAINT `ck_geo_lon` CHECK ((`longitude_deg` between -(180) and 180)),
  CONSTRAINT `ck_geo_pitch` CHECK (((`pitch_deg` is null) or (`pitch_deg` between -(90) and 90))),
  CONSTRAINT `ck_geo_roll` CHECK (((`roll_deg` is null) or (`roll_deg` between -(180) and 180))),
  CONSTRAINT `ck_geo_vertical` CHECK (((`altitude_reference` in (_utf8mb4'ELLIPSOIDAL',_utf8mb4'ORTHOMETRIC',_utf8mb4'LOCAL',_utf8mb4'UNKNOWN')) and ((`altitude_reference` not in (_utf8mb4'ORTHOMETRIC',_utf8mb4'LOCAL')) or (`vertical_crs_code` is not null))))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Versioned generic station/object geographic position and attitude;
```

```sql
CREATE TABLE `obj_dyn_location` (
  `id` bigint unsigned NOT NULL,
  `country` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `county` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `department` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `city` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `street` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `building` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `zipcode` varchar(64) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores structured location and postal-address data that can be associated with another object.';
```

```sql
CREATE TABLE `obj_dyn_number` (
  `id` bigint unsigned NOT NULL,
  `value` bigint unsigned NOT NULL,
  `year` int unsigned NOT NULL,
  `month` int unsigned NOT NULL,
  `roleId` bigint DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `idxYear` (`year`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores allocated business numbers together with their semantic role, year, and month context.';
```

```sql
CREATE TABLE `obj_dyn_number_class` (
  `id` bigint unsigned NOT NULL,
  `typeId` bigint unsigned NOT NULL,
  `prefix` varchar(64) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `value` varchar(64) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `sufix` varchar(64) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL,
  `resetEachYear` tinyint(1) DEFAULT NULL,
  `resetEachMonth` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idxType` (`typeId`),
  KEY `idxPrefix` (`prefix`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Defines configurable numbering sequences, prefixes, suffixes, and annual or monthly reset behavior.';
```

```sql
CREATE TABLE `obj_dyn_price` (
  `id` bigint unsigned NOT NULL,
  `currency_id` bigint unsigned NOT NULL,
  `value` decimal(20,6) unsigned NOT NULL DEFAULT '0.000000',
  `date` date NOT NULL DEFAULT '1900-01-01',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `ix_objprice_currency` (`currency_id`),
  CONSTRAINT `fk_objprice_currency` FOREIGN KEY (`currency_id`) REFERENCES `sys_currency` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores dated price values with their associated currency;
```

```sql
CREATE TABLE `obj_dyn_quantity` (
  `id` bigint unsigned NOT NULL,
  `umId` bigint unsigned NOT NULL,
  `value` double DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores quantity values with their unit of measure;
```

```sql
CREATE TABLE `obj_dyn_val_attrib` (
  `id` bigint unsigned NOT NULL,
  `attrName` varchar(64) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci NOT NULL,
  `attrValue` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `idxName` (`attrName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores short user-defined attribute name and value pairs associated with objects through the object kernel.';
```

```sql
CREATE TABLE `obj_dyn_val_blob` (
  `id` bigint unsigned NOT NULL DEFAULT '0',
  `data` blob,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores binary or long object data, including content that does not fit in the short string value tables.';
```

```sql
CREATE TABLE `obj_dyn_val_date` (
  `id` bigint unsigned NOT NULL DEFAULT '0',
  `value` date DEFAULT '1900-01-01',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores date values whose business meaning is defined by the owning object and semantic role.';
```

```sql
CREATE TABLE `obj_dyn_val_datetime` (
  `id` bigint unsigned NOT NULL,
  `value` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores date-and-time values whose business meaning is defined by the owning object and semantic role.';
```

```sql
CREATE TABLE `obj_dyn_val_double` (
  `id` bigint unsigned NOT NULL,
  `value` double DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores legacy floating-point object values such as coefficients, gains, or offsets.';
```

```sql
CREATE TABLE `obj_dyn_val_generic` (
  `id` bigint unsigned NOT NULL DEFAULT '0',
  `value` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL COMMENT 'The attribute value',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores generic short string values whose meaning is defined by the owning object and semantic role.';
```

```sql
CREATE TABLE `obj_dyn_val_int` (
  `id` bigint unsigned NOT NULL,
  `value` bigint NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores signed integer object values whose meaning is defined by the owning object and semantic role.';
```

```sql
CREATE TABLE `obj_dyn_val_string` (
  `id` bigint unsigned NOT NULL DEFAULT '0',
  `languageId` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'The language ID for the current string\n',
  `data` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci DEFAULT NULL COMMENT 'The data for the string in question',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores localized object strings associated with a language, owning object, and semantic role.';
```

```sql
CREATE TABLE `obj_dyn_val_time` (
  `id` bigint unsigned NOT NULL,
  `value` time NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Stores time-of-day object values whose meaning is defined by the owning object and semantic role.';
```

```sql
CREATE TABLE `obj_id` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT COMMENT 'Globally unique internal object ID, shared by the corresponding record in its concrete table.',
  `table_id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Concrete table identifier resolved through the active object table catalogue. Reserved values follow kernel catalogue conventions.',
  `invalid` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'Logical invalidation flag: 0 = active; 1 = invalidated or logically deleted. Does not imply physical deletion.',
  `original_id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Object ID referenced for version lineage when a revised object is created. The kernel versioning rules determine the referenced version.',
  `last_update` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Creation or latest modification timestamp of this registry row; does not automatically track changes to the concrete object record.',
  `user_id` bigint unsigned DEFAULT '0' COMMENT 'Internal PLO user ID attributed to this registry entry by kernel routines or triggers. Not automatically updated by the timestamp mechanism.',
  `master_id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Global object ID of the owning or parent object; for a dynamic property, identifies the object described by that property.',
  `role_id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Semantic role identifier resolved through the active role catalogue; defines the meaning of this object or its relationship to its master. Not an access-control role.',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `idxTableId` (`table_id`),
  KEY `idxNotDeleted` (`invalid`),
  KEY `idxOriginalId` (`original_id`),
  KEY `idxMasterId` (`master_id`),
  KEY `idxMasterSlave` (`master_id`,`role_id`),
  KEY `idxMasterSlaveValid` (`master_id`,`role_id`,`invalid`,`original_id`)
) ENGINE=InnoDB AUTO_INCREMENT=275907 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci COMMENT='Active PLO object identity registry: global IDs shared with concrete records, table mapping, logical validity, version lineage, user attribution, parent ownership and semantic roles.';
```

```sql
CREATE TABLE `obj_table` (
  `id` bigint unsigned NOT NULL,
  `table_name` varchar(196) NOT NULL DEFAULT '' COMMENT 'The table name',
  `table_index_field` varchar(64) DEFAULT '' COMMENT 'The table index field name (ex: ID)\n',
  `table_index_name` varchar(64) DEFAULT '' COMMENT 'The table index name (ex: idxId)',
  `type_id` bigint unsigned DEFAULT '0',
  `ref_nil` tinyint(1) DEFAULT '0' COMMENT 'If set, when no other references are made to the specified object then it can become nil.',
  `uses_global_object_id` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `tableName_UNIQUE` (`table_name`),
  KEY `idxTypeId` (`type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
```

```sql
CREATE TABLE `obj_type` (
  `id` bigint unsigned NOT NULL,
  `parent_id` bigint unsigned NOT NULL COMMENT 'The parent ID for the guid\\n',
  `guid` varchar(36) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci NOT NULL COMMENT 'The GUID number for the unit\n',
  `name` varchar(128) COLLATE utf8mb3_unicode_ci NOT NULL COMMENT 'The name of the current node',
  `singleton` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'If set, a single reference per object must be made\nex: \n- sys.Name --> a single reference must exist\n- user.group --> a user can be in several group\n',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci;
```

```sql
CREATE TABLE `platform_user` (
  `id` bigint unsigned NOT NULL COMMENT 'Principal ID for this human user; shared with security_principal.id.',
  `person_id` bigint DEFAULT NULL COMMENT 'Optional link to the legacy person row while person data is migrated.',
  `status` varchar(24) CHARACTER SET ascii COLLATE ascii_bin NOT NULL DEFAULT 'PENDING' COMMENT 'PENDING, ACTIVE, SUSPENDED or CLOSED.',
  `display_name` varchar(255) NOT NULL COMMENT 'Preferred user-facing name; authentication never relies on this value.',
  `preferred_locale` varchar(16) CHARACTER SET ascii COLLATE ascii_bin DEFAULT NULL COMMENT 'Optional BCP 47 locale such as en-GB, fr-FR or ro-RO.',
  `time_zone` varchar(64) CHARACTER SET ascii COLLATE ascii_bin DEFAULT NULL COMMENT 'Optional IANA time-zone identifier used for display, never for storing timestamps.',
  `email_requirement_met_utc` datetime(6) DEFAULT NULL COMMENT 'UTC time when the mandatory PLO email-verification requirement was last satisfied.',
  `phone_requirement_met_utc` datetime(6) DEFAULT NULL COMMENT 'UTC time when the mandatory PLO phone-verification requirement was last satisfied.',
  `mfa_requirement_met_utc` datetime(6) DEFAULT NULL COMMENT 'UTC time when the mandatory MFA-enrollment requirement was last satisfied.',
  `activated_utc` datetime(6) DEFAULT NULL COMMENT 'UTC time when access to business modules was activated.',
  `created_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'UTC creation date and time.',
  `updated_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6) COMMENT 'UTC time of the latest profile or status update.',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_platform_user_person` (`person_id`),
  CONSTRAINT `fk_platform_user_person` FOREIGN KEY (`person_id`) REFERENCES `legacy_person` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_platform_user_principal` FOREIGN KEY (`id`) REFERENCES `security_principal` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ck_platform_user_status` CHECK ((`status` in (_utf8mb4'PENDING',_utf8mb4'ACTIVE',_utf8mb4'SUSPENDED',_utf8mb4'CLOSED')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Modern global user account representing one person across company and personal workspaces.';
```

```sql
CREATE TABLE `security_principal` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT COMMENT 'Internal identifier used as the actor in modern audit and authorization records.',
  `principal_code` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL COMMENT 'Stable machine-readable code; system codes are never translated or reused.',
  `principal_type` varchar(32) CHARACTER SET ascii COLLATE ascii_bin NOT NULL COMMENT 'USER, SYSTEM, MIGRATION, INTEGRATION, BACKGROUND_JOB or SUPPORT.',
  `display_name` varchar(255) NOT NULL COMMENT 'Human-readable principal name; not used as an authentication key.',
  `status` varchar(16) CHARACTER SET ascii COLLATE ascii_bin NOT NULL DEFAULT 'ACTIVE' COMMENT 'ACTIVE or DISABLED.',
  `created_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'UTC creation date and time.',
  `disabled_utc` datetime(6) DEFAULT NULL COMMENT 'UTC date and time when the principal was disabled, if applicable.',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_security_principal_code` (`principal_code`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  CONSTRAINT `ck_security_principal_status` CHECK ((`status` in (_ascii'ACTIVE',_ascii'DISABLED'))),
  CONSTRAINT `ck_security_principal_type` CHECK ((`principal_type` in (_ascii'USER',_ascii'SYSTEM',_ascii'MIGRATION',_ascii'INTEGRATION',_ascii'BACKGROUND_JOB',_ascii'SUPPORT')))
) ENGINE=InnoDB AUTO_INCREMENT=219146 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Represents a human or automated actor that can perform and own auditable actions.';
```

```sql
CREATE TABLE `sys_currency` (
  `id` bigint unsigned NOT NULL,
  `name` varchar(128) NOT NULL,
  `short_name` varchar(12) NOT NULL,
  `iso_code` char(3) NOT NULL COMMENT 'ISO 4217 alphabetic code',
  `numeric_code` char(3) DEFAULT NULL COMMENT 'ISO 4217 numeric code',
  `symbol` varchar(16) NOT NULL,
  `minor_unit` tinyint unsigned DEFAULT NULL COMMENT 'ISO 4217 minor-unit precision; NULL when ISO publishes N.A.',
  `is_active` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Available for new transactions',
  `created_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `updated_at_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `row_version` bigint unsigned NOT NULL DEFAULT '1' COMMENT 'Incremented by the application on catalogue edits',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `uq_sys_currency_iso_code` (`iso_code`),
  UNIQUE KEY `uq_sys_currency_numeric_code` (`numeric_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Authoritative currency catalogue used by Money values and financial records';
```

```sql
CREATE TABLE `sys_language` (
  `id` bigint unsigned NOT NULL COMMENT 'Global internal identity and primary key, allocated via objId',
  `language_name` varchar(64) NOT NULL COMMENT 'Standard English name of the language',
  `native_name` varchar(64) DEFAULT NULL COMMENT 'Native localized name of the language',
  `iso_code_2` varchar(16) DEFAULT NULL COMMENT 'ISO 639-1 two-letter language code (e.g., en, fr)',
  `iso_code_3` varchar(16) DEFAULT NULL COMMENT 'ISO 639-2 three-letter language code (e.g., eng, fra)',
  `locale_code` varchar(16) DEFAULT NULL COMMENT 'Standard locale code (e.g., en-US, fr-FR)',
  `legacy_code` varchar(16) DEFAULT NULL COMMENT 'Legacy or alternative short code variant',
  `description` varchar(64) DEFAULT NULL COMMENT 'Internal notes or description of the language entry',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Stores supported languages and legacy short language-code variants used by localized object strings.';
```

```sql
CREATE TABLE `sys_um` (
  `id` bigint unsigned NOT NULL COMMENT 'Global obj_id identity allocated via AllocateId()',
  `uuid` binary(16) NOT NULL COMMENT 'Application UUID stored as binary for safe API exposure',
  `class_id` bigint unsigned DEFAULT NULL COMMENT 'Direct optional reference to sys_um_class.id',
  `unit_symbol` varchar(32) NOT NULL COMMENT 'Display symbol representation (e.g., m, kg, s, °C)',
  `is_base_unit` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'Flag indicating whether this is the SI / base reference unit for its class',
  `decimal_precision` tinyint unsigned DEFAULT '2' COMMENT 'Suggested display decimal precision for formatted outputs',
  `is_active` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Soft-disable status flag (1=Active, 0=Disabled)',
  `unit_name` varchar(64) NOT NULL COMMENT 'Human-readable unit name (e.g., Meter, Kilogram, Second)',
  `created_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Immutable microsecond UTC creation timestamp',
  `updated_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6) COMMENT 'Microsecond UTC update timestamp',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_sys_um_uuid` (`uuid`),
  KEY `idx_sys_um_class` (`class_id`),
  CONSTRAINT `fk_sys_um_class` FOREIGN KEY (`class_id`) REFERENCES `sys_um_class` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_sys_um_obj` FOREIGN KEY (`id`) REFERENCES `obj_id` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Stores units of measure identified by their object ID and display symbol.';
```

```sql
CREATE TABLE `sys_um_class` (
  `id` bigint unsigned NOT NULL COMMENT 'Global obj_id identity allocated via AllocateId()',
  `uuid` binary(16) NOT NULL COMMENT 'Application UUID stored as binary for safe API exposure',
  `class_code` varchar(32) CHARACTER SET ascii COLLATE ascii_bin NOT NULL COMMENT 'Unique invariant code (e.g., LENGTH, MASS, ELECTRIC_ENERGY)',
  `class_name` varchar(64) NOT NULL COMMENT 'Unit class name (e.g., Length, Mass, Electric Current, Volume)',
  `base_unit_id` bigint unsigned DEFAULT NULL COMMENT 'Reference to the default base sys_um.id for this measurement class',
  `is_active` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Soft-disable status flag (1=Active, 0=Disabled)',
  `created_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Immutable microsecond UTC creation timestamp',
  `updated_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6) COMMENT 'Microsecond UTC update timestamp',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `uq_sys_umc_uuid` (`uuid`),
  UNIQUE KEY `uq_sys_umc_code` (`class_code`),
  KEY `fk_sys_umc_base_unit` (`base_unit_id`),
  CONSTRAINT `fk_sys_umc_base_unit` FOREIGN KEY (`base_unit_id`) REFERENCES `sys_um` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_sys_umc_obj` FOREIGN KEY (`id`) REFERENCES `obj_id` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Stores unit-of-measure classes;
```

```sql
CREATE TABLE `sys_um_collection` (
  `id` bigint unsigned NOT NULL COMMENT 'Global obj_id identity allocated via AllocateId()',
  `uuid` binary(16) NOT NULL COMMENT 'Application UUID stored as binary for safe API exposure',
  `collection_code` varchar(32) CHARACTER SET ascii COLLATE ascii_bin NOT NULL COMMENT 'Unique invariant code (e.g., BOX_12, PACK_6, PALLET_STD)',
  `umId` bigint unsigned NOT NULL DEFAULT '0',
  `collection_symbol` varchar(32) DEFAULT NULL COMMENT 'Optional shorthand symbol for the collection set',
  `base_unit_id` bigint unsigned DEFAULT NULL COMMENT 'Base unit of measure contained inside this collection set',
  `base_quantity` decimal(20,10) NOT NULL DEFAULT '1.0000000000' COMMENT 'Quantity of base units inside the set (e.g., 12.0 for a box of 12)',
  `is_active` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Soft-disable status flag (1=Active, 0=Disabled)',
  `collection_name` varchar(64) NOT NULL COMMENT 'Collection or package set name (e.g., Box of 12, Pack of 6)',
  `created_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Immutable microsecond UTC creation timestamp',
  `updated_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6) COMMENT 'Microsecond UTC update timestamp',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `uq_sys_umcoll_uuid` (`uuid`),
  UNIQUE KEY `uq_sys_umcoll_code` (`collection_code`),
  KEY `fk_sys_umcoll_unit` (`base_unit_id`),
  CONSTRAINT `fk_sys_umcoll_obj` FOREIGN KEY (`id`) REFERENCES `obj_id` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_sys_umcoll_unit` FOREIGN KEY (`base_unit_id`) REFERENCES `sys_um` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Stores collections or multiples of units of measure, such as sets or packages, with an optional symbol.';
```

```sql
CREATE TABLE `sys_um_relation` (
  `id` bigint unsigned NOT NULL COMMENT 'Global obj_id identity allocated via AllocateId()',
  `uuid` binary(16) NOT NULL COMMENT 'Application UUID stored as binary for safe API exposure',
  `from_unit_id` bigint unsigned NOT NULL COMMENT 'Source unit of measure ID (references sys_um.id)',
  `to_unit_id` bigint unsigned NOT NULL COMMENT 'Target unit of measure ID (references sys_um.id)',
  `gain_factor` decimal(20,10) NOT NULL DEFAULT '1.0000000000' COMMENT 'Multiplier factor applied during conversion',
  `offset_value` decimal(20,10) NOT NULL DEFAULT '0.0000000000' COMMENT 'Offset value added during conversion (output = input * gain + offset)',
  `relation_type` varchar(32) NOT NULL DEFAULT 'EXACT' COMMENT 'Type indicator: EXACT, APPROXIMATE, FINANCIAL',
  `is_bidirectional` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Flags whether reverse conversion rule can be mathematically inferred',
  `is_active` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Soft-disable status flag (1=Active, 0=Disabled)',
  `created_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Immutable microsecond UTC creation timestamp',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_sys_umr_uuid` (`uuid`),
  UNIQUE KEY `uq_sys_umr_units` (`from_unit_id`,`to_unit_id`),
  KEY `fk_sys_umr_to_unit` (`to_unit_id`),
  CONSTRAINT `fk_sys_umr_from_unit` FOREIGN KEY (`from_unit_id`) REFERENCES `sys_um` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_sys_umr_obj` FOREIGN KEY (`id`) REFERENCES `obj_id` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_sys_umr_to_unit` FOREIGN KEY (`to_unit_id`) REFERENCES `sys_um` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_umr_from_unit` FOREIGN KEY (`from_unit_id`) REFERENCES `sys_um` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_umr_to_unit` FOREIGN KEY (`to_unit_id`) REFERENCES `sys_um` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Stores conversion rules between units of measure using output equals input multiplied by gain plus offset.';
```

```sql
CREATE TABLE `workspace` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `workspace_type` varchar(24) CHARACTER SET ascii COLLATE ascii_bin NOT NULL COMMENT 'COMPANY, PERSONAL or HOUSEHOLD.',
  `name` varchar(255) NOT NULL COMMENT 'Workspace name shown to members.',
  `status` varchar(16) CHARACTER SET ascii COLLATE ascii_bin NOT NULL DEFAULT 'ACTIVE' COMMENT 'ACTIVE, SUSPENDED or CLOSED.',
  `legacy_company_id` bigint unsigned DEFAULT NULL COMMENT 'Optional link to the existing company table during migration.',
  `created_by_principal_id` bigint unsigned NOT NULL COMMENT 'Human or system principal that created the workspace.',
  `created_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `updated_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `closed_utc` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_workspace_legacy_company` (`legacy_company_id`),
  KEY `idx_workspace_type_status` (`workspace_type`,`status`),
  KEY `fk_workspace_created_by` (`created_by_principal_id`),
  CONSTRAINT `fk_workspace_created_by` FOREIGN KEY (`created_by_principal_id`) REFERENCES `security_principal` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_workspace_legacy_company` FOREIGN KEY (`legacy_company_id`) REFERENCES `legacy_company` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ck_workspace_status` CHECK ((`status` in (_utf8mb4'ACTIVE',_utf8mb4'SUSPENDED',_utf8mb4'CLOSED'))),
  CONSTRAINT `ck_workspace_type` CHECK ((`workspace_type` in (_utf8mb4'COMPANY',_utf8mb4'PERSONAL',_utf8mb4'HOUSEHOLD')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Tenant and data-isolation boundary for a company, individual consumer or household.';
```

```sql
CREATE TABLE `workspace_membership` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `workspace_id` bigint unsigned NOT NULL,
  `platform_user_id` bigint unsigned NOT NULL,
  `status` varchar(16) CHARACTER SET ascii COLLATE ascii_bin NOT NULL DEFAULT 'INVITED' COMMENT 'INVITED, ACTIVE, SUSPENDED or ENDED.',
  `invited_by_principal_id` bigint unsigned DEFAULT NULL COMMENT 'Inviting principal; NULL for approved self-registration.',
  `invited_utc` datetime(6) DEFAULT NULL,
  `joined_utc` datetime(6) DEFAULT NULL,
  `ended_utc` datetime(6) DEFAULT NULL,
  `updated_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_workspace_membership_user` (`workspace_id`,`platform_user_id`),
  UNIQUE KEY `uq_workspace_membership_id_workspace` (`id`,`workspace_id`),
  KEY `idx_workspace_membership_user_status` (`platform_user_id`,`status`),
  KEY `fk_workspace_membership_inviter` (`invited_by_principal_id`),
  CONSTRAINT `fk_workspace_membership_inviter` FOREIGN KEY (`invited_by_principal_id`) REFERENCES `security_principal` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_workspace_membership_user` FOREIGN KEY (`platform_user_id`) REFERENCES `platform_user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_workspace_membership_workspace` FOREIGN KEY (`workspace_id`) REFERENCES `workspace` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ck_workspace_membership_status` CHECK ((`status` in (_utf8mb4'INVITED',_utf8mb4'ACTIVE',_utf8mb4'SUSPENDED',_utf8mb4'ENDED')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Links one global user to company, personal and household workspaces.';
```

```sql
CREATE TABLE `workspace_membership_role` (
  `workspace_membership_id` bigint unsigned NOT NULL,
  `workspace_role_id` bigint unsigned NOT NULL,
  `workspace_id` bigint unsigned NOT NULL COMMENT 'Repeated workspace key used to prevent assigning a role from another workspace.',
  `assigned_by_principal_id` bigint unsigned NOT NULL,
  `assigned_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  `expires_utc` datetime(6) DEFAULT NULL COMMENT 'Optional UTC expiration for temporary membership roles.',
  PRIMARY KEY (`workspace_membership_id`,`workspace_role_id`),
  KEY `idx_workspace_membership_role_workspace` (`workspace_id`),
  KEY `fk_workspace_membership_role_membership` (`workspace_membership_id`,`workspace_id`),
  KEY `fk_workspace_membership_role_role` (`workspace_role_id`,`workspace_id`),
  KEY `fk_workspace_membership_role_assigner` (`assigned_by_principal_id`),
  CONSTRAINT `fk_workspace_membership_role_assigner` FOREIGN KEY (`assigned_by_principal_id`) REFERENCES `security_principal` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_workspace_membership_role_membership` FOREIGN KEY (`workspace_membership_id`, `workspace_id`) REFERENCES `workspace_membership` (`id`, `workspace_id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `fk_workspace_membership_role_role` FOREIGN KEY (`workspace_role_id`, `workspace_id`) REFERENCES `workspace_role` (`id`, `workspace_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Assigns one or more workspace roles to a membership.';
```

```sql
CREATE TABLE `workspace_permission` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `permission_code` varchar(160) CHARACTER SET ascii COLLATE ascii_bin NOT NULL COMMENT 'Stable module.action permission code; never translate this value.',
  `description` varchar(1000) NOT NULL COMMENT 'English technical explanation of the permission.',
  `module_code` varchar(64) CHARACTER SET ascii COLLATE ascii_bin NOT NULL COMMENT 'Owning PLO module code.',
  `active` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_workspace_permission_code` (`permission_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Catalogue of invariant machine-readable permissions used by workspace roles.';
```

```sql
CREATE TABLE `workspace_role` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `workspace_id` bigint unsigned NOT NULL,
  `role_code` varchar(64) CHARACTER SET ascii COLLATE ascii_bin NOT NULL COMMENT 'Stable role code within the workspace, such as OWNER, FAMILY_MEMBER or CAREGIVER.',
  `display_name` varchar(255) NOT NULL COMMENT 'Default English display name; localized UI text uses resources.',
  `description` varchar(1000) DEFAULT NULL,
  `is_system_role` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'True when application policy protects the role from ordinary deletion.',
  `active` tinyint(1) NOT NULL DEFAULT '1',
  `created_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_workspace_role_code` (`workspace_id`,`role_code`),
  UNIQUE KEY `uq_workspace_role_id_workspace` (`id`,`workspace_id`),
  CONSTRAINT `fk_workspace_role_workspace` FOREIGN KEY (`workspace_id`) REFERENCES `workspace` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Roles defined within one workspace;
```

```sql
CREATE TABLE `workspace_role_permission` (
  `workspace_role_id` bigint unsigned NOT NULL,
  `workspace_permission_id` bigint unsigned NOT NULL,
  `granted_utc` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
  PRIMARY KEY (`workspace_role_id`,`workspace_permission_id`),
  KEY `fk_workspace_role_permission_permission` (`workspace_permission_id`),
  CONSTRAINT `fk_workspace_role_permission_permission` FOREIGN KEY (`workspace_permission_id`) REFERENCES `workspace_permission` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_workspace_role_permission_role` FOREIGN KEY (`workspace_role_id`) REFERENCES `workspace_role` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Permissions granted to a workspace-scoped role.';
```