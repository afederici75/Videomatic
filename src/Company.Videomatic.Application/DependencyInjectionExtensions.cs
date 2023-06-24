using Company.Videomatic.Application.Behaviors;
using Company.Videomatic.Application.Features.DataAccess;
using Company.Videomatic.Infrastructure.Data.Seeder;
using Microsoft.Extensions.Configuration;

// This is required so I can mark validators as 'internal' (i.e. instead of public) and still be able to access them from the test project.
// See https://learn.microsoft.com/en-us/dotnet/standard/assembly/friend for more information.
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Company.Videomatic.Application.Tests")]

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{    
    public static IServiceCollection AddVideomaticApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions

        // Services
        //services.AddValidatorsFromAssembly(typeof(LoggingBehaviour<,>).Assembly);
        services.AddValidatorsFromAssembly(
            typeof(PageResult<>).Assembly, // The only validators are in this assembly
            includeInternalTypes: true // TODO: Seems useless? Maybe it will surface in the app?
            );

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

        
        
        services.AddTransient<IDataSeeder, DataSeeder>();

        return services;
    }       
}