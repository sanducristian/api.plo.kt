using Kt.Data.Authorization.Objects;
using Kt.Data.Objects;
using Kt.Data.Queries.Objects;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Globalization;
using Kt.Data.Commands.Objects;

namespace Kt.Data.Repositories;

/// <summary>Global object resolution, explicit static projections and dynamic metadata. No guessed tenant scope.</summary>
public sealed class KtObjectRepository {
    private readonly KtDb _db;
    private readonly IKtObjectAccessPolicy _access;
    private readonly IReadOnlyDictionary<string, KtObjectTable> _tables;


    /// <summary>
    /// Initializes a new instance of the <see cref="KtObjectRepository"/> class with the specified database, access policy, and object tables.
    /// </summary>
    /// <param name="db">The database instance.</param>
    /// <param name="access">The object access policy.</param>
    /// <param name="tables">The collection of object tables.</param>
    public KtObjectRepository(KtDb db, IKtObjectAccessPolicy access, IEnumerable<KtObjectTable> tables) {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _access = access ?? throw new ArgumentNullException(nameof(access));
        _tables = tables.ToDictionary(t => t.Name, StringComparer.Ordinal);
    }


    /// <summary>
    /// Gets a global object record by its global ID. This method checks the access policy for read permissions and returns null if the object is invalid or not found.
    /// </summary>
    /// <param name="session">The database session.</param>
    /// <param name="globalId">The global ID of the object.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The global object record, or null if not found or invalid.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown if the access policy denies read access.</exception>
    public async Task<KtObjectRecord?> GetAsync(KtDbSession session, ulong globalId,
        CancellationToken cancellationToken = default) {
        var identity = await ReadIdentityAsync(session, globalId, cancellationToken);
        if (identity is null || identity.Invalid) return null;
        if (!await _access.CanReadAsync(session, identity.TableName, globalId, cancellationToken))
            throw new UnauthorizedAccessException("Object access denied.");
        return await ReadPayloadAsync(session, identity.TableName, globalId, identity, cancellationToken);
    }



    /// <summary>Supports ordinary static records even when their ID is not a global object ID.</summary>
    /// <param name="session">The database session.</param>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="id">The ID of the record.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The static object record, or null if not found.</returns>
    public async Task<KtObjectRecord?> GetStaticAsync(KtDbSession session, string tableName, ulong id, CancellationToken cancellationToken = default) {
        var table = GetTable(tableName);
        if (tableName.StartsWith("obj_dyn", StringComparison.Ordinal))
            throw new ArgumentException("Use GetAsync for dynamic objects.", nameof(tableName));
        if (!await _access.CanReadAsync(session, tableName, id, cancellationToken))
            throw new UnauthorizedAccessException("Object access denied.");

        using var registry = session.CreateCommand("""SELECT uses_global_object_id FROM obj_table WHERE BINARY table_name = BINARY @table;""");
        registry.Parameters.Add("@table", MySqlDbType.VarChar).Value = table.Name;
        var flag = await registry.ExecuteScalarAsync(cancellationToken);
        if (flag is null or DBNull) throw new InvalidOperationException("Static table is not registered.");

        // If the table uses global object IDs, read the identity to verify the record.
        KtObjectIdentity? identity = null;
        if (Convert.ToBoolean(flag, CultureInfo.InvariantCulture)) {
            identity = await ReadIdentityAsync(session, id, cancellationToken);
            if (identity is null || identity.Invalid || identity.TableName != tableName) return null;
        }
        return await ReadPayloadAsync(session, tableName, id, identity, cancellationToken);
    }




