using Company.SharedKernel;
using Company.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Company.Videomatic.Application.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        var cfg = LoadConfiguration();

        services.AddLogging(x => x.AddConsole());
        services.AddVideomaticApplication(cfg);

        // Mocks
        services.AddVideomaticData(cfg);
        //services.AddVideomaticDataForSqlServer(cfg);
        services.AddVideomaticDataForSqlite(cfg);

        // Overrides
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

// 