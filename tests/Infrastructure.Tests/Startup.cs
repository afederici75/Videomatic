using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Tests;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        var cfg = LoadConfiguration();

        services.AddLogging(x => x.AddConsole());

        services.AddVideomaticApplication(cfg);
        services.AddVideomaticData(cfg);
        services.AddVideomaticDataForSqlServer(cfg);
        
        services.AddVideomaticSemanticKernel(cfg);
        services.AddVidematicYouTubeInfrastructure(cfg);
        services.AddAzureSpeech(cfg);        
    }

    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
                        .AddJsonFile("testSettings.json", false)
                        .AddUserSecrets(typeof(Startup).Assembly)
                        .Build();
    }
}
