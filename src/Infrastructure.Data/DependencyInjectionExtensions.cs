using Infrastructure.Data;
using Infrastructure.Data.Seeder;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds IRepository and IReadRepository
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
#pragma warning disable IDE0060 // Remove unused parameter
    public static IServiceCollection AddVideomaticData(this IServiceCollection services, IConfiguration configuration)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        // Services
        services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddTransient(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddTransient(typeof(IDbSeeder), typeof(DbSeeder));
        return services;
    }
}