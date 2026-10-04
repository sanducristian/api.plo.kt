namespace Kt.Api.Authentication;


public class MyIdentityBackend : IKtIdentityBackend {
    public System.Threading.Tasks.Task<KtVerifiedIdentity?> VerifyCredentialsAsync(string login, string password, string? otp, string? remoteIp, System.Threading.CancellationToken cancellationToken) {

        Console.WriteLine("Call to VerifyCredentialsAsync with login: " + login + ", password: " + password + ", otp: " + otp + ", remoteIp: " + remoteIp);
        // Minimal placeholder: return no verified identity.
        return System.Threading.Tasks.Task.FromResult<KtVerifiedIdentity?>(null);

        // lets return a dummy 
    }

    public System.Threading.Tasks.Task<KtAccess?> GetCurrentAccessAsync(string userId, string application, string? companyId, System.Threading.CancellationToken cancellationToken) {
        Console.WriteLine("Call to GetCurrentAccessAsync with userId: " + userId + ", application: " + application + ", companyId: " + companyId);
        //return System.Threading.Tasks.Task.FromResult<KtAccess?>(new KtAccess(userId, application, companyId, new[] { "User" }, new[] { "Read" }, "1.0"));
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


