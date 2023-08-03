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
        // Keep this here!
        services.AddDbContext<VideomaticDbContext, SqlServerVideomaticDbContext>();

        services.AddDbContextFactory<VideomaticDbContext, VideomaticSqlServerDbContextFactory>(
            (sp, builder) =>
            {
                var cfg = configuration;
                Configure(builder, cfg);
            },
            ServiceLifetime.Scoped);

        return services;
    }

    public abstract class VideomaticDbContextFactory<TDBCONTEXT> : IDbContextFactory<TDBCONTEXT>
        where TDBCONTEXT : VideomaticDbContext
    {
        public VideomaticDbContextFactory(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IServiceProvider ServiceProvider { get; }
        public IConfiguration Configuration { get; }

        public TDBCONTEXT CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder();
            ConfigureOptions(builder, Configuration);

            return (TDBCONTEXT)Activator.CreateInstance(typeof(TDBCONTEXT), builder.Options)!;
        }

        protected abstract void ConfigureOptions(DbContextOptionsBuilder builder, IConfiguration configuration);
    }

    public class VideomaticSqlServerDbContextFactory : VideomaticDbContextFactory<SqlServerVideomaticDbContext>, IDbContextFactory<VideomaticDbContext>
    {
        public VideomaticSqlServerDbContextFactory(IConfiguration cfg) : base(cfg) { }

        protected override void ConfigureOptions(DbContextOptionsBuilder builder, IConfiguration configuration)
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
        }

        VideomaticDbContext IDbContextFactory<VideomaticDbContext>.CreateDbContext()
        {
            return base.CreateDbContext();
        }
    }
}