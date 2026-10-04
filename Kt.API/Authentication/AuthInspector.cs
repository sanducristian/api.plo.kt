using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.IO;

namespace Kt.Api.Authentication;


/// <summary>
/// Inspects incoming authentication; NEVER validates or authorizes a user.
/// A decoded JWT can be expired, forged, or intended for another application.
/// </summary>
public static class AuthInspector {
    public static IResult Inspect(HttpContext context) {
        var request = context.Request;
        context.Response.Headers["Cache-Control"] = "no-store";
        context.Response.Headers["Pragma"] = "no-cache";
        context.Response.Headers["X-Content-Type-Options"] = "nosniff";

        string authorization = request.Headers["Authorization"].ToString();
        bool hasAuthorization = !string.IsNullOrWhiteSpace(authorization);
        bool parsed = AuthenticationHeaderValue.TryParse(authorization, out var auth);
        bool bearer = parsed && string.Equals(auth!.Scheme, "Bearer", StringComparison.OrdinalIgnoreCase);
        string? token = bearer ? auth!.Parameter : null;

        // Cookies are shown below, but are not treated as authenticated sessions.
        int status = !hasAuthorization ? StatusCodes.Status401Unauthorized : !parsed || (bearer && string.IsNullOrWhiteSpace(token)) ? StatusCodes.Status400BadRequest : StatusCodes.Status200OK;

        if (status == StatusCodes.Status401Unauthorized)
            context.Response.Headers["WWW-Authenticate"] = "Bearer realm=\"plo-debug\"";

        using var stream = new MemoryStream();
        using (var json = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true })) {
            json.WriteStartObject();
            json.WriteString("purpose", "Development diagnostics only; no authentication performed.");
            json.WriteBoolean("tokenValidated", false);
            json.WriteString("message", !hasAuthorization
                ? "Send Authorization: Bearer <access_token>. Cookies alone are not validated here."
                : !parsed ? "Malformed Authorization header."
                : bearer ? "JWT claims below are untrusted until cryptographically validated."
                : "Authorization scheme detected; its credential is not decoded or validated.");
            json.WriteString("method", request.Method);
            json.WriteString("path", request.Path.ToString());
            json.WriteString("authorizationScheme", parsed ? auth!.Scheme : null);

            json.WriteStartObject("headers");
            foreach (var header in request.Headers.OrderBy(h => h.Key)) {
                json.WriteStartArray(header.Key);
                // Avoid echoing reusable raw bearer/basic credentials and cookies twice.
                if (IsSensitiveHeader(header.Key))
                    json.WriteStringValue("[redacted]");
                else
                    foreach (string? value in header.Value)
                        json.WriteStringValue(value);
                json.WriteEndArray();
            }
            json.WriteEndObject();

            json.WriteStartObject("cookies");
            foreach (var cookie in request.Cookies.OrderBy(c => c.Key))
                json.WriteString(cookie.Key, cookie.Value);
            json.WriteEndObject();

            if (!string.IsNullOrWhiteSpace(token))
                WriteJwt(json, token);

            json.WriteEndObject();
        }

        return Results.Text(Encoding.UTF8.GetString(stream.ToArray()), "application/json; charset=utf-8", Encoding.UTF8, status);
    }

    private static bool IsSensitiveHeader(string name) =>
        name.Equals("Authorization", StringComparison.OrdinalIgnoreCase) ||
        name.Equals("Proxy-Authorization", StringComparison.OrdinalIgnoreCase) ||
        name.Equals("Cookie", StringComparison.OrdinalIgnoreCase) ||
        name.Equals("X-Api-Key", StringComparison.OrdinalIgnoreCase);


    private static void WriteJwt(Utf8JsonWriter writer, string token) {
        writer.WriteStartObject("jwt");
        writer.WriteBoolean("signatureVerified", false);
        try {
            if (token.Length > 32768)
                throw new FormatException("Token exceeds this inspector's 32 KiB limit.");

            string[] parts = token.Split('.');
            if (parts.Length != 3)
                throw new FormatException("Expected three JWT/JWS segments. Opaque tokens and encrypted JWE tokens are not decoded.");

            // Parse everything before writing so malformed JSON cannot corrupt output.
            using var header = JsonDocument.Parse(DecodeBase64Url(parts[0]));
            using var payload = JsonDocument.Parse(DecodeBase64Url(parts[1]));
            if (header.RootElement.ValueKind != JsonValueKind.Object ||
                payload.RootElement.ValueKind != JsonValueKind.Object)
                throw new FormatException("JWT header and payload must be JSON objects.");

            writer.WriteString("decodeStatus", "decoded-unverified");
            writer.WritePropertyName("header");
            header.RootElement.WriteTo(writer);
            writer.WritePropertyName("payload");
            payload.RootElement.WriteTo(writer);
        }
        catch (Exception ex) when (ex is FormatException || ex is JsonException) {
            writer.WriteString("decodeStatus", "not-decodable");
            writer.WriteString("error", ex is FormatException ? ex.Message : "JWT contains invalid JSON.");
        }
        writer.WriteEndObject();
    }

    private static byte[] DecodeBase64Url(string value) {
        if (value.Length == 0 || value.Any(c =>
            !((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') ||
              (c >= '0' && c <= '9') || c == '-' || c == '_')))
            throw new FormatException("Invalid base64url segment.");

        string base64 = value.Replace('-', '+').Replace('_', '/');
        base64 = (base64.Length % 4) switch {
            0 => base64,
            2 => base64 + "==",
            3 => base64 + "=",
            _ => throw new FormatException("Invalid base64url length.")
        };
        return Convert.FromBase64String(base64);
    }
}
