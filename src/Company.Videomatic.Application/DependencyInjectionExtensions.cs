using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Implementations;
using Company.Videomatic.Application.Options;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        var section = configuration.GetRequiredSection("Application");
        services.Configure<ApplicationOptions>(section);

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationOptions).Assembly);
            //cfg.AddBehavior<IPipelineBehavior<Ping, Pong>, PingPongBehavior>();
            //cfg.AddOpenBehavior(typeof(GenericBehavior<,>));
        });

        // Services
        services.AddScoped<IVideoStorage, InMemoryVideoStorage>();

        return services;
    }   
}