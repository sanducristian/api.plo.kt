#nullable enable
// KT-System shared authentication API — first integration version.
// Target: ASP.NET Core 8+ Web SDK; no third-party packages.
// No SQL/schema assumptions. Implement IKtIdentityBackend using the current database.
//
// PROGRAM.CS:
// var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddKtAuthentication<MyIdentityBackend>();
// var app = builder.Build();
// // If behind a proxy, configure trusted proxies and UseForwardedHeaders FIRST.
// app.UseHttpsRedirection();
// app.UseRouting();
// app.UseRateLimiter();
// app.UseAuthentication();
// app.UseAuthorization();
// app.MapKtAuthentication();
// // Other endpoints MUST use RequireAuthorization(KtAuthenticationApi.Policy).
// // Also enforce their expected kt_application, company and granular permissions.
// app.Run();
//
// HTTPS is mandatory. Native clients send Authorization: Bearer <accessToken>.
// Web clients use the same header; keep the token in memory, not localStorage.
// No cookies are used. Serve web/API at the same origin or configure an explicit
// CORS origin allowlist in the host; never treat CORS as authentication.
//
// Sessions here are server-side, opaque (NOT JWT), expire after 30 minutes and
// are revoked immediately on logout. No refresh token/social login in this version.
// The included bounded memory store is SINGLE-PROCESS ONLY; restarting logs users
// out. Replace IKtSessionStore with shared persistent storage before scale-out.
// This does not accept existing PHP JWT/Digest tokens without a migration adapter.
// Never log passwords, OTPs, Authorization headers or successful login bodies.
//
// Native AOT: source-generated JSON metadata is included. Publish/build validation
// is still required with the actual host and database driver.

using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;



namespace Kt.Api.Authentication;

public static class KtAuthenticationApi {
    public const string Scheme = "KtSession";
    public const string Policy = "KtAuthenticated";
    private const string LoginLimit = "KtLogin";
    internal const string SessionItem = "KtValidatedSession";
    public const string SessionCookieName = "kt_session";
    internal static readonly TimeSpan Lifetime = TimeSpan.FromMinutes(30);
    private static readonly string[] Applications =
        { "plo.kt.live", "kt.cab", "kt.delivery", "kt.aero", "kt.live" };

