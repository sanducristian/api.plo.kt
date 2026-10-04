using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MySqlConnector;

namespace Kt.Data.Caching;

public sealed record KtTableInfo(
    ulong Id,
    string TableName,
    string? IndexField,
    string? IndexName,
    ulong? TypeId,
    bool? RefNil,
    bool UsesGlobalObjectId);


public sealed record KtTypeInfo(
    ulong Id,
    ulong ParentId,
    string Guid,
    string Name,
    bool Singleton);


public sealed record KtCurrencyInfo(
    ulong Id,
    string Name,
    string ShortName,
    string IsoCode,
    string? NumericCode,
    string Symbol,
    byte? MinorUnit,
    bool IsActive,
    ulong RowVersion);


public sealed record KtLanguageInfo(
    ulong Id,
    string Name,
    string? NativeName,
    string? IsoCode2,
    string? IsoCode3,
    string? LocaleCode,
    string? LegacyCode,
    string? Description);


public sealed record KtCountryInfo(
    ulong Id,
    string IsoAlpha2,
    string IsoAlpha3,
    string IsoNumeric,
    string Name,
    bool IsActive);



/// <summary>
/// Immutable reference-data snapshot.
/// Capture Current once when an operation needs several catalogues.
/// </summary>
public sealed record KtDbSnapshot(
    DateTimeOffset LoadedAtUtc,
    FrozenDictionary<ulong, KtTableInfo> Tables,
    FrozenDictionary<ulong, KtTypeInfo> Types,
    FrozenDictionary<ulong, KtCurrencyInfo> Currencies,
    FrozenDictionary<ulong, KtLanguageInfo> Languages,
    FrozenDictionary<ulong, KtCountryInfo> Countries);


/// <summary>
/// Preloads shared database catalogues into process memory.
/// Register one instance per database.
/// </summary>
public sealed class KtDbPreload {
    private readonly string _connectionString;
    private readonly SemaphoreSlim _loadLock = new(1, 1);

    private KtDbSnapshot? _current;

    public KtDbPreload(string connectionString) {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("A database connection string is required.", nameof(connectionString));

        _connectionString = connectionString;
    }


    public bool IsLoaded => Volatile.Read(ref _current) is not null;

    public KtDbSnapshot Current => Volatile.Read(ref _current)
        ?? throw new InvalidOperationException("Database catalogues are not loaded. Call LoadAsync first.");



    /// <summary>
    /// Loads the catalogues if no snapshot has been published.
    /// </summary>
    public Task LoadAsync(CancellationToken cancellationToken = default) => LoadCoreAsync(forceReload: false, cancellationToken);



    /// <summary>
    /// Builds and publishes a replacement snapshot.
    /// Readers can continue using the previous snapshot during loading.
    /// </summary>
    public Task ReloadAsync(CancellationToken cancellationToken = default) =>
        LoadCoreAsync(forceReload: true, cancellationToken);



