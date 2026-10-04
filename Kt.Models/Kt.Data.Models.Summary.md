# Kt.Data.Models Summary

## Project and dependencies

- `Kt.Data.Models.csproj` targets **.NET 10** (`net10.0`), enables implicit usings and nullable reference types, and declares no direct package or project references.
- The models project contains schema-oriented record types. Database functionality is in the separate `Kt.Data` project, which targets `net10.0`, references this models project, and has a direct `MySqlConnector` 2.6.2 dependency. `Kt.Data.Models` itself does not depend on that connector or configure a database.

## Database connection and DI

- **In the models project:** there is no connection-string handling, database provider, context, connection factory, or DI registration.
- **Implemented in `Kt.Data`:** `KtDb` accepts a connection string and creates a `MySqlDataSource`. It enables pooling, connection reset, and MySQL user variables. `OpenAsync` opens an actor-scoped connection, sets `@plo_user_id` and the session time zone to UTC, then validates the actor through `GetUserId()`. `KtDbSession` represents one connection and optional transaction; `InTransactionAsync` commits on success, while disposal cleans up uncommitted work. This is MySQL-specific behavior, not EF Core `DbContext` configuration.
- **Current API registration:** `Kt.API/Program.cs` does not instantiate or register `KtDb`, repositories, or a model database context. The API's `AddKtAuthentication<MyIdentityBackend>()` is not KtDb registration.
- **Examples/proposals, not active host configuration:** `Kt.Data/README.md` recommends singleton registration for `KtDb` and repositories, using the `ConnectionStrings:Plo` configuration key. That snippet is not present in the API startup code. `Kt.Data.IntegrationChecks` creates `KtDb` directly from the `KT_TEST_CONNECTION` environment variable for its test process. This summary deliberately omits all credential values.

## Model conventions and implementation status

The classes below are sealed records representing database rows, grouped by their `Kt.Data.Models` namespace. Their `[Table]` and `[Column]` attributes document table/column mappings and SQL types. Source remarks state that explicit repository mapping is required: these attributes do not execute SQL or enforce permissions, and SQL defaults are not applied by C#. These records are database models, not API contracts or insert requests.

No model source was found to contain TODOs, `NotImplementedException`, or a type explicitly designated as a placeholder/proposal. A defined record means the row shape is represented; it does not mean a complete business workflow or persistence integration exists. In particular, `Invoice.SettlementStatus` describes a state reserved for future bank/payment reconciliation; payment execution is explicitly not implemented. `DeviceOwnershipTransfer` carries proposed-owner and request-state data but does not itself perform an ownership transfer. Legacy and compatibility records are mappings, not migration/conversion services.

## Models by domain

### Accounting (`Kt.Data.Models.Accounting`)

- `AccountingPeriod` — accounting period dates and close state.
- `BankAccountMapping` — association between a bank account and accounting records.
- `CostCenter` — cost-center master data.
- `CountryPolicy` — country-specific accounting policy settings.
- `GlAccount` — general-ledger account definition.
- `Journal` — journal configuration.
- `JournalEntry` — accounting journal entry header.
- `JournalEntryLine` — debit/credit and account details for a journal entry.
- `PeriodCloseEvent` — accounting-period close history.
- `PostingRule` — accounting posting-rule data.
- `Project` — accounting project/cost object.
- `TaxCode` — tax-code definition.
- `TaxLedger` — tax ledger record.
- `TaxPeriod` — tax-period dates and status.

### Audit (`Kt.Data.Models.Audit`)

- `AuditEvent` — auditable business/action event data.
- `SystemEvent` — system event data.

### Companies (`Kt.Data.Models.Companies`)

- `CompanyOnboardingCase` — company onboarding case and state.
- `CompanyWorkspace` — company-to-workspace association.

### Compatibility (`Kt.Data.Models.Compatibility`)

- `CompanyAgency` — legacy company/agency association.
- `ComponentClass` — component classification.
- `ComponentIn`, `ComponentInItem` — inbound component record and its item rows.
- `ComponentOut`, `ComponentOutItem` — outbound component record and its item rows.
- `ComponentSupplier` — component/supplier association.
- `Conversion` — legacy conversion-factor record.
- `UserAccess`, `UserGroup` — legacy user access and group records.

### Core (`Kt.Data.Models.Core`)

