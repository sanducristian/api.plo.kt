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
using System.Threading;
using System.Threading.Tasks;
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


/// <summary>
/// Database integration boundary. Use existing global object IDs as strings
/// without allocating new identities or assuming SQL types/table names.
/// Implement ALL security requirements below before connecting real users.
/// </summary>
public interface IKtIdentityBackend {


    /// <summary>
    /// Verify the stored password hash using its actual algorithm; never plaintext.
    /// Enforce active/verified-account rules, persistent per-account throttling and
    /// lockout, required MFA (including OTP replay prevention), and audit outcomes.
    /// Use generic failures and a dummy hash check for unknown users to reduce
    /// account enumeration. Return null for ANY failure, including missing MFA.
    /// Never return a successful identity until every required factor passes.
    /// SecurityVersion is a server-managed opaque revision changed on password
    /// reset, account recovery or explicit "revoke all sessions".
    /// </summary>
    Task<KtVerifiedIdentity?> VerifyCredentialsAsync(string login, string password, string? otp, string? remoteIp, CancellationToken cancellationToken);



    /// <summary>
    /// Called at login and on EVERY authenticated request. Return null for deleted,
    /// disabled or locked accounts, revoked application access, or invalid company
    /// membership. Validate application and company together; neither is trusted.
    /// A null company means personal scope only, NEVER all companies.
    /// Return current permissions for this exact scope and current SecurityVersion.
    /// Do not accept permissions/roles supplied by the client.
    /// </summary>
    Task<KtAccess?> GetCurrentAccessAsync(string userId, string application, string? companyId, CancellationToken cancellationToken);
}
