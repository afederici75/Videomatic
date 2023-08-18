using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
                       .UseSqlServer(connString, (opts) =>
                        {
                            opts.MigrationsAssembly(VideomaticConstants.MigrationAssemblyNamePrefix + SqlServerVideomaticDbContext.ProviderName);
                        });
            },                 
            lifetime: ServiceLifetime.Transient);

        services.AddTransient<VideomaticDbContext, SqlServerVideomaticDbContext>();

        //services.AddTransient(typeof(IDbContextFactory<VideomaticDbContext>), typeof(DbContextFactory<VideomaticDbContext>));
        //services.AddTransient(typeof(DbContextOptions<VideomaticDbContext>), typeof(DbContextOptions<SqlServerVideomaticDbContext>));


        return services;
    }

    
}