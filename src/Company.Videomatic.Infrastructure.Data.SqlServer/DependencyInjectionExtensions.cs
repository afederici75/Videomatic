using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    
    /// <summary>
    /// Adds scoped DbContext for VideomaticDbContext.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddVideomaticDataForSqlServer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<VideomaticDbContext, SqlServerVideomaticDbContext>();

        services.AddDbContextFactory<VideomaticDbContext, SqlServerVideomaticDbContextFactory>();

        return services;
    }
}