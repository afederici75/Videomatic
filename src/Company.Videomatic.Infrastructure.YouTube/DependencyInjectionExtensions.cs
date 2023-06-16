using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Infrastructure.YouTube;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVidematicYouTubeInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        var section = configuration.GetRequiredSection("YouTube");
        services.Configure<YouTubeOptions>(section);

        // Services
        services.AddScoped<IYouTubeHelper, YouTubePlaylistsHelper>();
        services.AddHttpClient<YouTubePlaylistsHelper>(client =>
        {

            client.BaseAddress = new Uri("https://www.googleapis.com/youtube/v3/");
        });

        return services;
    }   
}