using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjectionExtensions
{
    public class VideomaticSqlServerDbContextFactory : VideomaticDbContextFactory<SqlServerVideomaticDbContext>, IDbContextFactory<VideomaticDbContext>
    {
        public VideomaticSqlServerDbContextFactory(IConfiguration cfg, ILoggerFactory loggerFactory) : base(cfg, loggerFactory) { }

        protected override void ConfigureOptions(DbContextOptionsBuilder builder, IConfiguration configuration)
        {
            // Looks for the connection string SqlServer
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
            var db = base.CreateDbContext();

            return db;
        }
    }
}