    public static IServiceCollection AddKtAuthentication<TBackend>(this IServiceCollection services)
        where TBackend : class, IKtIdentityBackend {
        services.AddScoped<IKtIdentityBackend, TBackend>();
        services.TryAddSingleton<IKtSessionStore, KtMemorySessionStore>();
        services.ConfigureHttpJsonOptions(o =>
            o.SerializerOptions.TypeInfoResolverChain.Insert(0, KtAuthJsonContext.Default));
        services.AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, KtSessionHandler>(Scheme, _ => { });
        services.AddAuthorization(o => o.AddPolicy(Policy, p => {
            p.AddAuthenticationSchemes(Scheme);
            p.RequireAuthenticatedUser();
        }));
        services.AddRateLimiter(o => {
            o.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            o.AddPolicy(LoginLimit, context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    _ => new FixedWindowRateLimiterOptions {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0,
                        AutoReplenishment = true
                    }));
        });
        return services;
    }


    /*
     * Map endpoints for login, logout and current user profile.
     * All endpoints are HTTPS-only and return JSON errors on failure.
     * Login is anonymous with rate limiting. Logout and Me require authentication.
     * All other endpoints MUST use RequireAuthorization(KtAuthenticationApi.Policy).
     */
    
    public static IEndpointRouteBuilder MapKtAuthentication(this IEndpointRouteBuilder endpoints) {
        var group = endpoints.MapGroup("/api/auth");
        group.AddEndpointFilter(async (context, next) => {
            context.HttpContext.Response.Headers.CacheControl = "no-store";
            context.HttpContext.Response.Headers.Pragma = "no-cache";
            if (!context.HttpContext.Request.IsHttps)
                return Results.Json(new KtAuthError("https_required"), KtAuthJsonContext.Default.KtAuthError, statusCode: 400);
            return await next(context);
        });
        group.MapPost("/login", LoginAsync).AllowAnonymous().RequireRateLimiting(LoginLimit);
        group.MapGet("/me", Me).RequireAuthorization(Policy);
        group.MapPost("/logout", LogoutAsync).RequireAuthorization(Policy);
        return endpoints;
    }

    private static async Task<IResult> LoginAsync(
        KtLoginRequest request, HttpContext http, IKtIdentityBackend backend,
        IKtSessionStore sessions, CancellationToken cancellationToken) {
        var app = request.Application?.Trim().ToLowerInvariant();
        var client = request.Client?.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(request.Login) || request.Login.Length > 254 ||
            string.IsNullOrEmpty(request.Password) || request.Password.Length > 1024 ||
            app is null || !Applications.Contains(app, StringComparer.Ordinal) ||
            client is not ("web" or "mobile" or "windows") ||
            request.CompanyId?.Length > 128 || request.CompanyId == "" ||
            request.Otp?.Length > 64)
            return Results.Json(new KtAuthError("invalid_request"), KtAuthJsonContext.Default.KtAuthError, statusCode: 400);

        // Client is descriptive metadata, never a trusted client identity.
        // Password is deliberately not trimmed/normalized.
        var verified = await backend.VerifyCredentialsAsync(
            request.Login.Trim(), request.Password, request.Otp,
            http.Connection.RemoteIpAddress?.ToString(), cancellationToken);
        if (verified is null)
            return Results.Json(new KtAuthError("invalid_credentials"), KtAuthJsonContext.Default.KtAuthError, statusCode: 401);

        /// Validate that the user still has access to the requested application and company.
        var user = await backend.GetCurrentAccessAsync(verified.UserId, app, request.CompanyId, cancellationToken);
        if (user is null || user.UserId != verified.UserId ||
            user.SecurityVersion != verified.SecurityVersion)
            return Results.Json(new KtAuthError("access_denied"), KtAuthJsonContext.Default.KtAuthError, statusCode: 403);

        /// Create a new session and store it in the session store.
        var now = DateTimeOffset.UtcNow;
        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        var session = new KtSession(user.UserId, user.SecurityVersion, app, request.CompanyId, client!, now.Add(Lifetime));
        await sessions.PutAsync(Hash(token), session, cancellationToken);
        return Results.Ok(new KtLoginResponse(token, "Bearer", (int)Lifetime.TotalSeconds, session.ExpiresAt, ToProfile(user, session)));
    }



    /// <summary>
    /// Gets the profile of the currently authenticated user.
    /// </summary>
    /// <param name="http">The current HttpContext with an authenticated user.</param>
    /// <returns>The profile of the currently authenticated user.</returns>
    private static IResult Me(HttpContext http) {
        var current = (KtValidatedSession)http.Items[SessionItem]!;
        return Results.Ok(ToProfile(current.User, current.Session));
    }



    /// <summary>
    /// Logs out the current user by removing their session from the session store.
    /// </summary>
    /// <param name="http">The current HttpContext with an authenticated user.</param>
    /// <param name="sessions">The session store to remove the current session from.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A 204 No Content result on success, or an error result on failure.</returns>
    private static async Task<IResult> LogoutAsync(HttpContext http, IKtSessionStore sessions, CancellationToken cancellationToken) {
        var current = (KtValidatedSession)http.Items[SessionItem]!;
        await sessions.RemoveAsync(current.TokenHash, cancellationToken);
        return Results.NoContent();
    }



    /// <summary>
    /// Hash a token using SHA256 and return the hex string representation.
    /// </summary>
    /// <param name="token">The token to hash.</param>
    /// <returns>The hex string representation of the hashed token.</returns>
    internal static string Hash(string token) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

    private static KtUserProfile ToProfile(KtAccess user, KtSession session) =>
        new(user.UserId, user.DisplayName, user.Email, session.Application,
            session.CompanyId, user.Permissions);
}