- `BusinessPartner` — business-partner data.
- `CountryPack` — country-specific data/configuration package.
- `FinancialInstitution` — financial-institution reference data.
- `LegalEntity` — legal-entity identity and status.
- `LegalEntityCountryPack` — legal-entity-to-country-pack association.
- `Location`, `LocationType` — location data and its type classification.
- `Party` — shared person or organization identity.
- `PartyBankAccount` — bank-account details associated with a party.
- `PartyBankAccountEvent` — party bank-account history.
- `PartyRegistration` — registration data for a party.
- `PartyRegistrationVerification` — verification record for a party registration.

### Customs (`Kt.Data.Models.Customs`)

- `CostAllocation` — declaration-related cost allocation.
- `Declaration` — customs declaration header and status.
- `DeclarationDocument` — documents associated with a declaration.
- `DeclarationInvoice` — declaration-to-invoice association.
- `DeclarationItem` — goods item in a declaration.
- `DeclarationItemInvoiceLine` — link between a declaration item and invoice line.
- `ItemTariffAssessment` — tariff assessment for an item.
- `Office` — customs office reference data.
- `ProductClassification` — product classification data.
- `TariffClassification` — tariff classification data.
- `TariffMeasure` — tariff measure data.

### Devices (`Kt.Data.Models.Devices`)

- `DeviceChannel`, `DeviceChannelAssignment`, `DeviceChannelLatest` — channel definitions, assignments, and latest-value snapshots.
- `DeviceClaim` — device claim/request state.
- `DeviceComponentInstallation` — installed component association for a device.
- `DeviceDataStream` — device data-stream metadata.
- `DeviceDocument` — document associated with a device.
- `DeviceIdentifier` — device identifier and type.
- `DeviceLifecycleEvent` — device lifecycle history.
- `DeviceMeasurementDefinition` — measurement definition/metadata.
- `DeviceModel` — device model reference data.
- `DeviceOwnership`, `DeviceOwnershipTransfer` — ownership state and transfer-request data.
- `DeviceUnit` — device measurement unit.

### Finance (`Kt.Data.Models.Finance`)

- `BankReconciliationEvent` — bank reconciliation event history.
- `BankStatementImport`, `BankStatementLine` — imported statement and its lines.
- `BankStatementLineInvoiceMatch` — match between a statement line and invoice.
- `CountryPolicy` — finance-specific country policy data.
- `DocumentSequence` — document-number sequence state.
- `EinvoiceEvent`, `EinvoiceExchange`, `EinvoiceProfile` — e-invoice exchange events, exchanges, and profile/configuration data.
- `Invoice` — invoice header and document, party, accounting, and settlement state.
- `InvoiceApprovalEvent`, `InvoiceStatusEvent` — invoice approval and status histories.
- `InvoiceBankDetail` — invoice bank details.
- `InvoiceDocument`, `InvoiceImport` — invoice documents and intake/import records.
- `InvoiceLine` — invoice line details.
- `InvoiceLineAllocation` — invoice-line allocation data.
- `InvoiceLineMatch`, `InvoiceLinePurchaseOrderMatch`, `InvoiceLineReceiptMatch` — invoice-line matching records.
- `InvoiceLineTax`, `InvoiceTaxSummary` — line-level and summarized invoice tax data.
- `InvoiceRelation` — relationship between invoices.
- `LegalEntityBankAccount` — bank-account association for a legal entity.
- `OpenItem` — outstanding receivable/payable item data.
- `PaymentAllocation` — payment-to-open-item allocation data.
- `PaymentTerm` — payment-term definition.
- `Settlement` — settlement record.

### Fleet (`Kt.Data.Models.Fleet`)

- `FleetBattery`, `FleetBatteryInstallation`, `FleetBatteryState` — battery identity, installation, and state.
- `FleetChargeSession` — vehicle/battery charging session data.
- `FleetPositionLatest` — latest fleet-position snapshot.
- `FleetUsageInterval` — measured vehicle-usage interval.
- `FleetVehicle`, `FleetVehicleModel`, `FleetVehicleState` — vehicle, vehicle model, and state data.

### Identity (`Kt.Data.Models.Identity`)

- `PlatformPhoneNumber` — platform phone-number reference data.
- `PlatformUser` — platform user profile/status row.
- `PlatformUserAccountRecoveryCase`, `PlatformUserAccountRecoveryEvent` — account recovery case and history.
- `PlatformUserAgeProfile` — user age/profile data.
- `PlatformUserEmail`, `PlatformUserPhone` — user email and phone contact records.
- `PlatformUserGuardianConsent`, `PlatformUserGuardianRelationship` — guardian consent and relationship records.
- `PlatformUserIdentity` — identity/verification data for a platform user.
- `PlatformUserMfaMethod` — multi-factor authentication method record.
- `PlatformUserOnboarding`, `PlatformUserOnboardingEvent` — onboarding state and history.
- `PlatformUserReferralAttribution`, `PlatformUserReferralCode`, `PlatformUserReferralProgram` — referral attribution, code, and program data.
- `SecurityDbScriptActor` — database-script actor/audit metadata.
- `SecurityPrincipal` — security principal identity and status.

