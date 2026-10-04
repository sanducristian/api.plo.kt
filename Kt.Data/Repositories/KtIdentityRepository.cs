using Kt.Data.Queries.Identity;
using MySqlConnector;

namespace Kt.Data.Repositories;

/// <summary>Authenticated profile retrieval. Does not verify passwords or implement login.</summary>
public sealed class KtIdentityRepository {
    /// <summary>
    /// Gets the current user's profile asynchronously.
    /// </summary>
    /// <param name="session">The database session.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The current user's profile, or null if not found.</returns>
    public async Task<KtUserProfile?> GetCurrentAsync2(KtDbSession session, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(session);
        using var command = session.CreateCommand("""
            SELECT p.id, s.principal_code, p.display_name, p.preferred_locale, p.time_zone
            FROM platform_user p
            JOIN security_principal s ON s.id = p.id
            WHERE p.id = @actor AND p.status = 'ACTIVE'
              AND s.status = 'ACTIVE' AND s.principal_type = 'USER';
            """);
        command.Parameters.Add("@actor", MySqlDbType.UInt64).Value = session.PrincipalId;
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken)) return null;
        return new(reader.GetUInt64(0), reader.GetString(1), reader.GetString(2),
            reader.IsDBNull(3) ? null : reader.GetString(3),
            reader.IsDBNull(4) ? null : reader.GetString(4));
    }


    /// <summary>
    /// Returns the active human-user profile for the principal
    /// already authenticated and bound to this database session.
    /// Returns null when no eligible account exists.
    /// </summary>
    public async Task<KtUserProfile?> GetCurrentAsync(KtDbSession session, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(session);
        cancellationToken.ThrowIfCancellationRequested();

        if (session.PrincipalId == 0)
            return null;

        const string sql = """
            SELECT u.id, p.principal_code, u.display_name, u.preferred_locale, u.time_zone
            FROM platform_user AS u
            INNER JOIN security_principal AS p ON p.id = u.id
            WHERE u.id = @principal_id AND u.status = 'ACTIVE' AND p.status = 'ACTIVE' AND p.principal_type = 'USER' AND p.disabled_utc IS NULL
            LIMIT 1;
            """;

        // Uses the existing connection and its current transaction.
        await using var command = session.CreateCommand(sql);

        command.Parameters.Add("@principal_id", MySqlDbType.UInt64) .Value = session.PrincipalId;
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
            return null;

        return new KtUserProfile(
            Id: reader.GetUInt64(0), 
            PrincipalCode: reader.GetString(1),
            DisplayName: reader.GetString(2), 
            PreferredLocale: reader.IsDBNull(3) ? null : reader.GetString(3),
            TimeZone: reader.IsDBNull(4) ? null : reader.GetString(4)
        );
    }
}
