using Company.SharedKernel;
using Company.SharedKernel.Abstractions;
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
        services.AddApplication(cfg);

        // Mocks
        services.AddScoped(typeof(IRepositoryBase<>), typeof(InMemoryRepository<>)); // Ardalis.Specification 
        services.AddScoped(typeof(IReadRepositoryBase<>), typeof(InMemoryRepository<>)); // Ardalis.Specification 

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