### Legacy (`Kt.Data.Models.Legacy`)

- `LegacyCompany`, `LegacyPerson`, `LegacyUser` — legacy company, person, and user rows.
- `LegacyComponent`, `LegacyInvoice`, `LegacyOrder` — legacy component, invoice, and order rows.
- `LegacySysrole`, `LegacySystable` — legacy role and table metadata.
- `LegacyUserMapping` — mapping between a legacy user and its platform identity.

### Manufacturing (`Kt.Data.Models.Manufacturing`)

- `Bom`, `BomItem` — bill of materials and component rows.
- `MaterialConsumption` — material consumed in production.
- `OutputUnit` — production output unit.
- `Routing`, `RoutingOperation` — production routing and operations.
- `WorkOrder`, `WorkOrderOperation`, `WorkOrderEvent` — work-order data, operation rows, and event history.

### Objects (`Kt.Data.Models.Objects`)

- `ObjectIdentity` — global object identity.
- `ObjectTable` — registered static-object table metadata.
- `ObjectType` — object type metadata.

#### Dynamic object values (`Kt.Data.Models.Objects.Dynamic`)

- `ObjectDynAttributeValue`, `ObjectDynGenericValue` — dynamic attribute/value metadata and generic value representation.
- `ObjectDynBlobValue`, `ObjectDynFile` — blob and file values.
- `ObjectDynDateValue`, `ObjectDynDateTimeValue`, `ObjectDynTimeValue` — date/time values.
- `ObjectDynDoubleValue`, `ObjectDynIntegerValue`, `ObjectDynNumber` — numeric values.
- `ObjectDynGeoPose`, `ObjectDynLocation` — geographic pose and location values.
- `ObjectNumberClass`, `ObjectPrice`, `ObjectDynQuantity` — number-class, price, and quantity values.
- `ObjectDynReference`, `ObjectDynStringValue` — reference and string values.

### Procurement (`Kt.Data.Models.Procurement`)

- `PurchaseOrder`, `PurchaseOrderLine` — purchase-order header and line data.

### Reference (`Kt.Data.Models.Reference`)

- `Country`, `CountryRegion`, `CountryRegionTranslation`, `CountryTranslation` — country/region reference data and translations.
- `Currency` — currency reference data.
- `Language` — language reference data.
- `UnitOfMeasure`, `UnitOfMeasureClass`, `UnitOfMeasureCollection`, `UnitOfMeasureRelation` — unit-of-measure definitions, classes, collections, and relations.

### Support (`Kt.Data.Models.Support`)

- `SupportAccessGrant` — support-access grant data.
- `SupportAccessScope` — scope attached to support access.

### System (`Kt.Data.Models.System`)

- `DatabaseMigration` — database migration history row.
- `SystemSetting` — system setting key/value metadata.
- `SystemVersion` — system version record.

### Transport (`Kt.Data.Models.Transport`)

- `TransportCargoLine` — cargo line for a transport mission.
- `TransportEvent` — transport event history.
- `TransportMission`, `TransportMissionStop` — mission header and stop rows.
- `TransportVehicleAssignment` — vehicle assignment for a mission.

### Warehouse (`Kt.Data.Models.Warehouse`)

- `GoodsReceipt`, `GoodsReceiptLine`, `GoodsReceiptLineOrderAllocation` — receipt header, lines, and order allocations.
- `HandlingUnit` — warehouse handling unit.
- `StockMovement` — inventory movement.
- `StorageLocation` — warehouse storage location.
- `Warehouse` — warehouse identity and configuration.

### Workspaces (`Kt.Data.Models.Workspaces`)

- `Workspace` — workspace identity and status.
- `WorkspaceAdministrator` — workspace administrator record.
- `WorkspaceInvitation` — invitation and its state.
- `WorkspaceMembership`, `WorkspaceMembershipRole` — workspace membership and assigned role.
- `WorkspacePermission`, `WorkspaceRole`, `WorkspaceRolePermission` — permission catalog, role, and role-to-permission association.
