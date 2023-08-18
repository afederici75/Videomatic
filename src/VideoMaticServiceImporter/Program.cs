using VideoMaticServiceImporter;

var builder = Host.CreateDefaultBuilder(args);
var host = builder
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetupVideomaticConfiguration();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();

        services.AddVideomaticServer(
            configuration: context.Configuration,
            addHangfireHostedService: true,
            workerCount: 20, 
            registerDbContextFactory: true);
    })
    .Build();

host.Run();