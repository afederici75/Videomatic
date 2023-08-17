using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVideomaticSharedKernel(this IServiceCollection services, IConfiguration configuration)
    {
        // Adds Serilog from the configuration
        var cfg = new LoggerConfiguration().ReadFrom.Configuration(configuration);
        Serilog.Core.Logger logger = cfg.CreateLogger();

        services.AddLogging(bld =>
        {
            bld.ClearProviders();
            bld.AddSerilog(logger: logger, dispose: true);
        });

        return services;
    }
}
