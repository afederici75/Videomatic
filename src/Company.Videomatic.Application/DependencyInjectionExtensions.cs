using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Behaviors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public class MockVideoAnalzyer : IVideoAnalyzer
{
    Task<Artifact> IVideoAnalyzer.ReviewVideoAsync(Video video)
    {
        return Task.FromResult(new Artifact("", "", ""));
    }

    Task<Artifact> IVideoAnalyzer.SummarizeVideoAsync(Video video)
    {
        return Task.FromResult(new Artifact("", "", ""));
    }
}

public class MockPlaylistImporter : IPlaylistImporter
{
    public Task<Playlist> ImportAsync(Uri location)
    {
        return Task.FromResult(new Playlist("A dummy video collection", "More dummy stuff..."));
    }
}   

public class MockVideoImporter : IVideoImporter
{
    public Task<Video> ImportAsync(Uri location)
    {
        return Task.FromResult(new Video("http://somewhere.com/v1", "A dummy video", "More dummy stuff..."));
    }
}

public static class DependencyInjectionExtensions
{    
    public static IServiceCollection AddVideomaticApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions

        // Services
        services.AddValidatorsFromAssembly(typeof(IVideoAnalyzer).Assembly);

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(IVideoAnalyzer).Assembly);
            //cfg.AddBehavior<IPipelineBehavior<Ping, Pong>, PingPongBehavior>();
            //cfg.AddOpenBehavior(typeof(GenericBehavior<,>));
        })            
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));        

        services.AddTransient<IVideoImporter, MockVideoImporter>();  
        services.AddTransient<IVideoAnalyzer, MockVideoAnalzyer>();
        services.AddTransient<IPlaylistImporter, MockPlaylistImporter>();

        return services;
    }       
}