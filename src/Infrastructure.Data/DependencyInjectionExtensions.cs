using Infrastructure.Data;
using Infrastructure.Data.Seeder;
using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using SharedKernel.EntityFrameworkCore;

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
        services.AddTransient(typeof(IDbSeeder), typeof(DbSeeder));
        services.AddTransient(typeof(IRepositoryFactory<>), typeof(VideomaticRepositoryFactory<>));
        services.AddTransient(typeof(IMyRepository<>), typeof(VideomaticRepository<>));

        return services;
    }
}