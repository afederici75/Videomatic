using Company.SharedKernel.Abstractions;
using Company.SharedKernel;
using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Infrastructure.TestData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Company.Videomatic.Integration.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        var cfg = LoadConfiguration();

        services.AddLogging(x => x.AddConsole());

        services.AddApplication(cfg);
        services.AddYouTubeInfrastructure(cfg);
        services.AddSemanticKernelInfrastructure(cfg);
        services.AddSqlServerInfrastructure(cfg);

        services.AddScoped<IVideoImporter, MockVideoImporter>();
        services.AddScoped<IVideoAnalyzer, MockVideoAnalyzer>();
    }

    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
                        .AddJsonFile("testSettings.json", false)
                        .AddUserSecrets(typeof(Startup).Assembly)
                        .Build();
    }
}
