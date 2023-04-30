using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Infrastructure.Data.Sqlite;
using Company.Videomatic.Infrastructure.Data.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Company.Videomatic.Infrastructure.Data.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        var cfg = LoadConfiguration();

        services.AddVideomaticData(cfg);
        services.AddVideomaticSqliteDbContext(cfg);
        services.AddVideomaticSqlServerDbContext(cfg);
        
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
