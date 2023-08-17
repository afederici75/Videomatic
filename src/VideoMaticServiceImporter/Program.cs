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

        services.AddVideomaticServer(context.Configuration);
    })
    .Build();

host.Run();