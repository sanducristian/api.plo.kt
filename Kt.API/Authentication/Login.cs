using Kt.Api.Session;
using System.Text.Json;

namespace Kt.Api.Authentication;

public class Login {

    private static async Task<IResult> LoginAsync(KtLoginRequest request, HttpContext http, IKtIdentityBackend backend, IKtSessionStore sessions, CancellationToken cancellationToken) {
        var remoteIp = http.Connection.RemoteIpAddress?.ToString();

        Console.WriteLine("Called LoginAsync with request: " + JsonSerializer.Serialize(request, KtAuthJsonContext.Default.KtLoginRequest) + ", remoteIp: " + remoteIp);

        var identity = await backend.VerifyCredentialsAsync(request.Login, request.Password, request.Otp, remoteIp, cancellationToken);
        if (identity is null)
            return Results.Unauthorized();

        var access = await backend.GetCurrentAccessAsync(identity.UserId, request.Application, request.CompanyId, cancellationToken);
        if (access is null)
            return Results.Forbid();

        var session = await sessions.CreateSessionAsync(
            userId: access.UserId,
            securityVersion: access.SecurityVersion,
            application: request.Application,
            companyId: request.CompanyId,
            client: "web",
            cancellationToken: cancellationToken
        );

        //      var session = await sessions.CreateSessionAsync(access, cancellationToken);
        http.Response.Cookies.Append(KtAuthenticationApi.SessionCookieName, session.SessionId, new CookieOptions {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = session.ExpiresUtc
        });

        return Results.Ok(new KtLoginResponse(
            AccessToken: session.SessionId,
            TokenType: "Bearer",
            ExpiresIn: (int)(session.ExpiresUtc - DateTimeOffset.UtcNow).TotalSeconds,
            ExpiresAt: session.ExpiresUtc,
            User: new KtUserProfile(
                UserId: access.UserId,
                DisplayName: access.DisplayName,
                Email: access.Email,
                Application: request.Application,
                CompanyId: request.CompanyId,
                Permissions: access.Permissions
            )
        ));
    }

}
