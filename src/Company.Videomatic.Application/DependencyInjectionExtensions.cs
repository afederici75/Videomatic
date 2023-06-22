using AutoMapper;
using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Behaviors;
using Company.Videomatic.Infrastructure.Data.Seeder;
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
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => a.FullName?.Contains(".Videomatic.") ?? false);
            cfg.RegisterServicesFromAssemblies(assemblies.ToArray());// typeof(LoggingBehaviour<,>).Assembly);            
        })            
        .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
        .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddAutoMapper((cfg) =>
        {
        },
        AppDomain.CurrentDomain.GetAssemblies());

        services.AddValidatorsFromAssembly(typeof(Filter).Assembly); // Validation is all in this Assembly!
        
        services.AddTransient<IDataSeeder, DataSeeder>();

        return services;
    }       
}