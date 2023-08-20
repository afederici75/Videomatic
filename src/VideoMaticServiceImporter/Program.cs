var builder = Host.CreateDefaultBuilder(args);
var host = builder
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetupVideomaticConfiguration();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddVideomaticServer(
            configuration: context.Configuration,
            addHangfireHostedService: true,
            workerCount: null /* Leave the defaults to Hangfire */);
    })
    .Build();

host.Run();