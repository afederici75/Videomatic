using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Company.Videomatic.Infrastructure.Data.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        var cfg = LoadConfiguration();

        services.AddVideomaticData(cfg);
        //services.AddVideomaticSqliteDbContextForTests(cfg);
        services.AddVideomaticSqlServerDbContextForTests(cfg);
        
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
