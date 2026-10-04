using MySqlConnector;
using Kt.Data.Queries.Workspaces;

namespace Kt.Data.Repositories;

/// <summary>Membership and role-grant reads. Resource, application and workflow authorisation remain API responsibilities.</summary>
public sealed class KtWorkspaceRepository {

    /// <summary>
    /// Lists the workspaces the current user is a member of, with pagination support.
    /// </summary>
    /// <param name="session">The database session.</param>
    /// <param name="afterId">The ID after which to start listing workspaces.</param>
    /// <param name="limit">The maximum number of workspaces to return.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of workspace summaries.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public async Task<IReadOnlyList<KtWorkspaceSummary>> ListCurrentAsync(KtDbSession session, ulong afterId = 0, int limit = 100, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(session);
        if (limit is < 1 or > 200) throw new ArgumentOutOfRangeException(nameof(limit));
        using var command = session.CreateCommand("""
            SELECT w.id, w.name, w.workspace_type
            FROM workspace_membership m
            JOIN workspace w ON w.id = m.workspace_id
            JOIN platform_user u ON u.id = m.platform_user_id
            JOIN security_principal s ON s.id = u.id
            WHERE m.platform_user_id = @actor AND m.status = 'ACTIVE'
              AND w.status = 'ACTIVE' AND u.status = 'ACTIVE'
              AND s.status = 'ACTIVE' AND s.principal_type = 'USER'
              AND w.id > @after
            ORDER BY w.id LIMIT @limit;
            """);
        command.Parameters.Add("@actor", MySqlDbType.UInt64).Value = session.PrincipalId;
        command.Parameters.Add("@after", MySqlDbType.UInt64).Value = afterId;
        command.Parameters.Add("@limit", MySqlDbType.Int32).Value = limit;
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var result = new List<KtWorkspaceSummary>();
        while (await reader.ReadAsync(cancellationToken))
            result.Add(new(reader.GetUInt64(0), reader.GetString(1), reader.GetString(2)));
        return result;
    }



    /// <summary>Checks one explicit grant; false for inactive/missing scope or expired roles. No administrator bypass.</summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <param name="session">Database session.</param>
    /// <param name="permissionCode">Permission code to check.</param>
    /// <param name="workspaceId">Workspace ID to check.</param>
    /// <returns>True if the user has the permission, false otherwise.</returns>
    public async Task<bool> HasPermissionAsync(KtDbSession session, ulong workspaceId,string permissionCode, CancellationToken cancellationToken = default) {

        ArgumentNullException.ThrowIfNull(session);
        if (workspaceId == 0) throw new ArgumentOutOfRangeException(nameof(workspaceId));

        ArgumentException.ThrowIfNullOrWhiteSpace(permissionCode);
        if (permissionCode.Length > 160) throw new ArgumentOutOfRangeException(nameof(permissionCode));

        using var command = session.CreateCommand("""
            SELECT EXISTS (
                SELECT 1 FROM workspace_membership m
                JOIN workspace w ON w.id = m.workspace_id
                JOIN platform_user u ON u.id = m.platform_user_id
                JOIN security_principal s ON s.id = u.id
                JOIN workspace_membership_role mr
                  ON mr.workspace_membership_id = m.id AND mr.workspace_id = m.workspace_id
                JOIN workspace_role r ON r.id = mr.workspace_role_id AND r.workspace_id = m.workspace_id
                JOIN workspace_role_permission rp ON rp.workspace_role_id = r.id
                JOIN workspace_permission p ON p.id = rp.workspace_permission_id
                WHERE m.platform_user_id = @actor AND m.workspace_id = @workspace
                  AND m.status = 'ACTIVE' AND w.status = 'ACTIVE' AND u.status = 'ACTIVE'
                  AND s.status = 'ACTIVE' AND s.principal_type = 'USER'
                  AND r.active = 1 AND p.active = 1
                  AND mr.assigned_utc <= UTC_TIMESTAMP(6)
                  AND (mr.expires_utc IS NULL OR mr.expires_utc > UTC_TIMESTAMP(6))
                  AND p.permission_code = @permission
            );
            """);

        command.Parameters.Add("@actor", MySqlDbType.UInt64).Value = session.PrincipalId;
        command.Parameters.Add("@workspace", MySqlDbType.UInt64).Value = workspaceId;
        command.Parameters.Add("@permission", MySqlDbType.VarChar, 160).Value = permissionCode;
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) == 1;
    }



    /// <summary>
    /// Returns the current user's granted permissions for one application
    /// within one explicit workspace.
    ///
    /// applicationPermissionCodes and entryPermissionCode must come from
    /// trusted server configuration, never from the client's request.
    ///
    /// Returns an empty array when application entry is not granted.
    /// Database errors and cancellation propagate to the caller.
    /// </summary>
    /// <param name="applicationPermissionCodes">The application's catalogue of permission codes.</param>
    /// <param name="entryPermissionCode">The application's entry permission code.</param>
    /// <param name="workspaceId">The ID of the workspace.</param>
    /// <param name="session">The database session.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An array of granted permission codes.</returns>
    /// <exception cref="ArgumentException">Thrown when entryPermissionCode is null, empty, or whitespace, or when applicationPermissionCodes contains invalid codes.</exception>
    public async Task<string[]> GetApplicationPermissionsAsync(KtDbSession session, ulong workspaceId, string entryPermissionCode, IReadOnlyCollection<string> applicationPermissionCodes, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(applicationPermissionCodes);
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(entryPermissionCode)) {
            throw new ArgumentException("An application entry permission is required.", nameof(entryPermissionCode));
        }

        // Copy the catalogue so it remains stable during this operation.
        // Permission codes are exact, case-sensitive identifiers.
        var allowedCodes = new HashSet<string>(StringComparer.Ordinal);

        foreach (var code in applicationPermissionCodes) {
            if (string.IsNullOrWhiteSpace(code) || code.Length > 160) {
                throw new ArgumentException("Permission codes must contain 1–160 characters.", nameof(applicationPermissionCodes));
            }
            allowedCodes.Add(code);
        }

        if (!allowedCodes.Contains(entryPermissionCode)) {
            throw new ArgumentException("The application catalogue must include its entry permission.", nameof(applicationPermissionCodes));
        }

        if (session.PrincipalId == 0 || workspaceId == 0)
            return Array.Empty<string>();

        const string sql = """
        SELECT DISTINCT permission.permission_code
        FROM platform_user AS usr
        INNER JOIN security_principal AS principal      ON principal.id = usr.id
        INNER JOIN workspace_membership AS membership   ON membership.platform_user_id = usr.id
        INNER JOIN workspace AS workspace               ON workspace.id = membership.workspace_id
        INNER JOIN workspace_membership_role AS assignment  ON assignment.workspace_membership_id = membership.id AND assignment.workspace_id = membership.workspace_id
        INNER JOIN workspace_role AS role               ON role.id = assignment.workspace_role_id AND role.workspace_id = assignment.workspace_id
        INNER JOIN workspace_role_permission AS grant_row ON grant_row.workspace_role_id = role.id
        INNER JOIN workspace_permission AS permission   ON permission.id = grant_row.workspace_permission_id
        WHERE usr.id = @principal_id    AND workspace.id = @workspace_id
          AND usr.status = 'ACTIVE' AND principal.status = 'ACTIVE' AND principal.principal_type = 'USER' AND principal.disabled_utc IS NULL
          AND workspace.status = 'ACTIVE' AND workspace.closed_utc IS NULL
          AND membership.status = 'ACTIVE'
          AND (membership.joined_utc IS NULL OR membership.joined_utc <= UTC_TIMESTAMP(6))
          AND (membership.ended_utc IS NULL OR membership.ended_utc > UTC_TIMESTAMP(6))
          AND role.active = 1 AND permission.active = 1
          AND assignment.assigned_utc <= UTC_TIMESTAMP(6)
          AND (assignment.expires_utc IS NULL OR assignment.expires_utc > UTC_TIMESTAMP(6))
          AND grant_row.granted_utc <= UTC_TIMESTAMP(6)
        ORDER BY permission.permission_code;
        """;

        await using var command = session.CreateCommand(sql);

        command.Parameters.Add("@principal_id", MySqlDbType.UInt64).Value = session.PrincipalId;
        command.Parameters.Add("@workspace_id", MySqlDbType.UInt64).Value = workspaceId;
        var grantedCodes = new HashSet<string>(StringComparer.Ordinal);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken)) {
            var code = reader.GetString(0);

            // Intersect actual database grants with the application's catalogue.
            if (allowedCodes.Contains(code))
                grantedCodes.Add(code);
        }

        if (!grantedCodes.Contains(entryPermissionCode))
            return Array.Empty<string>();

        return grantedCodes.OrderBy(code => code, StringComparer.Ordinal).ToArray();
    }
}
