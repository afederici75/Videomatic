using Company.Videomatic.Infrastructure.SqlServer;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    static IServiceCollection AddCommon(this IServiceCollection services)
    {
        // Services
        services.AddScoped(typeof(IRepositoryBase<>), typeof(VideomaticRepository<>)); // Ardalis.Specification 
        services.AddScoped(typeof(IReadRepositoryBase<>), typeof(VideomaticRepository<>)); // Ardalis.Specification 

        return services;
    }

    public static IServiceCollection AddSqlServerInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<VideomaticDbContext>(builder =>
        {
            var connStr = configuration.GetConnectionString("Videomatic");
        
            builder.EnableSensitiveDataLogging()
                   //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                   .UseSqlServer(connStr);
        });

        services.AddCommon();

        return services;
    }

    public static IServiceCollection AddInMemoryInfrastructure(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder, IConfiguration> configureAction,
        IConfiguration configuration)
    {
        services.AddDbContext<VideomaticDbContext>(o => configureAction(o, configuration));

        services.AddCommon();

        return services;
    }
}