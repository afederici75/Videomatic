using Company.Videomatic.Infrastructure.Data;
using Company.Videomatic.Infrastructure.Data.Seeder;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds IRepository and IReadRepository
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddVideomaticData(this IServiceCollection services, IConfiguration configuration)
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