    /// <summary>Direct dynamic children, paginated by scanned ID; every child is independently authorised.</summary>
    /// <param name="session">The database session.</param>
    /// <param name="masterId">The ID of the master object.</param>
    /// <param name="afterId">The ID after which to start pagination.</param>
    /// <param name="limit">The maximum number of results to return.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A page of dynamic object records.</returns>
    public async Task<KtObjectPage> ListDynamicAsync(KtDbSession session, ulong masterId,
        ulong afterId = 0, int limit = 100, CancellationToken cancellationToken = default) {
        if (limit is < 1 or > 200) throw new ArgumentOutOfRangeException(nameof(limit));
        var master = await GetAsync(session, masterId, cancellationToken);
        if (master is null) return new(Array.Empty<KtObjectRecord>(), afterId);
        var ids = new List<ulong>();
        using (var command = session.CreateCommand("""
            SELECT o.id FROM obj_id o JOIN obj_table t ON t.id = o.table_id
            WHERE o.master_id = @master AND o.invalid = 0 AND o.id > @after
              AND (t.table_name = 'obj_dyn' OR LEFT(t.table_name, 8) = 'obj_dyn_')
            ORDER BY o.id LIMIT @limit;
            """)) {
            command.Parameters.Add("@master", MySqlDbType.UInt64).Value = masterId;
            command.Parameters.Add("@after", MySqlDbType.UInt64).Value = afterId;
            command.Parameters.Add("@limit", MySqlDbType.Int32).Value = limit;
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken)) ids.Add(reader.GetUInt64(0));
        }
        var results = new List<KtObjectRecord>();
        foreach (var id in ids) {
            var identity = await ReadIdentityAsync(session, id, cancellationToken);
            if (identity is null || identity.Invalid || !_tables.ContainsKey(identity.TableName)) continue;
            if (!await _access.CanReadAsync(session, identity.TableName, id, cancellationToken)) continue;
            var record = await ReadPayloadAsync(session, identity.TableName, id, identity, cancellationToken);
            if (record is not null) results.Add(record);
        }
        return new(results.AsReadOnly(), ids.Count == 0 ? afterId : ids[^1]);
    }



    /// <summary>Outgoing typed references use obj_dyn.masterId, not assumed obj_id.master_id semantics.</summary>
    /// <param name="session">The database session.</param>
    /// <param name="masterId">The ID of the master object.</param>
    /// <param name="afterId">The ID after which to start pagination.</param>
    /// <param name="limit">The maximum number of results to return.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A page of dynamic object records.</returns>
    public async Task<KtObjectPage> ListReferencesAsync(KtDbSession session, ulong masterId, ulong afterId = 0, int limit = 100, CancellationToken cancellationToken = default) {
        if (limit is < 1 or > 200) throw new ArgumentOutOfRangeException(nameof(limit));
        if (await GetAsync(session, masterId, cancellationToken) is null)
            return new(Array.Empty<KtObjectRecord>(), afterId);
        var ids = new List<ulong>();
        using (var command = session.CreateCommand("""
            SELECT d.id FROM obj_dyn d
            JOIN obj_id o ON o.id = d.id AND o.invalid = 0
            JOIN obj_table t ON t.id = o.table_id AND BINARY t.table_name = BINARY 'obj_dyn'
            WHERE d.masterId = @master AND d.id > @after ORDER BY d.id LIMIT @limit;
            """)) {
            command.Parameters.Add("@master", MySqlDbType.UInt64).Value = masterId;
            command.Parameters.Add("@after", MySqlDbType.UInt64).Value = afterId;
            command.Parameters.Add("@limit", MySqlDbType.Int32).Value = limit;
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken)) ids.Add(reader.GetUInt64(0));
        }
        var result = new List<KtObjectRecord>();
        foreach (var id in ids) {
            if (!await _access.CanReadAsync(session, "obj_dyn", id, cancellationToken)) continue;
            var item = await GetAsync(session, id, cancellationToken);
            if (item is not null) result.Add(item);
        }
        return new(result.AsReadOnly(), ids.Count == 0 ? afterId : ids[^1]);
    }

    /// <summary>Atomically adds a scalar property. Owns its transaction; never updates existing properties implicitly.</summary>
    public Task<ulong> CreateValueAsync(ulong authenticatedPrincipalId, ulong masterId, ulong roleId,
        KtObjectDynamicValue value, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(value);
        var mapped = Map(value);
        GetTable(mapped.Table);
        if (masterId == 0 || roleId == 0) throw new ArgumentOutOfRangeException(nameof(masterId));
        return _db.InTransactionAsync(authenticatedPrincipalId, async (session, ct) => {
            // Serialise property creation for this master. All writers must follow this protocol.
            using (var master = session.CreateCommand("SELECT id FROM obj_id WHERE id = @id AND invalid = 0 FOR UPDATE;")) {
                master.Parameters.Add("@id", MySqlDbType.UInt64).Value = masterId;
                if (await master.ExecuteScalarAsync(ct) is null) throw new InvalidOperationException("Active master not found.");
            }
            if (!await _access.CanCreatePropertyAsync(session, masterId, roleId, mapped.Table, ct))
                throw new UnauthorizedAccessException("Property creation denied.");
            using (var role = session.CreateCommand("SELECT singleton FROM obj_type WHERE id = @id FOR SHARE;")) {
                role.Parameters.Add("@id", MySqlDbType.UInt64).Value = roleId;
                var singleton = await role.ExecuteScalarAsync(ct);
                if (singleton is null) throw new InvalidOperationException("Semantic role does not exist.");
                if (Convert.ToBoolean(singleton, CultureInfo.InvariantCulture)) {
                    using var duplicate = session.CreateCommand("SELECT id FROM obj_id WHERE master_id = @master AND role_id = @role AND invalid = 0 LIMIT 1 FOR UPDATE;");
                    duplicate.Parameters.Add("@master", MySqlDbType.UInt64).Value = masterId;
                    duplicate.Parameters.Add("@role", MySqlDbType.UInt64).Value = roleId;
                    if (await duplicate.ExecuteScalarAsync(ct) is not null)
                        throw new InvalidOperationException("Singleton role already has an active value.");
                }
            }
            if (value is KtObjectDynamicLocalizedTextValue localized) {
                using var language = session.CreateCommand("SELECT id FROM sys_language WHERE id = @id;");
                language.Parameters.Add("@id", MySqlDbType.UInt64).Value = localized.LanguageId;
                if (await language.ExecuteScalarAsync(ct) is null) throw new ArgumentException("Unknown language.");
            }
            // Resolve exact physical name; do not use the broken AllocateIdMaster routine.
            ulong tableId;
            using (var table = session.CreateCommand("SELECT id FROM obj_table WHERE BINARY table_name = BINARY @name AND uses_global_object_id = 1 FOR SHARE;")) {
                table.Parameters.Add("@name", MySqlDbType.VarChar).Value = mapped.Table;
                var result = await table.ExecuteScalarAsync(ct);
                if (result is null) throw new InvalidOperationException("Table must be registered as using global IDs.");
                tableId = Convert.ToUInt64(result, CultureInfo.InvariantCulture);
            }
            ulong id;
            using (var allocation = session.CreateCommand("""
                INSERT INTO obj_id (table_id, invalid, original_id, master_id, role_id, user_id)
                VALUES (@table, 0, 0, @master, @role, @actor);
                """)) {
                allocation.Parameters.Add("@table", MySqlDbType.UInt64).Value = tableId;
                allocation.Parameters.Add("@master", MySqlDbType.UInt64).Value = masterId;
                allocation.Parameters.Add("@role", MySqlDbType.UInt64).Value = roleId;
                allocation.Parameters.Add("@actor", MySqlDbType.UInt64).Value = session.PrincipalId;
                await allocation.ExecuteNonQueryAsync(ct);
                // Read the session's unsigned LAST_INSERT_ID rather than narrowing via Int64.
                using var last = session.CreateCommand("SELECT LAST_INSERT_ID();");
                id = Convert.ToUInt64(await last.ExecuteScalarAsync(ct), CultureInfo.InvariantCulture);
            }
            var columns = string.Join(", ", mapped.Fields.Select(f => $"`{f.Name}`"));
            var parameters = string.Join(", ", mapped.Fields.Select((_, index) => $"@v{index}"));
            using var insert = session.CreateCommand($"INSERT INTO `{mapped.Table}` (`id`, {columns}) VALUES (@id, {parameters});");
            insert.Parameters.Add("@id", MySqlDbType.UInt64).Value = id;
            for (var i = 0; i < mapped.Fields.Length; i++)
                insert.Parameters.Add($"@v{i}", mapped.Fields[i].Type).Value = mapped.Fields[i].Value ?? DBNull.Value;
            await insert.ExecuteNonQueryAsync(ct);
            return id;
        }, cancellationToken);
    }

    private KtObjectTable GetTable(string name) => _tables.TryGetValue(name, out var table)
        ? table : throw new NotSupportedException("No reviewed projection is registered for this table.");

    private static async Task<KtObjectIdentity?> ReadIdentityAsync(KtDbSession session, ulong id, CancellationToken ct) {
        using var command = session.CreateCommand("""
            SELECT o.id, o.table_id, t.table_name, o.invalid, o.original_id, o.master_id,
                   o.role_id, o.user_id, o.last_update
            FROM obj_id o JOIN obj_table t ON t.id = o.table_id WHERE o.id = @id;
            """);
        command.Parameters.Add("@id", MySqlDbType.UInt64).Value = id;
        await using var r = await command.ExecuteReaderAsync(ct);
        if (!await r.ReadAsync(ct)) return null;
        return new(r.GetUInt64(0), r.GetUInt64(1), r.GetString(2), r.GetBoolean(3),
            r.GetUInt64(4), r.GetUInt64(5), r.GetUInt64(6), r.IsDBNull(7) ? null : r.GetUInt64(7),
            DateTime.SpecifyKind(r.GetDateTime(8), DateTimeKind.Utc));
    }

    private async Task<KtObjectRecord?> ReadPayloadAsync(KtDbSession session, string name, ulong id,
        KtObjectIdentity? identity, CancellationToken ct) {
        var table = GetTable(name);
        using var command = session.CreateCommand($"SELECT {string.Join(", ", table.Columns.Select(c => $"`{c}`"))} FROM `{table.Name}` WHERE `id` = @id;");
        command.Parameters.Add("@id", MySqlDbType.UInt64).Value = id;
        await using var reader = await command.ExecuteReaderAsync(ct);
        if (!await reader.ReadAsync(ct)) return null;
        var fields = new Dictionary<string, object?>(StringComparer.Ordinal);
        for (var i = 0; i < reader.FieldCount; i++)
            fields.Add(reader.GetName(i), reader.IsDBNull(i) ? null : reader.GetValue(i));
        return new(name, id, identity, new ReadOnlyDictionary<string, object?>(fields));
    }

    private sealed record Field(string Name, MySqlDbType Type, object? Value);
    private sealed record Mapped(string Table, params Field[] Fields);
    private static Mapped Map(KtObjectDynamicValue value) {
        static string? Text(string? s, int length) {
            if (s is not null && (s.Length > length || s.Any(char.IsSurrogate)))
                throw new ArgumentException("Text exceeds the utf8mb3 column length or contains unsupported characters.");
            return s;
        }
        return value switch {
            KtObjectDynamicIntegerValue v => new("obj_dyn_val_int", new Field("value", MySqlDbType.Int64, v.Value)),
            KtObjectDynamicDoubleValue v when v.Value is null || double.IsFinite(v.Value.Value) => new("obj_dyn_val_double", new Field("value", MySqlDbType.Double, v.Value)),
            KtObjectDynamicTextValue v => new("obj_dyn_val_generic", new Field("value", MySqlDbType.VarChar, Text(v.Value, 128))),
            KtObjectDynamicLocalizedTextValue v => new("obj_dyn_val_string", new Field("languageId", MySqlDbType.UInt64, v.LanguageId), new Field("data", MySqlDbType.VarChar, Text(v.Value, 128))),
            KtObjectDynamicAttributeValue v when v.Name is not null && v.Value is not null => new("obj_dyn_val_attrib", new Field("attrName", MySqlDbType.VarChar, Text(v.Name, 64)), new Field("attrValue", MySqlDbType.VarChar, Text(v.Value, 128))),
            KtObjectDynamicBlobValue v when v.Value is null || v.Value.Length <= 65535 => new("obj_dyn_val_blob", new Field("data", MySqlDbType.Blob, v.Value?.ToArray())),
            KtObjectDynamicDateValue v when v.Value is null || v.Value.Value.Year >= 1000 => new("obj_dyn_val_date", new Field("value", MySqlDbType.Date, v.Value)),
            KtObjectDynamicDateTimeValue v when v.Value.Year >= 1000 && v.Value.Kind == DateTimeKind.Unspecified && v.Value.Ticks % TimeSpan.TicksPerSecond == 0 => new("obj_dyn_val_datetime", new Field("value", MySqlDbType.DateTime, v.Value)),
            KtObjectDynamicTimeValue v when v.Value >= TimeSpan.Zero && v.Value < TimeSpan.FromDays(1) && v.Value.Ticks % TimeSpan.TicksPerSecond == 0 => new("obj_dyn_val_time", new Field("value", MySqlDbType.Time, v.Value)),
            _ => throw new ArgumentException("Unsupported value, invalid range or precision. Legacy datetime has unspecified timezone; time is whole-second time of day.")
        };
    }
}
