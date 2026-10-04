using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kt.Authentication.Internal;

internal sealed class KtSessionAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IKtSessionValidator validator,
    TimeProvider timeProvider)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var headers = Request.Headers.Authorization;
        if (headers.Count == 0)
            return AuthenticateResult.NoResult();

        if (headers.Count != 1 ||
            !AuthenticationHeaderValue.TryParse(headers[0], out var header) ||
            !string.Equals(header.Scheme, "Bearer", StringComparison.OrdinalIgnoreCase) ||
            string.IsNullOrWhiteSpace(header.Parameter) ||
            header.Parameter.Length > 8192 ||
            header.Parameter.Any(char.IsWhiteSpace) ||
            header.Parameter.Contains(','))
        {
            return AuthenticateResult.Fail("Invalid authentication header.");
        }

        // An infrastructure failure propagates to the host exception handler.
        // It can never become an authenticated request.
        var session = await validator.ValidateAsync(
            header.Parameter, Context.RequestAborted);

        if (session is null || session.ExpiresAt <= timeProvider.GetUtcNow() ||
            string.IsNullOrWhiteSpace(session.User.UserId))
        {
            return AuthenticateResult.Fail("Invalid or expired session.");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, session.User.UserId)
        };

        if (!string.IsNullOrWhiteSpace(session.User.DisplayName))
            claims.Add(new Claim(ClaimTypes.Name, session.User.DisplayName));

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var properties = new AuthenticationProperties
        {
            ExpiresUtc = session.ExpiresAt
        };

        return AuthenticateResult.Success(new AuthenticationTicket(
            new ClaimsPrincipal(identity), properties, Scheme.Name));
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        Response.Headers.WWWAuthenticate = "Bearer";
        return Task.CompletedTask;
    }

    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    }
}
