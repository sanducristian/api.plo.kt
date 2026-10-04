1\. Structure

I would use three projects initially:

Project	Responsibility

Kt.Api	HTTP endpoints, authentication, authorisation and business workflows

Kt.Data	MySQL connections, transactions, SQL and typed repositories

Kt.Contracts	Request/response DTOs shared with your C# client





The desktop application calls the API over HTTPS. Only Kt.Data, running on the server, accesses MySQL.

Within Kt.Data, I propose:

Component	Purpose

KtDb	Opens pooled connections and initialises database context

KtDbSession	Owns one connection and its optional transaction

KtIdentityRepository	Retrieves identity and authentication data

KtWorkspaceRepository	Retrieves memberships, roles and permissions

KtObjectRepository	Handles validated global-ID and dynamic-metadata operations

KtInvoiceRepository	Reads and writes invoice records



2\. Use explicit SQL first

MySqlConnector supports asynchronous access, connection pooling and Native AOT. That makes it a suitable foundation for your Linux deployment. MySqlConnector

For this database, I would initially use MySqlCommand and explicit reader mappings. This gives us direct control over:

\- Existing stored routines and triggers.

\- Global object allocation.

\- Workspace ownership joins.

\- Transactions spanning multiple tables.

\- Exact database-to-C# type conversions.

I would avoid generating a generic CRUD API for every table. Public operations should express business actions such as GetInvoiceAsync or CreateDraftInvoiceAsync; SQL execution helpers stay internal.



3\. Make audit context part of opening a connection

This is particularly important in your schema: GetUserId() reads @plo\_user\_id and returns a security principal ID.

For an authenticated operation, the wrapper should:

1\. Obtain the principal from the validated API session.

2\. Open a connection.

3\. Set @plo\_user\_id using a parameter.

4\. Validate the actor through GetUserId() before performing writes.

5\. Execute the operation.

6\. Dispose the connection, with connection reset enabled for pooled reuse.

Initialise context on every checkout; never rely on whatever a previous request left behind. MySqlConnector documents connection reset and pooling behaviour. MySqlConnector

The client must never supply the audit actor directly.

Login needs a separate, limited path because the user is not authenticated yet. Background jobs also need an explicit actor design: your current GetUserId() does not provide a general service-principal path.



4\. Treat workspace scope separately from identity

Your database separates:

\- security\_principal: the actor.

\- platform\_user: the human account.

\- workspace: company, personal or household context.

\- core\_legal\_entity: the legal entity.

The API should resolve these into a validated request context. Do not treat CompanyId, WorkspaceId and LegalEntityId as interchangeable.

Every protected repository operation must enforce the applicable ownership boundary in its SQL, including through parent records when the target table has no workspace column. This applies to lists, individual reads and updates.

The API checks action permissions; repository queries constrain which records that action can affect.



5\. Use one transaction for each complete business operation

For a kernel-backed record, creation should be atomic:

1\. Begin the transaction.

2\. Allocate its global ID through the validated allocation path.

3\. Insert the concrete record and required children.

4\. Write any required audit record.

5\. Commit.

All participating repositories use the same connection and transaction. An error rolls back the operation.

Your reference explicitly warns that some tables use AUTO\_INCREMENT independently. We must confirm the allocation rule per table rather than calling AllocateId() for every insert.

It also identifies broken old-name references in routines such as AllocateIdMaster and GetAttrId. Those should be reviewed before exposing corresponding wrapper methods.





6\. Keep types and API responses deliberate

Use:

\- ulong for BIGINT UNSIGNED identities internally.

\- String IDs in JSON contracts where browser clients must preserve full precision.

\- decimal for financial values, after checking database precision and range.

\- Explicit date/time handling matching each column’s meaning.

\- Dedicated response DTOs so password hashes and internal security fields cannot accidentally be returned.

All methods should accept a CancellationToken. List operations should have bounded pagination, and updates should check affected rows.

I would implement the first slice in this order:

1\. KtDb and KtDbSession, including actor context and transaction handling.

2\. KtIdentityRepository, connected to your existing IKtIdentityBackend.

3\. Workspace and permission retrieval.

4\. One complete supplier-invoice workflow.

5\. Focused MySQL integration tests for rollback, cross-workspace isolation and audit-actor separation between pooled requests.