public sealed class KtSessionHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
    private readonly IKtSessionStore _sessions;
    private readonly IKtIdentityBackend _backend;

    public KtSessionHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder,
        IKtSessionStore sessions, IKtIdentityBackend backend)
        : base(options, logger, encoder) {
        _sessions = sessions;
        _backend = backend;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
        if (!Request.IsHttps)
            return AuthenticateResult.Fail("HTTPS required.");
        var header = Request.Headers.Authorization;
        if (header.Count == 0) return AuthenticateResult.NoResult();
        if (header.Count != 1 || header[0] is not string value ||
            !value.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return AuthenticateResult.Fail("Invalid authorization.");
        var token = value[7..];
        if (token.Length != 64 || !token.All(Uri.IsHexDigit))
            return AuthenticateResult.Fail("Invalid session.");
        // Tokens are case sensitive, even though represented in hexadecimal.
        var hash = KtAuthenticationApi.Hash(token);
        var session = await _sessions.GetAsync(hash, Context.RequestAborted);
        if (session is null || session.ExpiresAt <= DateTimeOffset.UtcNow)
            return AuthenticateResult.Fail("Invalid session.");
        var user = await _backend.GetCurrentAccessAsync(
            session.UserId, session.Application, session.CompanyId, Context.RequestAborted);
        if (user is null || user.UserId != session.UserId ||
            user.SecurityVersion != session.SecurityVersion) {
            await _sessions.RemoveAsync(hash, Context.RequestAborted);
            return AuthenticateResult.Fail("Invalid session.");
        }
        var identity = new ClaimsIdentity(KtAuthenticationApi.Scheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId));
        identity.AddClaim(new Claim(ClaimTypes.Name, user.DisplayName));
        identity.AddClaim(new Claim("kt_application", session.Application));
        if (session.CompanyId is not null)
            identity.AddClaim(new Claim("kt_company", session.CompanyId));
        foreach (var permission in user.Permissions)
            identity.AddClaim(new Claim("kt_permission", permission));
        Context.Items[KtAuthenticationApi.SessionItem] = new KtValidatedSession(hash, session, user);
        return AuthenticateResult.Success(new AuthenticationTicket(
            new ClaimsPrincipal(identity), KtAuthenticationApi.Scheme));
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties) {
        Response.Headers.WWWAuthenticate = "Bearer";
        Response.Headers.CacheControl = "no-store";
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    }
}

/// <summary>
/// Store ONLY token hashes. Persistent implementations must enforce expiration,
/// immediate revocation and unique hashes across all API instances.
/// </summary>
public interface IKtSessionStore {
    /// <summary>
    /// Stores a session associated with a token hash. The session will expire at the specified time.
    /// </summary>
    /// <param name="tokenHash">The hash of the token associated with the session.</param>
    /// <param name="session">The session to store.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task PutAsync(string tokenHash, KtSession session, CancellationToken cancellationToken);


    /// <summary>
    /// Retrieves a session associated with a token hash.
    /// </summary>
    /// <param name="tokenHash">The hash of the token associated with the session.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the session if found; otherwise, null.</returns>
    Task<KtSession?> GetAsync(string tokenHash, CancellationToken cancellationToken);


    /// <summary>
    /// Removes a session associated with a token hash.
    /// </summary>
    /// <param name="tokenHash">The hash of the token associated with the session.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveAsync(string tokenHash, CancellationToken cancellationToken);
}




/// <summary>
/// Bounded single-process starter store. Eviction may require users to log in again.
/// Register a replacement singleton IKtSessionStore for shared/durable sessions.
/// </summary>
public sealed class KtMemorySessionStore : IKtSessionStore, IDisposable {

    /// <summary>
    /// A memory cache to store sessions with a size limit. Each session is stored with an absolute expiration time and a size of 1.
    /// </summary>
    private readonly MemoryCache _cache = new(new MemoryCacheOptions { SizeLimit = 10_000 });


