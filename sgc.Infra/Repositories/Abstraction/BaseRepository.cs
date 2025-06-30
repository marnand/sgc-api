using Dapper;
using sgc.Infra.Security;
using System.Data;

namespace sgc.Infra.Repositories.Abstraction;

public abstract class BaseRepository(ConnectionFactory connection)
{
    private readonly ConnectionFactory _connection = connection;
    private IDbConnection? _currentConnection;
    private IDbTransaction? _currentTransaction;

    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null)
    {
        var conn = transaction?.Connection ?? _currentConnection ?? _connection.CreateConnection();
        var shouldDisposeConnection = transaction == null && _currentConnection == null;

        try
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            return await conn.QueryAsync<T>(sql, param, transaction ?? _currentTransaction);
        }
        finally
        {
            if (shouldDisposeConnection)
                conn?.Dispose();
        }
    }

    protected async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null)
    {
        var conn = transaction?.Connection ?? _currentConnection ?? _connection.CreateConnection();
        var shouldDisposeConnection = transaction == null && _currentConnection == null;

        try
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            return await conn.QueryFirstOrDefaultAsync<T>(sql, param, transaction ?? _currentTransaction);
        }
        finally
        {
            if (shouldDisposeConnection)
                conn?.Dispose();
        }
    }

    protected async Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null)
    {
        var conn = transaction?.Connection ?? _currentConnection ?? _connection.CreateConnection();
        var shouldDisposeConnection = transaction == null && _currentConnection == null;

        try
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            return await conn.ExecuteAsync(sql, param, transaction ?? _currentTransaction);
        }
        finally
        {
            if (shouldDisposeConnection)
                conn?.Dispose();
        }
    }

    protected async Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null)
    {
        var conn = transaction?.Connection ?? _currentConnection ?? _connection.CreateConnection();
        var shouldDisposeConnection = transaction == null && _currentConnection == null;

        try
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            return await conn.ExecuteScalarAsync<T>(sql, param, transaction ?? _currentTransaction);
        }
        finally
        {
            if (shouldDisposeConnection)
                conn?.Dispose();
        }
    }

    public void BeginTransaction()
    {
        if (_currentTransaction != null)
            throw new InvalidOperationException("Uma transação já está ativa.");

        _currentConnection = _connection.CreateConnection();
        _currentConnection.Open();
        _currentTransaction = _currentConnection.BeginTransaction();
    }

    public void JoinTransaction(IDbTransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        _currentTransaction = transaction;
        _currentConnection = transaction.Connection;
    }

    public void CommitTransaction()
    {
        try
        {
            _currentTransaction?.Commit();
        }
        finally
        {
            DisposeTransaction();
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            DisposeTransaction();
        }
    }

    private void DisposeTransaction()
    {
        _currentTransaction?.Dispose();
        _currentConnection?.Dispose();
        _currentTransaction = null;
        _currentConnection = null;
    }

    public IDbTransaction? GetCurrentTransaction() => _currentTransaction;
}

