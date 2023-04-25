using Company.Videomatic.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{    
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions

        // Services
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(IVideoAnalyzer).Assembly);
            //cfg.AddBehavior<IPipelineBehavior<Ping, Pong>, PingPongBehavior>();
            //cfg.AddOpenBehavior(typeof(GenericBehavior<,>));
        });


        return services;
    }       
}