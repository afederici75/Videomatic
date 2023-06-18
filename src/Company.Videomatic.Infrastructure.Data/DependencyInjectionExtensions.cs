using Company.Videomatic.Infrastructure.Data.Handlers;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVideomaticData(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddScoped<PlaylistCommandsHandler>();
        services.AddScoped<PlaylistQueriesHandler>();
        
        return services;
    }
}