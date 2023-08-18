using Application;
using Application.Behaviors;
using Microsoft.Extensions.Configuration;

// This is required so I can mark validators as 'internal' (i.e. instead of public) and still be able to access them from the test project.
// See https://learn.microsoft.com/en-us/dotnet/standard/assembly/friend for more information.

//[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Application.Tests")]

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{    
    public static IServiceCollection AddVideomaticApplication(this IServiceCollection services, IConfiguration configuration)
    {        
        var videomaticAssemblies = AppDomain.CurrentDomain
            .GetAssemblies() 
            //.Where(a => a.  FullName?.Contains("Videomatic") ?? false) // TODO: what is the best way to filter VM's assemblies?
            .ToArray();

        services.AddMediatR(cfg =>        
        {            
            cfg.RegisterServicesFromAssemblies(videomaticAssemblies);
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddAutoMapper((cfg) => { },
            videomaticAssemblies
        );


        services.AddValidatorsFromAssembly(
            typeof(AutomappingProfile).Assembly, // The only validators are in this assembly
            includeInternalTypes: true // Important! All our validators are internal, so we need to include them..
            );

        return services;
    }       
}