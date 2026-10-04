using System.Globalization;
using MySqlConnector;

namespace Kt.Data;

/// <summary>Application-owned pooled data source. Register one instance for the host lifetime.</summary>
public sealed class KtDb : IAsyncDisposable {
    private readonly MySqlDataSource _source;


    /// <summary>
    /// Initializes a new instance of the <see cref="KtDb"/> class with the specified connection string.
    /// </summary>
    /// <param name="connectionString">The connection string to use for the database connection.</param>
    public KtDb(string connectionString) {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        var settings = new MySqlConnectionStringBuilder(connectionString) {
            Pooling = true,
            ConnectionReset = true,
            AllowUserVariables = true
        };
        _source = new MySqlDataSourceBuilder(settings.ConnectionString).Build();
    }



    /// <summary>principalId must come from trusted, validated authentication, never the request body.</summary>
    /// <param name="principalId">The ID of the authenticated user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A new database session with the specified principal ID.</returns>
    public async Task<KtDbSession> OpenAsync(ulong principalId, CancellationToken cancellationToken = default) {
        if (principalId == 0) throw new ArgumentOutOfRangeException(nameof(principalId));
        var connection = await _source.OpenConnectionAsync(cancellationToken);
        try {
            using (var command = connection.CreateCommand()) {
                command.CommandText = "SET @plo_user_id = @actor, time_zone = '+00:00';";
                command.Parameters.Add("@actor", MySqlDbType.UInt64).Value = principalId;
                await command.ExecuteNonQueryAsync(cancellationToken);
            }
            using (var command = connection.CreateCommand()) {
                command.CommandText = "SELECT GetUserId();";
                var value = await command.ExecuteScalarAsync(cancellationToken);
                if (value is null or DBNull || Convert.ToUInt64(value, CultureInfo.InvariantCulture) != principalId)
                    throw new UnauthorizedAccessException("Database actor validation failed.");
            }
            return new KtDbSession(connection, principalId);
        }
        catch {
            await connection.DisposeAsync();
            throw;
        }
    }



    /// <summary>Commits only after work succeeds. Disposal rolls back uncommitted work. No automatic retries.</summary>
    /// <param name="principalId">The ID of the authenticated user.</param>
    /// <param name="work">The work to perform within the transaction.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the work performed within the transaction.</returns>
    public async Task<T> InTransactionAsync<T>(ulong principalId, Func<KtDbSession, CancellationToken, Task<T>> work, CancellationToken cancellationToken = default) {
        ArgumentNullException.ThrowIfNull(work);
        await using var session = await OpenAsync(principalId, cancellationToken);
        await session.BeginTransactionAsync(cancellationToken);
        var result = await work(session, cancellationToken);
        await session.CommitAsync(cancellationToken);
        return result;
    }


    /// <summary>
    /// Disposes the underlying data source asynchronously.
    /// </summary>
    /// <returns>A ValueTask representing the asynchronous dispose operation.</returns>
    public ValueTask DisposeAsync() => _source.DisposeAsync();
}
