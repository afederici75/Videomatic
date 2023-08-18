using Infrastructure.Data.SqlServer;
using Hangfire;
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
            .ConfigureServices((context, services) =>
            {
                services.AddLogging(x => x.AddConsole());

                services.AddVideomaticApplication(context.Configuration);
                services.AddVideomaticData(context.Configuration);
                services.AddVideomaticDataForSqlServer(context.Configuration);

                services.AddVideomaticSemanticKernel(context.Configuration);
                services.AddVidematicYouTubeInfrastructure(context.Configuration);
                services.AddAzureSpeech(context.Configuration);

                services.AddHangfire(configuration =>
                {
                    configuration
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseActivator(new ContainerJobActivator(services.BuildServiceProvider()))
                        .UseFilter(new AutomaticRetryAttribute { Attempts = 0 })
                        .UseSqlServerStorage(
                            context.Configuration.GetConnectionString($"Videomatic.SqlServer"), // TODO: remove magical string
                            new Hangfire.SqlServer.SqlServerStorageOptions()
                            {
                                PrepareSchemaIfNecessary = true
                            });
                });

                // Add the processing server as IHostedService
                services.AddHangfireServer();
                
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
}

// TODO: this is copied from src\VideomaticRadzen\ContainerJobActivator.cs
public class ContainerJobActivator : JobActivator
{
    readonly IServiceProvider Provider;

    public ContainerJobActivator(IServiceProvider provider)
    {
        Provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public override object ActivateJob(Type type)
    {
        return Provider.GetRequiredService(type);
    }
}