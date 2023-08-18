using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using static Microsoft.Extensions.DependencyInjection.DependencyInjectionExtensions;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjectionExtensions
{
    private const string ConnectionStringName = "Videomatic.SqlServer";    

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
        services = services.AddDbContextFactory<SqlServerVideomaticDbContext>(
            (sp, builder) =>
            {
                var cfg = configuration;
                var connString = configuration.GetConnectionString(ConnectionStringName);

                builder.EnableSensitiveDataLogging()
                       .UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>())
                       .UseSqlServer(connString, (opts) =>
                        {
                            opts.MigrationsAssembly(typeof(SqlServerVideomaticDbContext).Assembly.FullName);                                                    
                        });
            },                 
            lifetime: ServiceLifetime.Transient);

        services.AddTransient<VideomaticDbContext, SqlServerVideomaticDbContext>(); // Because of inheritance

        return services;
    }

    
}