using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Company.Videomatic.Infrastructure.Data.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        var cfg = LoadConfiguration();

        services.AddVideomaticApplication(cfg);
        services.AddVideomaticData(cfg);
        services.AddVideomaticDataForSqlServer(cfg);
        
        services.AddScoped<IVideoImporter, MockVideoImporter>();        
    }

    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
                        .AddJsonFile("testSettings.json", false)
                        .AddUserSecrets(typeof(Startup).Assembly, false)
                        .Build();
    }
}
