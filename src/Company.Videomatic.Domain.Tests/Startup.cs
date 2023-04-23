using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Company.Videomatic.Domain.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        var cfg = LoadConfiguration();
    }

    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
                        //.AddJsonFile("testSettings.json", false)
                        //.AddUserSecrets(typeof(Startup).Assembly)
                        .Build();
    }
}
