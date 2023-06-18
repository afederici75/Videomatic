using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Company.Videomatic.Application.Tests;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        var cfg = LoadConfiguration();

        services.AddLogging(x => x.AddConsole());
        services.AddVideomaticApplication(cfg);

        // Mocks
        services.AddVideomaticData(cfg);        
        //services.AddVideomaticDataForSqlite(cfg);
        services.AddVideomaticDataForSqlServer(cfg);

        // Overrides
        //services.AddScoped<IVideoImporter, MockVideoImporter>();
        //services.AddScoped<IVideoAnalyzer, MockVideoAnalyzer>();
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