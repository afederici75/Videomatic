using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Infrastructure.YouTube;
using Company.Videomatic.Infrastructure.YouTube.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddYouTubeDrivers(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        var section = configuration.GetRequiredSection("YouTube");
        services.Configure<YouTubeOptions>(section);

        // Services
        services.AddScoped<IVideoImporter, YouTubeVideoImporter>();
        //services.AddScoped<IVideoProvider, MockYouTubeVideoProvider>();
        return services;
    }   
}