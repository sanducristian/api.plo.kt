# Kt.Data

Initial C# data-access library for the KT-PLO Linux API. Targets .NET 8 or a compatible newer application; MySqlConnector is pinned to 2.6.2. Source schema: PLO_Complete_Database_Project_Reference_2026-09-26.md (MySQL 8.0.46 snapshot).

## Included

- Application-owned pooled data source, async disposal and cancellation.
- Actor initialisation and validation through the existing GetUserId() routine.
- Transaction session and callback with automatic rollback on disposal when uncommitted.
- Current active user profile retrieval.
- Paginated active workspace membership listing.
- Explicit workspace permission checks with active user, principal, workspace, membership, role and permission checks, plus assignment/expiration times.

This first slice implements the data-access foundation and authenticated reads. It does not implement password validation, session storage, invoice writes or global-ID allocation. No tables or routines are changed. No unknown IKtIdentityBackend interface implementation is fabricated.

## Add to the solution

Extract this folder beside your API project. In Visual Studio use **Add > Existing Project**, select Kt.Data.csproj, then add a project reference from the API to Kt.Data.

Alternatively, from the API project directory:

```sh
dotnet add reference ../Kt.Data/Kt.Data.csproj
dotnet restore
dotnet build
```

## Register in Program.cs

```csharp
using Kt.Data;
using Kt.Data.Repositories;

builder.Services.AddSingleton<KtDb>(_ => new KtDb(
    builder.Configuration.GetConnectionString("Plo")
    ?? throw new InvalidOperationException("ConnectionStrings:Plo is missing.")));
builder.Services.AddSingleton<KtIdentityRepository>();
builder.Services.AddSingleton<KtWorkspaceRepository>();
```

Factory registration lets dependency injection dispose the data source during host shutdown. Repositories hold no request state. Sessions belong to an operation and must be disposed; never register an open session as a singleton.

Set `ConnectionStrings__Plo` through your deployment secret configuration. Example value (replace placeholders; do not commit real secrets):

```text
Server=db.example.internal;Database=plo;User ID=kt_api;Password=REPLACE_ME;SslMode=VerifyFull;Connection Timeout=10;Default Command Timeout=30;Maximum Pool Size=50;
```

Configure the certificate trust appropriate to the actual database. For a local Unix socket, configure the corresponding driver options. Pool size 50 is an initial example, not a measured capacity requirement. Use a dedicated least-privilege DB account, not a migration or root login.

## Read a profile and workspaces

```csharp
// authenticatedPrincipalId is a ulong resolved by YOUR validated authentication code.
// Never obtain it directly from an untrusted route, header or request-body user ID.
await using var session = await db.OpenAsync(authenticatedPrincipalId, cancellationToken);
var profile = await identityRepository.GetCurrentAsync(session, cancellationToken);
var workspaces = await workspaceRepository.ListCurrentAsync(
    session, afterId: 0, limit: 100, cancellationToken: cancellationToken);
```

The repository model `Kt.Data.KtUserProfile` is not your existing API profile type. Map it explicitly to that type or qualify the name if there is a collision. It excludes credentials and internal authentication secrets. Map `ulong` IDs to strings in browser-facing DTOs. Native AOT hosts must also register their own JSON DTOs in a source-generated serialization context.

## Check a grant

```csharp
bool permitted = await workspaceRepository.HasPermissionAsync(
    session, selectedWorkspaceId, requiredPermissionCode, cancellationToken);
```

Use permission codes that actually exist in your database. This checks ONE workspace grant, not complete authorisation. The API must also check application entry, session requirements, application enablement, resource ownership and workflow conditions. No automatic OWNER, administrator or support override is inferred. Membership listings are context choices, not lists of applications the user may enter.

Resource writes must include their own scope predicates. A previous permission read does not make a subsequent unscoped update safe. For sensitive transitions, define appropriate transactional locking and revalidation.

## Transaction use

`KtDb.InTransactionAsync<T>` opens an actor session, begins a transaction, calls the supplied asynchronous operation and commits only on success. Extend repositories inside Kt.Data using the internal `session.CreateCommand` helper, which automatically attaches the active transaction. Do not open another connection inside that operation. Do not call commit or rollback inside the callback; let the wrapper own completion.

For manual transactions use OpenAsync, BeginTransactionAsync and CommitAsync. Dispose an uncommitted session to roll back. Do not share sessions across threads or use parallel commands on the same session. After a database failure, dispose the session rather than reusing it. No automatic write retries are performed; uncertain commit outcomes require application reconciliation/idempotency.

## Actor and time handling

Every checkout sets @plo_user_id and the SQL session time zone to UTC before validating GetUserId(). Pool connection reset is always enabled. Rollback is not a session-variable reset. Actor initialisation uses an explicitly typed UInt64 parameter. The resolver currently accepts active human USER principals through this route.

This API intentionally has no anonymous/pre-authentication session entry point yet. Login credentials must be handled by a separate, constrained repository once the actual authentication interface and credential mapping are supplied. Background job actor handling likewise needs a deliberate extension to the current resolver.

Permission checks treat assigned_utc as the assignment effective time and reject future-dated assignments; confirm this conservative policy if importing historical data.

## Verification status and required checks

Source mappings were reviewed against the supplied DDL. The delivery environment has no dotnet executable and no MySQL connection, so compilation, Native AOT publishing and database integration have NOT been run. Run dotnet build before integration.

On a disposable database copied from the current schema, verify:

1. OpenAsync accepts an ACTIVE user/USER principal and rejects missing, suspended, disabled and non-human actors.
2. Set Maximum Pool Size=1. Alternate sessions for two valid users; GetCurrentAsync must always return the current actor. Repeat after cancellation/failure and disposal.
3. Verify workspace lists exclude inactive memberships/workspaces and paginate without duplicates.
4. Verify HasPermissionAsync returns false for another workspace, absent permissions, inactive roles/permissions, expired assignments and inactive accounts; verify the positive case.
5. When the first write repository is added, deliberately fail after its first write and verify the entire operation rolls back; repeat with a successful commit.
6. Publish and exercise the actual API binary on Linux, including its JSON source generation if Native AOT is enabled.

No integration-test results are claimed. Global-ID and invoice repositories are the next slice after reviewing the concrete insert paths; known broken old-name routines must not be called blindly.

## References

- https://mysqlconnector.net/api/mysqlconnector/mysqldatasourcetype/
- https://mysqlconnector.net/connection-options/
- https://mysqlconnector.net/overview/version-history/
