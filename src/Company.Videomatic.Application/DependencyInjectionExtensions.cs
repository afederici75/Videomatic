using Company.Videomatic.Application;
using Company.Videomatic.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        var section = configuration.GetRequiredSection("Application");
        services.Configure<ApplicationOptions>(section);

        // Services
        return services;
    }   
}