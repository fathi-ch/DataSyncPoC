using System.Text;
using Dapper;
using DbReporter.Core.Data;

namespace DbReporter.Core.TargetToMonitor;

public class ClientChecks
{
    private readonly ISqliteDbConnectionFactory _connectionFactory;

    public ClientChecks(ISqliteDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public  async Task<IEnumerable<ClientCheck>> CheckClientUpdates(int id)
    {
        var connection = await _connectionFactory.CreateDbConnectionAsync();
         
       
        var sb = new StringBuilder();
        sb.Append("SELECT * ");
        sb.Append("FROM clientLogs ");
        sb.Append("WHERE Id > @Id ");
        sb.Append("AND Processed != 1;");
       
       var query = sb.ToString();
       

        var result = await connection.QueryAsync<ClientCheck>(query, new {Id= id});
        

        return result;
    }
}

public class Client
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class ClientCheck
{
    public int Id { get; set; }
    public int LastId { get; set; }
    public string? Name { get; set; }
    public string? OperationType { get; set; }
    public bool Processed { get; set; }
}