    /// <summary>
    /// Loads the catalogues if no snapshot has been published, or reloads and publishes a replacement snapshot.
    /// </summary>
    /// <param name="forceReload">If true, forces a reload of the catalogues even if they are already loaded.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous load operation.</returns>
    private async Task LoadCoreAsync(bool forceReload, CancellationToken cancellationToken) {
        if (!forceReload && IsLoaded)
            return;

        await _loadLock.WaitAsync(cancellationToken);

        try {
            // Another caller may have completed the first load.
            if (!forceReload && IsLoaded)
                return;

            await using var connection =
                new MySqlConnection(_connectionString);

            await connection.OpenAsync(cancellationToken);

            // All SELECTs see the same InnoDB consistent-read snapshot.
            await using var transaction =
                await connection.BeginTransactionAsync(
                    IsolationLevel.RepeatableRead,
                    cancellationToken);

            var tables = await ReadAsync(
                connection, transaction,
                """
                SELECT id, table_name, table_index_field,
                       table_index_name, type_id, ref_nil,
                       uses_global_object_id
                FROM obj_table
                """,
                r => new KtTableInfo(
                    r.GetUInt64(0),
                    r.GetString(1),
                    NullableString(r, 2),
                    NullableString(r, 3),
                    r.IsDBNull(4) ? null : r.GetUInt64(4),
                    r.IsDBNull(5) ? null : r.GetBoolean(5),
                    r.GetBoolean(6)),
                x => x.Id,
                cancellationToken);

            var types = await ReadAsync(
                connection, transaction,
                """
                SELECT id, parent_id, guid, name, singleton
                FROM obj_type
                """,
                r => new KtTypeInfo(
                    r.GetUInt64(0),
                    r.GetUInt64(1),
                    r.GetString(2),
                    r.GetString(3),
                    r.GetBoolean(4)),
                x => x.Id,
                cancellationToken);

            var currencies = await ReadAsync(
                connection, transaction,
                """
                SELECT id, name, short_name, iso_code,
                       numeric_code, symbol, minor_unit,
                       is_active, row_version
                FROM sys_currency
                """,
                r => new KtCurrencyInfo(
                    r.GetUInt64(0),
                    r.GetString(1),
                    r.GetString(2),
                    r.GetString(3),
                    NullableString(r, 4),
                    r.GetString(5),
                    r.IsDBNull(6) ? null : r.GetByte(6),
                    r.GetBoolean(7),
                    r.GetUInt64(8)),
                x => x.Id,
                cancellationToken);

            var languages = await ReadAsync(
                connection, transaction,
                """
                SELECT id, language_name, native_name,
                       iso_code_2, iso_code_3, locale_code,
                       legacy_code, description
                FROM sys_language
                """,
                r => new KtLanguageInfo(
                    r.GetUInt64(0),
                    r.GetString(1),
                    NullableString(r, 2),
                    NullableString(r, 3),
                    NullableString(r, 4),
                    NullableString(r, 5),
                    NullableString(r, 6),
                    NullableString(r, 7)),
                x => x.Id,
                cancellationToken);

            var countries = await ReadAsync(
                connection, transaction,
                """
                SELECT id, iso_alpha2, iso_alpha3,
                       iso_numeric, default_name, is_active
                FROM sys_country
                """,
                r => new KtCountryInfo(
                    r.GetUInt64(0),
                    r.GetString(1),
                    r.GetString(2),
                    r.GetString(3),
                    r.GetString(4),
                    r.GetBoolean(5)),
                x => x.Id,
                cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            var snapshot = new KtDbSnapshot(
                DateTimeOffset.UtcNow,
                tables,
                types,
                currencies,
                languages,
                countries);

            // Publish only after every catalogue loaded successfully.
            Interlocked.Exchange(ref _current, snapshot);
        }
        finally {
            _loadLock.Release();
        }
    }


    /// <summary>
    /// Reads a set of records from the database and maps them to a frozen dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the records.</typeparam>
    /// <param name="connection">The MySQL connection.</param>
    /// <param name="transaction">The MySQL transaction.</param>
    /// <param name="sql">The SQL query to execute.</param>
    /// <param name="map">A function to map a data reader row to a record.</param>
    /// <param name="getId">A function to get the ID of a record.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous read operation. The task result contains a frozen dictionary of the records.</returns>
    private static async Task<FrozenDictionary<ulong, T>> ReadAsync<T>(
        MySqlConnection connection, MySqlTransaction transaction, string sql,
        Func<MySqlDataReader, T> map, Func<T, ulong> getId, CancellationToken cancellationToken) {

        using var command = new MySqlCommand(sql, connection, transaction) { 
            CommandTimeout = 30
            };

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var records = new Dictionary<ulong, T>();

        while (await reader.ReadAsync(cancellationToken)) {
            var record = map(reader);
            records.Add(getId(record), record);
        }

        return records.ToFrozenDictionary();
    }

    private static string? NullableString(MySqlDataReader reader, int ordinal) =>
        reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
}