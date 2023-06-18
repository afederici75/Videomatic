using AutoMapper;
using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Behaviors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{    
    public static IServiceCollection AddVideomaticApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions

        // Services
        services.AddValidatorsFromAssembly(typeof(LoggingBehaviour<,>).Assembly);

        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(typeof(LoggingBehaviour<,>).Assembly);            
        })            
        .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
        .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddAutoMapper((cfg) =>
        {            
        },
        AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }       
}