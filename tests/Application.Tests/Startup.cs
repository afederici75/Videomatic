using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Tests;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        context.Configuration = LoadConfiguration();

        services.AddLogging(x => x.AddConsole());
        services.AddVideomaticApplication(context.Configuration);        
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