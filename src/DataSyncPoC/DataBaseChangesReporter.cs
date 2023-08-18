using Dapper;
using DbReporter.Core.Data;
using DbReporter.Core.TargetToMonitor;

namespace DataSyncPoC;

public class DataBaseChangesReporter : BackgroundService
{
    private readonly ISqliteDbConnectionFactory _connectionFactory;
    private readonly ILogger<DataBaseChangesReporter> _logger;

    public DataBaseChangesReporter(ISqliteDbConnectionFactory connectionFactory, ILogger<DataBaseChangesReporter> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken )
    {
        int lastId = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Monitoring is running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
            var dataBaseReport = new ClientChecks(_connectionFactory);
            
           var results =  await dataBaseReport.CheckClientUpdates(lastId);
           var connection = await _connectionFactory.CreateDbConnectionAsync();
           foreach (var clientLog in results)
           {
               _logger.LogInformation("the newly inserted client: {clientLog} with Id: {Id}", clientLog.Name, clientLog.Id);
               lastId = clientLog.Id;
              
               await connection.ExecuteAsync($"UPDATE clientLogs SET Processed=1 WHERE Id={lastId}");

           }
           await connection.ExecuteAsync($"INSERT INTO clients (Name) VALUES ('test{lastId}')");



        }
    }
}