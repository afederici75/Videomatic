using Company.Videomatic.Infrastructure.YouTube;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVidematicYouTubeInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        var section = configuration.GetRequiredSection("YouTube");
        services.Configure<YouTubeOptions>(section);

        // Services
        services.AddScoped<IVideoHostingProvider, YouTubeVideoHostingProvider>();
        services.AddScoped<IVideoImporter, YouTubeImporter>();
        
        return services;
    }   
}