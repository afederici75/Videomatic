using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Tests;

public class Startup
{
    public Startup() { }

    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        hostBuilder
            .ConfigureHostConfiguration(builder =>
            {

            })
            .ConfigureAppConfiguration((context, builder) =>
            {
                // IMPORTANT: if I don't do this here the IConfiguration and IServiceProvider 
                // are empty or default and tests fail later (e.g. read conn strings).
                // See https://github.com/pengweiqhca/Xunit.DependencyInjection#how-to-inject-iconfiguration-or-ihostenvironment-into-startup
                var cfg = new ConfigurationBuilder()
                        .AddJsonFile("testSettings.json", false)
                        .AddUserSecrets(typeof(Startup).Assembly)
                        .Build();

                builder.AddConfiguration(cfg);
            });
    }

    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddLogging(x => x.AddConsole());

        services.AddVideomaticApplication(context.Configuration);
        services.AddVideomaticData(context.Configuration);
        services.AddVideomaticDataForSqlServer(context.Configuration);

        services.AddVideomaticSemanticKernel(context.Configuration);
        services.AddVidematicYouTubeInfrastructure(context.Configuration);
        services.AddAzureSpeech(context.Configuration);
    }
}
