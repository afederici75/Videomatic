namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjectionExtensions
{
    private const string ConnectionStringName = "Videomatic.SqlServer";

    static void Configure(DbContextOptionsBuilder builder, IConfiguration configuration)
    {
        // Looks for the connection string SqlServer
        //var connectionName = $"{VideomaticConstants.Videomatic}.{SqlServerVideomaticDbContext.ProviderName}";
        var connString = configuration.GetConnectionString(ConnectionStringName);
        if (string.IsNullOrWhiteSpace(connString))
        {
            throw new Exception($"Required connection string '{ConnectionStringName}' missing.");
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
        IConfiguration configuration,
        bool registerDbContextFactory)
    {
        // Keep this here!
        services.AddDbContext<VideomaticDbContext, SqlServerVideomaticDbContext>(
            (sp, builder) =>
            {
                var cfg = configuration;
                Configure(builder, cfg);
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient
            );

        if (registerDbContextFactory)
        {
            services = services.AddDbContextFactory<VideomaticDbContext, VideomaticSqlServerDbContextFactory>(
                (sp, builder) =>
                {
                    var cfg = configuration;
                    Configure(builder, cfg);                    
                },                 
                lifetime: ServiceLifetime.Singleton);
        }

        //services.AddDbContext<VideomaticDbContext, SqlServerVideomaticDbContext>(
        //    (sp, builder) =>
        //    {
        //        var cfg = configuration;
        //        Configure(builder, cfg);
        //    },  
        //    contextLifetime: ServiceLifetime.Transient,
        //    optionsLifetime: ServiceLifetime.Transient);

        return services;
    }
}