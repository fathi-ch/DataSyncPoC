using System.Data;
using DbReporter.Core.Data;
using Microsoft.Data.Sqlite;

public class SqliteDbConnectionFactory : ISqliteDbConnectionFactory
{
    private string _connectionString;
    
    public SqliteDbConnectionFactory()
    {
      
        _connectionString = $"Data Source=d:\\Code\\MyDB.db";
    }
    public async Task<IDbConnection> CreateDbConnectionAsync()
    {
        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}