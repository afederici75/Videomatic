using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Drivers.YouTube.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        var cfg = LoadConfiguration();

        services.AddLogging(x => x.AddConsole());
        services.AddYouTubeDrivers(cfg);
    }

    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
                        .AddJsonFile("testSettings.json", false)
                        .AddUserSecrets(typeof(Startup).Assembly)
                        .Build();
    }
}
