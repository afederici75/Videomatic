using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVideomaticData(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddScoped(typeof(IRepositoryBase<>), typeof(VideomaticRepository<>)); // Ardalis.Specification 
        services.AddScoped(typeof(IReadRepositoryBase<>), typeof(VideomaticRepository<>)); // Ardalis.Specification 

        return services;
    }
}