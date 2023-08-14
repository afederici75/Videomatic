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
        services.AddScoped(typeof(IRepository<>), typeof(MyNewRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(MyNewRepository<>));
        services.AddScoped(typeof(IDbSeeder), typeof(DbSeeder));
        return services;
    }
}

public class MyNewRepository<T> : EfRepository<T>
    where T : class, IEntity
{
    public MyNewRepository(IDbContextFactory<VideomaticDbContext> factory) : 
        base(factory.CreateDbContext())
    {        
        Id = Guid.NewGuid().ToString();
    }    

    public string Id { get; }
}