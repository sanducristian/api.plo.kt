using global::Kt.Data.Repositories;
using Kt.Data.Repositories;
using MySqlConnector;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kt.Data.Authorization.Objects;

public sealed class KtObjectAccessPolicy : IKtObjectAccessPolicy {
    private readonly KtWorkspaceRepository _workspaces;
    private readonly string _applicationEntry;
    private readonly string _locationRead;

    private readonly Dictionary<(ulong RoleId, string ValueTable), KtLocationPropertyRule> _rules;

    private static readonly HashSet<string> ScalarTables =
        new(StringComparer.Ordinal) {
            "obj_dyn_val_int",
            "obj_dyn_val_double",
            "obj_dyn_val_generic",
            "obj_dyn_val_string",
            "obj_dyn_val_attrib",
            "obj_dyn_val_blob",
            "obj_dyn_val_date",
            "obj_dyn_val_datetime",
            "obj_dyn_val_time"
        };


    /// <summary>
    /// Initializes a new instance of the <see cref="KtObjectAccessPolicy"/> class with the specified workspace repository, application entry, location read permission, and property rules.
    /// </summary>
    /// <param name="workspaces">The workspace repository.</param>
    /// <param name="applicationEntry">The application entry permission.</param>
    /// <param name="locationRead">The location read permission.</param>
    /// <param name="rules">The collection of location property rules.</param>
    /// <exception cref="ArgumentException">Thrown when a property rule has an invalid role or value table, or when there are duplicate property role/table rules.</exception>
    public KtObjectAccessPolicy(KtWorkspaceRepository workspaces, string applicationEntry, string locationRead, IEnumerable<KtLocationPropertyRule> rules) {
        ArgumentNullException.ThrowIfNull(workspaces);
        ArgumentNullException.ThrowIfNull(rules);

        _workspaces = workspaces;
        _applicationEntry = ValidatePermission(applicationEntry);
        _locationRead = ValidatePermission(locationRead);
        _rules = new();

        foreach (var rule in rules) {
            ArgumentNullException.ThrowIfNull(rule);

            if (rule.RoleId == 0 ||
                !ScalarTables.Contains(rule.ValueTable)) {
                throw new ArgumentException("A property rule requires a valid role and scalar table.");
            }

            ValidatePermission(rule.ReadPermission);
            ValidatePermission(rule.WritePermission);

            if (!_rules.TryAdd((rule.RoleId, rule.ValueTable), rule)) {
                throw new ArgumentException("Duplicate property role/table rule.");
            }
        }
    }


    /// <summary>
    /// Determines whether the specified session has read access to the object identified by the given table name and ID.
    /// </summary>
    /// <param name="session">The database session.</param>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="id">The ID of the object.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the session has read access; otherwise, false.</returns>
    public async Task<bool> CanReadAsync(
        KtDbSession session,
        string tableName,
        ulong id,
        CancellationToken cancellationToken) {

        ArgumentNullException.ThrowIfNull(session);

        if (id == 0) return false;

        if (tableName == "core_location") {
            var workspaceId = await FindLocationWorkspaceAsync(
                session, id, lockOwner: false, cancellationToken);

            return workspaceId is ulong workspace &&
                await HasGrantAsync(
                    session, workspace, _locationRead,
                    cancellationToken);
        }

        // Only explicitly supported scalar property tables are considered.
        if (!ScalarTables.Contains(tableName)) return false;

        ulong masterId;
        ulong roleId;

        using (var command = session.CreateCommand("""
            SELECT o.master_id, o.role_id
            FROM obj_id o
            JOIN obj_table t ON t.id = o.table_id
            WHERE o.id = @id
              AND o.invalid = 0
              AND t.uses_global_object_id = 1
              AND BINARY t.table_name = BINARY @table;
            """)) {

            command.Parameters.Add(
                "@id", MySqlDbType.UInt64).Value = id;

            command.Parameters.Add(
                "@table", MySqlDbType.VarChar).Value = tableName;

            await using var reader =
                await command.ExecuteReaderAsync(cancellationToken);

            if (!await reader.ReadAsync(cancellationToken)) return false;

            masterId = reader.GetUInt64(0);
            roleId = reader.GetUInt64(1);
        }

        // Unknown roles or unexpected physical value types are denied.
        if (!_rules.TryGetValue((roleId, tableName), out var rule)) {
            return false;
        }

        // Only direct location ownership is supported here.
        var ownerWorkspace = await FindLocationWorkspaceAsync(
            session, masterId, lockOwner: false, cancellationToken);

        if (ownerWorkspace is not ulong owner) return false;

        return await HasGrantAsync(
                   session, owner, _locationRead, cancellationToken)
            && await HasGrantAsync(
                   session, owner, rule.ReadPermission,
                   cancellationToken);
    }