    /// <summary>
    /// Stores a session associated with a token hash in the memory cache. The session will expire at the specified time.
    /// </summary>
    /// <param name="tokenHash">The hash of the token associated with the session.</param>
    /// <param name="session">The session to store.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task PutAsync(string tokenHash, KtSession session, CancellationToken cancellationToken) {
        cancellationToken.ThrowIfCancellationRequested();
        _cache.Set(tokenHash, session, new MemoryCacheEntryOptions {
            AbsoluteExpiration = session.ExpiresAt,
            Size = 1
        });
        return Task.CompletedTask;
    }


    /// <summary>
    /// Retrieves a session associated with a token hash from the memory cache.
    /// </summary>
    /// <param name="tokenHash">The hash of the token associated with the session.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the session if found; otherwise, null.</returns>
    public Task<KtSession?> GetAsync(string tokenHash, CancellationToken cancellationToken) {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_cache.Get<KtSession>(tokenHash));
    }


    /// <summary>
    /// Removes a session associated with a token hash from the memory cache.
    /// </summary>
    /// <param name="tokenHash">The hash of the token associated with the session.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task RemoveAsync(string tokenHash, CancellationToken cancellationToken) {
        cancellationToken.ThrowIfCancellationRequested();
        _cache.Remove(tokenHash);
        return Task.CompletedTask;
    }


    /// <summary>
    /// Disposes the memory cache.
    /// </summary>  
    public void Dispose() => _cache.Dispose();
}


[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(KtLoginRequest))]
public sealed record KtLoginRequest(
    string Login, string Password, string Application, string Client,
    string? CompanyId = null, string? Otp = null);


/// <summary>
/// Represents a verified identity with a user ID and security version.
/// </summary>
/// <param name="UserId">The ID of the user.</param>
/// <param name="SecurityVersion">The security version of the user.</param>
public sealed record KtVerifiedIdentity(string UserId, string SecurityVersion);


/// <summary>
///     Represents the access information for a user, including their ID, display name, email, security version, and permissions.
/// </summary>
/// <param name="UserId">The ID of the user.</param>
/// <param name="DisplayName">The display name of the user.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="SecurityVersion">The security version of the user.</param>
/// <param name="Permissions">The permissions assigned to the user.</param>
public sealed record KtAccess(
    string UserId, string DisplayName, string? Email,
    string SecurityVersion, string[] Permissions);



/// <summary>
/// Represents a session associated with a user, including the user ID, security version, application, company ID, client, and expiration time. 
/// </summary>
/// <param name="UserId">The ID of the user.</param>
/// <param name="SecurityVersion">The security version of the user.</param>
/// <param name="Application">The application associated with the session.</param>
/// <param name="CompanyId">The ID of the company associated with the session.</param>
/// <param name="Client">The client associated with the session.</param>
/// <param name="ExpiresAt">The expiration time of the session.</param>
public sealed record KtSession(
    string UserId, string SecurityVersion, string Application,
    string? CompanyId, string Client, DateTimeOffset ExpiresAt);


/// <summary>
/// Represents a validated session, including the token hash, session information, and user access information.
/// </summary>
/// <param name="TokenHash">The hash of the token.</param>
/// <param name="Session">The session information.</param>
/// <param name="User">The user access information.</param>
internal sealed record KtValidatedSession(string TokenHash, KtSession Session, KtAccess User);


/// <summary>
/// Represents the profile information of a user, including their user ID, display name, email, application, company ID, and permissions.
/// </summary>
/// <param name="UserId">The ID of the user.</param>
/// <param name="DisplayName">The display name of the user.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="Application">The application associated with the user.</param>
/// <param name="CompanyId">The ID of the company associated with the user.</param>
/// <param name="Permissions">The permissions assigned to the user.</param>
public sealed record KtUserProfile(
    string UserId, string DisplayName, string? Email,
    string Application, string? CompanyId, string[] Permissions);


public sealed record KtLoginResponse(
    string AccessToken, string TokenType, int ExpiresIn,
    DateTimeOffset ExpiresAt, KtUserProfile User);


[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(KtAuthError))]
public sealed record KtAuthError(string Error);



[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(KtLoginRequest))]
[JsonSerializable(typeof(KtLoginResponse))]
[JsonSerializable(typeof(KtUserProfile))]
[JsonSerializable(typeof(KtAuthError))]
internal partial class KtAuthJsonContext : JsonSerializerContext { }
