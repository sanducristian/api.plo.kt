namespace Kt.Api.Authentication;

using Kt.Data;
using Kt.Data.Repositories;


public class MyIdentityBackend : IKtIdentityBackend {

    private readonly KtDb _db;
    private readonly KtIdentityRepository _identities;
    private readonly KtWorkspaceRepository _workspaces;


    public MyIdentityBackend(KtDb db, KtIdentityRepository identities, KtWorkspaceRepository workspaces) {
        _db = db;
        _identities = identities;
        _workspaces = workspaces;
    }



    public System.Threading.Tasks.Task<KtVerifiedIdentity?> VerifyCredentialsAsync(string login, string password, string? otp, string? remoteIp, System.Threading.CancellationToken cancellationToken) {

        // Console.WriteLine("Call to VerifyCredentialsAsync with login: " + login + ", password: " + password + ", otp: " + otp + ", remoteIp: " + remoteIp);
        cancellationToken.ThrowIfCancellationRequested();

        // No database verification is implemented yet.
        // Never authenticate a user or log their password/OTP.
        return Task.FromResult<KtVerifiedIdentity?>(null);
    }



    private async Task<bool> HasWorkspacePermissionAsync(ulong authenticatedPrincipalId, ulong workspaceId, string permissionCode, CancellationToken cancellationToken) {
        cancellationToken.ThrowIfCancellationRequested();

        if (authenticatedPrincipalId == 0 ||
            workspaceId == 0 ||
            string.IsNullOrWhiteSpace(permissionCode)) {
            return false;
        }

        // Must originate from verified authentication or a validated session.
        // Never pass a user ID taken directly from a request body.
        await using var session = await _db.OpenAsync(authenticatedPrincipalId, cancellationToken);
        var user = await _identities.GetCurrentAsync(session, cancellationToken);
        if (user is null)
            return false;

        return await _workspaces.HasPermissionAsync(
            session,
            workspaceId,
            permissionCode,
            cancellationToken);
    }



    public System.Threading.Tasks.Task<KtAccess?> GetCurrentAccessAsync(string userId, string application, string? companyId, System.Threading.CancellationToken cancellationToken) {
        Console.WriteLine("Call to GetCurrentAccessAsync with userId: " + userId + ", application: " + application + ", companyId: " + companyId);
        //return System.Threading.Tasks.Task.FromResult<KtAccess?>(new KtAccess(userId, application, companyId, new[] { "User" }, new[] { "Read" }, "1.0"));

        cancellationToken.ThrowIfCancellationRequested();


        return System.Threading.Tasks.Task.FromResult<KtAccess?>(new KtAccess(
            UserId: userId,
            DisplayName: application,
            Email: companyId,
            SecurityVersion: "1.0",
            Permissions: new[] { "Read", "Write", "Delete" }
        ));

        // Dummy: lets retrun a valid access
        //return System.Threading.Tasks.Task.FromResult<KtAccess?>(new KtAccess {
        //    UserId = userId,
        //    DisplayName = "Dummy User",
        //    Email = "test@test.com",
        //    SecurityVersion = "1.0",
        //    //Application = application,
        //    //CompanyId = companyId,
        //    //Roles = new[] { "User", "Admin" },
        //    Permissions = new[] { "Read", "Write", "Delete" }
        //});


        // Minimal placeholder: return no access.
        //return System.Threading.Tasks.Task.FromResult<KtAccess?>(null);
    }
}


