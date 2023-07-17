using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    static void Configure(DbContextOptionsBuilder builder, IConfiguration configuration)
    {
        // Looks for the connection string Videomatic.SqlServer
        var connectionName = $"{VideomaticConstants.Videomatic}.{SqlServerVideomaticDbContext.ProviderName}";
        var connString = configuration.GetConnectionString(connectionName);
        if (string.IsNullOrWhiteSpace(connString))
        {
            throw new Exception($"Required connection string '{connectionName}' missing.");
        }

        builder.EnableSensitiveDataLogging()
               //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
               .UseSqlServer(connString, (opts) =>
               {
                   opts.MigrationsAssembly(VideomaticConstants.MigrationAssemblyNamePrefix + SqlServerVideomaticDbContext.ProviderName);
               });

        // Services        
    }

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
        //services.AddDbContext<VideomaticDbContext, SqlServerVideomaticDbContext>(
        //    (sp, builder) => Configure(sp, builder, configuration),
        //    ServiceLifetime.Scoped);

        services.AddDbContextFactory<VideomaticDbContext, SqlServerVideomaticDbContextFactory>(
            (sp, builder) =>
            {
                Configure(builder, configuration);
            });

        return services;
    }

    //public static IServiceCollection AddVideomaticSqlServerDbContextForTests(
    //    this IServiceCollection services,
    //    IConfiguration configuration)
    //    => services.AddDbContext<VideomaticDbContext, SqlServerVideomaticDbContext>((sp, builder) => Configure(sp, builder, configuration));
}