using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Domain.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        context.Configuration = LoadConfiguration();
    }

    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
                        //.AddJsonFile("testSettings.json", true)
                        //.AddUserSecrets(typeof(Startup).Assembly)
                        .Build();
    }
}
