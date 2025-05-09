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
        using var connection = transaction?.Connection ?? _connection.CreateConnection();
        return await connection.QueryAsync<T>(sql, param, transaction);
    }

    protected async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null)
    {
        using var connection = transaction?.Connection ?? _connection.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    protected async Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null)
    {
        using var connection = transaction?.Connection ?? _connection.CreateConnection();
        return await connection.ExecuteAsync(sql, param, transaction);
    }

    protected async Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null)
    {
        using var connection = transaction?.Connection ?? _connection.CreateConnection();
        return await connection.ExecuteScalarAsync<T>(sql, param, transaction);
    }

    protected void BeginTransaction()
    {
        _currentConnection = _connection.CreateConnection();
        _currentTransaction = _currentConnection.BeginTransaction();
    }

    protected void JoinTransaction(IDbTransaction transaction)
    {
        _currentTransaction = transaction;
        _currentConnection = transaction.Connection;
    }

    protected void CommitTransaction()
    {
        try
        {
            _currentTransaction?.Commit();
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentConnection?.Dispose();
            _currentTransaction = null;
            _currentConnection = null;
        }
    }

    protected void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentConnection?.Dispose();
            _currentTransaction = null;
            _currentConnection = null;
        }
    }

    protected IDbTransaction? GetCurrentTransaction() => _currentTransaction;
}
