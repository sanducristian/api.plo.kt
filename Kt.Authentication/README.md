# Kt.Authentication

Shared server-side authentication foundation for KT-PLO API modules.
Targets .NET 8; a newer .NET host can reference this library.

## Responsibilities

- One ASP.NET Core authentication scheme: `KtSession`.
- One authentication-only policy: `Kt.Authenticated`.
- A scoped `IKtCurrentUser` service for every API module.
- An `IKtSessionValidator` contract for the existing identity/session backend.
- Bearer-header parsing, expiry checks, trusted identity construction, 401 and 403 responses.

This is a new foundation, not a migration of the current C# auth implementation:
its actual source files were not supplied. No SQL schema or backend has been invented.

## Dependency direction

The API host and each API module reference Kt.Authentication. Kt.Authentication
does not reference Kt.Api, Kt.Data or IKtApiModule. The host's adapter implements
IKtSessionValidator and can reference Kt.Data and the existing session store.
Future module DLLs reference this same shared assembly; they do not register
another authentication scheme or ship an independent identity system.

## Add the project

Copy this folder beside Kt.Api, then run from your solution directory:

```sh
dotnet sln add Kt.Authentication/Kt.Authentication.csproj
dotnet add Kt.Api/Kt.Api.csproj reference Kt.Authentication/Kt.Authentication.csproj
dotnet build Kt.Authentication/Kt.Authentication.csproj
```

For a separate module project, add the same project reference to that project.

## Host integration

Implement `MyKtSessionValidator : IKtSessionValidator` against the actual session
store and identity backend. That is an adapter name in this example, not a supplied class.
Its successful result contains the authoritative user ID, optional display name and
session expiry. Return null for failed authentication. Account disablement, session
revocation, security-version changes and intended-recipient checks belong in this adapter.
Validate on every request initially; any later cache must have a documented revocation
bound. Do not catch backend errors and return an identity.

```csharp
using Kt.Authentication;

// Keep backend/session-store service registrations from the existing implementation.
builder.Services.AddProblemDetails();
builder.Services.AddKtSessionAuthentication<MyKtSessionValidator>();

var app = builder.Build();
app.UseForwardedHeaders(); // Retain trusted-proxy configuration in the host.
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

// Existing KtApi dispatcher from the modular API design:
app.MapKtApi(KtAuthenticationDefaults.Policy);
app.Run();
```

Do not simply stack this registration on top of the previous AddKtAuthentication.
Move its required backend registrations, replace its request-validation handler with
this scheme, and ensure there is one owner of each route. Keep the existing login/logout
handlers until they can be migrated using their actual source code. Do not delete their
backend registrations or change client URLs during this extraction.

## Use from any sub-API

Inside `KtUsersApi.MapEndpoints(RouteGroupBuilder group)`:

```csharp
group.MapGet("/me/id", (IKtCurrentUser current) =>
{
    var user = current.User;
    return user is null
        ? Results.Unauthorized()
        : Results.Text(user.UserId);
}).RequireAuthorization(KtAuthenticationDefaults.Policy);
```

This returns only an identity string, not the complete user profile.
The module uses `Microsoft.AspNetCore.Builder`, `Microsoft.AspNetCore.Http`,
`Microsoft.AspNetCore.Routing`, and `Kt.Authentication` namespaces.

Never take the current user's identity from a query parameter, request body, or an
unvalidated header. IKtCurrentUser is available only in the current HTTP request;
background work needs its own explicit identity. Do not capture it in singleton modules.

## Transport and scope

This version accepts only `Authorization: Bearer <session-token>`. The token is
opaque to this library: the adapter must validate it, not merely parse it.
It is compatible with a login response that returns a server-side session ID as its
AccessToken once the validator adapter is connected. No assumption about that store's
method signatures is made here.

Cookie authentication is NOT implemented in this version. Existing cookie-only clients
must keep their existing handler until cookie validation and CSRF protection are
integrated deliberately. Do not silently start accepting cookie secrets in this handler.
Credentials in query strings are not supported. Require HTTPS at the public ingress,
including when Nginx terminates TLS. Exclude Authorization headers and tokens from logs.

Login, password verification, MFA, token issuance, logout/revocation storage, rate limits,
recovery and social login remain in the existing authentication implementation pending
source-based integration. This package is not a replacement implementation for them.

The policy only proves identity. Application entry, active company membership,
personal ownership, action permissions, record scope and workflow constraints must be
enforced by the authorisation layer. No roles, company claims or automatic grants are
created here. Business endpoints remain closed until their access policy is connected.

## Verification status and acceptance checks

The files were reviewed structurally. The generation environment had no dotnet SDK,
so compilation and runtime tests have not been executed. Build in your solution first.
Before integration is accepted, exercise a protected endpoint with:

1. No Authorization header: 401 and WWW-Authenticate: Bearer.
2. Malformed, duplicated, unknown or invalid credentials: 401.
3. Valid backend-approved session: endpoint succeeds with the backend's user ID.
4. Expired session, including expiry exactly at current time: 401.
5. Revoked session, disabled account or changed security version: adapter rejects, 401.
6. Backend outage: server error through exception handling, never authenticated success.
7. Authenticated user lacking the endpoint's separate business permission: 403.
8. Company A user requesting Company B's records: denied by resource authorisation.

Source: ASP.NET Core authentication scheme/handler documentation:
https://learn.microsoft.com/en-us/aspnet/core/security/authentication/
