namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVideomaticData(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddScoped(typeof(IPlaylistRepository), typeof(PlaylistRepository));
        services.AddScoped(typeof(IVideoRepository), typeof(VideoRepository));
        
        return services;
    }
}