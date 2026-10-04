using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MySqlConnector;

namespace Kt.Data.Caching;


/// <summary>
/// Immutable record representing information about a database table.
/// </summary>
/// <param name="Id">The unique identifier of the table.</param>
/// <param name="TableName">The name of the table.</param>
/// <param name="IndexField">The name of the index field.</param>
/// <param name="IndexName">The name of the index.</param>
/// <param name="TypeId">The type identifier.</param>
/// <param name="RefNil">Indicates whether the reference is nil.</param>
/// <param name="UsesGlobalObjectId">Indicates whether the table uses a global object ID.</param>
public sealed record KtTableInfo(
    ulong Id,
    string TableName,
    string? IndexField,
    string? IndexName,
    ulong? TypeId,
    bool? RefNil,
    bool UsesGlobalObjectId);




/// <summary>
/// Immutable record representing information about a type in the database.
/// </summary>
/// <param name="Id">The unique identifier of the type.</param>
/// <param name="ParentId">The unique identifier of the parent type.</param>
/// <param name="Guid">The globally unique identifier of the type.</param>
/// <param name="Name">The name of the type.</param>
/// <param name="Singleton">Indicates whether the type is a singleton.</param>
public sealed record KtTypeInfo(
    ulong Id,
    ulong ParentId,
    string Guid,
    string Name,
    bool Singleton);



/// <summary>
/// Immutable record representing information about a currency in the database.
/// </summary>
/// <param name="Id">The unique identifier of the currency.</param>
/// <param name="Name">The name of the currency.</param>
/// <param name="ShortName">The short name of the currency.</param>
/// <param name="IsoCode">The ISO code of the currency.</param>
/// <param name="NumericCode">The numeric code of the currency.</param>
/// <param name="Symbol">The symbol of the currency.</param>
/// <param name="MinorUnit">The minor unit of the currency.</param>
/// <param name="IsActive">Indicates whether the currency is active.</param>
/// <param name="RowVersion">The row version of the currency.</param>
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



/// <summary>
/// Immutable record representing information about a language in the database.
/// </summary>
/// <param name="Id">The unique identifier of the language.</param>
/// <param name="Name">The name of the language.</param>
/// <param name="NativeName">The native name of the language.</param>
/// <param name="IsoCode2">The ISO 639-1 two-letter code of the language.</param>
/// <param name="IsoCode3">The ISO 639-2 three-letter code of the language.</param>
/// <param name="LocaleCode">The locale code of the language.</param>
/// <param name="LegacyCode">The legacy code of the language.</param>
/// <param name="Description">The description of the language.</param>
public sealed record KtLanguageInfo(
    ulong Id,
    string Name,
    string? NativeName,
    string? IsoCode2,
    string? IsoCode3,
    string? LocaleCode,
    string? LegacyCode,
    string? Description);



/// <summary>
/// Immutable record representing information about a country in the database.
/// </summary>
/// <param name="Id">The unique identifier of the country.</param>
/// <param name="IsoAlpha2">The ISO 3166-1 alpha-2 code of the country.</param>
/// <param name="IsoAlpha3">The ISO 3166-1 alpha-3 code of the country.</param>
/// <param name="IsoNumeric">The ISO 3166-1 numeric code of the country.</param>
/// <param name="Name">The name of the country.</param>
/// <param name="IsActive">Indicates whether the country is active.</param>
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



    /// <summary>
    /// Initializes a new instance of the <see cref="KtDbPreload"/> class with the specified database connection string.
    /// </summary>
    /// <param name="connectionString">The connection string to the database.</param>
    public KtDbPreload(string connectionString) {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("A database connection string is required.", nameof(connectionString));

        _connectionString = connectionString;
    }


    /// <summary>
    /// Gets a value indicating whether the catalogues have been loaded.
    /// </summary>
    public bool IsLoaded => Volatile.Read(ref _current) is not null;


    /// <summary>
    ///     Gets the current snapshot of the loaded catalogues.
    /// </summary>
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