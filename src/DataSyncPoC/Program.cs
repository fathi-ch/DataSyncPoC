using DataSyncPoC;
using DbReporter.Core.Data;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ISqliteDbConnectionFactory, SqliteDbConnectionFactory>();
        services.AddHostedService<DataBaseChangesReporter>();
        
    })
    .Build();



await host.RunAsync();