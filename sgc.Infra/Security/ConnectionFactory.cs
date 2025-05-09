using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace sgc.Infra.Security;

public sealed class ConnectionFactory(IOptions<Database> config)
{
    private readonly Database _config = config.Value;

    public IDbConnection CreateConnection() => new NpgsqlConnection(_config.DefaultConnection);
}

public sealed class Database
{
    public string DefaultConnection { get; set; } = string.Empty;
}