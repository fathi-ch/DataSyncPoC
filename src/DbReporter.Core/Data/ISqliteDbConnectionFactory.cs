using System.Data;

namespace DbReporter.Core.Data;

public interface ISqliteDbConnectionFactory
{
    Task<IDbConnection> CreateDbConnectionAsync();
}