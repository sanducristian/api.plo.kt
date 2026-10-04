using MySqlConnector;

namespace Kt.Data;

/// <summary>One actor, connection and optional transaction. Not thread safe; do not execute commands concurrently.</summary>
public sealed class KtDbSession : IAsyncDisposable {
    private readonly MySqlConnection _connection;
    private MySqlTransaction? _transaction = null;
    private bool _disposed = false;



    /// <summary>
    /// Gets the ID of the authenticated user associated with this session.
    /// </summary>
    public ulong PrincipalId { get; }



    /// <summary>
    /// Gets a value indicating whether there is an active transaction in this session.
    /// </summary>
    public bool HasTransaction => _transaction is not null;



    /// <summary>
    /// Initializes a new instance of the <see cref="KtDbSession"/> class with the specified MySQL connection and principal ID.
    /// </summary>
    /// <param name="connection">The MySQL connection to use for the session.</param>
    /// <param name="principalId">The ID of the authenticated user.</param>
    internal KtDbSession(MySqlConnection connection, ulong principalId) {
        _connection = connection;
        PrincipalId = principalId;
    }


    /// <summary>
    /// Creates a new MySqlCommand with the specified SQL query, using the current connection and transaction (if any).
    /// Intentionally internal: HTTP clients must never supply SQL through this library.
    /// </summary>
    /// <param name="sql">The SQL query to execute.</param>
    /// <returns>A new MySqlCommand instance.</returns>
    internal MySqlCommand CreateCommand(string sql) {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return new MySqlCommand(sql, _connection, _transaction);
    }


    /// <summary>
    /// Begins a new transaction on the current connection. Throws an exception if a transaction is already active.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if a transaction is already active.</exception>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) {
        ObjectDisposedException.ThrowIf(_disposed, this);

        // Ensure that there is no active transaction before starting a new one
        if (_transaction is not null) throw new InvalidOperationException("A transaction is already active.");

        // Begin a new transaction and store it in the _transaction field
        _transaction = await _connection.BeginTransactionAsync(cancellationToken);
    }


    /// <summary>
    /// Commits the current transaction. Throws an exception if there is no active transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if there is no active transaction.</exception>
    public async Task CommitAsync(CancellationToken cancellationToken = default) {
        ObjectDisposedException.ThrowIf(_disposed, this);
        var transaction = _transaction ?? throw new InvalidOperationException("No active transaction.");
        await transaction.CommitAsync(cancellationToken);
        _transaction = null;
        await transaction.DisposeAsync();
    }



    /// <summary>
    /// Rolls back the current transaction. Throws an exception if there is no active transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if there is no active transaction.</exception>
    public async Task RollbackAsync(CancellationToken cancellationToken = default) {
        ObjectDisposedException.ThrowIf(_disposed, this);
        var transaction = _transaction ?? throw new InvalidOperationException("No active transaction.");
        await transaction.RollbackAsync(cancellationToken);
        _transaction = null;
        await transaction.DisposeAsync();
    }


    /// <summary>
    /// Disposes the current session asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync() {
        if (_disposed) return;
        _disposed = true;
        try {
            if (_transaction is not null) await _transaction.DisposeAsync();
        }
        finally {
            _transaction = null;
            // Pool checkout reset plus explicit actor initialisation in KtDb prevent actor reuse.
            await _connection.DisposeAsync();
        }
    }
}