    /// <summary>
    /// Determines whether the specified session has permission to create a property for the given master ID, role ID, and value table.
    /// </summary>
    /// <param name="session">The database session.</param>
    /// <param name="masterId">The ID of the master object.</param>
    /// <param name="roleId">The ID of the role.</param>
    /// <param name="valueTable">The name of the value table.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the session has permission to create the property; otherwise, false.</returns>
    /// <exception cref="InvalidOperationException">Thrown if there is no active transaction in the session.</exception>
    public async Task<bool> CanCreatePropertyAsync(
        KtDbSession session,
        ulong masterId,
        ulong roleId,
        string valueTable,
        CancellationToken cancellationToken) {

        ArgumentNullException.ThrowIfNull(session);

        // KtObjectRepository.CreateValueAsync already opens a transaction.
        if (!session.HasTransaction) {
            throw new InvalidOperationException(
                "Property authorization requires an active transaction.");
        }

        if (masterId == 0 ||
            !_rules.TryGetValue((roleId, valueTable), out var rule)) {
            return false;
        }

        // Hold the ownership row stable until the write transaction ends.
        var workspaceId = await FindLocationWorkspaceAsync(
            session, masterId, lockOwner: true, cancellationToken);

        if (workspaceId is not ulong workspace) return false;

        return await HasGrantAsync(
                   session, workspace, _locationRead, cancellationToken)
            && await HasGrantAsync(
                   session, workspace, rule.WritePermission,
                   cancellationToken);
    }


    /// <summary>
    /// Determines whether the specified session has the given permission in the specified workspace, including the application entry permission.
    /// </summary>
    /// <param name="session">The database session.</param>
    /// <param name="workspaceId">The ID of the workspace.</param>
    /// <param name="permission">The permission to check.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the session has the given permission; otherwise, false.</returns>
    private async Task<bool> HasGrantAsync(
        KtDbSession session,
        ulong workspaceId,
        string permission,
        CancellationToken cancellationToken) {

        return await _workspaces.HasPermissionAsync(session, workspaceId, _applicationEntry, cancellationToken)
            && await _workspaces.HasPermissionAsync(session, workspaceId, permission, cancellationToken);
    }


    /// <summary>
    /// Finds the workspace ID associated with the specified location ID, optionally locking the ownership row for the duration of a transaction.
    /// </summary>
    /// <param name="session">The database session.</param>
    /// <param name="locationId">The ID of the location.</param>
    /// <param name="lockOwner">Whether to lock the ownership row.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the workspace ID if found; otherwise, null.</returns>
    private static async Task<ulong?> FindLocationWorkspaceAsync(
        KtDbSession session,
        ulong locationId,
        bool lockOwner,
        CancellationToken cancellationToken) {

        if (locationId == 0) return null;

        const string sql = """
            SELECT l.workspace_id
            FROM core_location l
            JOIN obj_id o
              ON o.id = l.id AND o.invalid = 0
            JOIN obj_table t
              ON t.id = o.table_id
             AND t.uses_global_object_id = 1
             AND BINARY t.table_name = BINARY 'core_location'
            WHERE l.id = @id AND l.is_active = 1
            """;

        // This suffix is selected by server code, never request input.
        using var command = session.CreateCommand(
            sql + (lockOwner ? " FOR SHARE;" : ";"));

        command.Parameters.Add(
            "@id", MySqlDbType.UInt64).Value = locationId;

        var result = await command.ExecuteScalarAsync(cancellationToken);

        return result is null or DBNull
            ? null
            : Convert.ToUInt64(result, CultureInfo.InvariantCulture);
    }


    /// <summary>
    /// Validates that the provided permission string is a non-empty ASCII string with a maximum length of 160 characters. Throws an ArgumentException if the validation fails.
    /// </summary>
    /// <param name="value">The permission string to validate.</param>
    /// <returns>The validated permission string.</returns>
    /// <exception cref="ArgumentException">Thrown if the permission string is invalid.</exception>
    private static string ValidatePermission(string value) {
        if (string.IsNullOrWhiteSpace(value) ||
            value.Length > 160 ||
            value.Any(c => c > 127)) {

            throw new ArgumentException("A valid ASCII permission code is required.");
        }

        return value;
    }
}