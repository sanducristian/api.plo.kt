using MySqlConnector;
using Kt.Data.Queries.Workspaces;

namespace Kt.Data.Repositories;

/// <summary>Membership and role-grant reads. Resource, application and workflow authorisation remain API responsibilities.</summary>
public sealed class KtWorkspaceRepository {
    public async Task<IReadOnlyList<KtWorkspaceSummary>> ListCurrentAsync(KtDbSession session,
        ulong afterId = 0, int limit = 100, CancellationToken cancellationToken = default) {
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
}
