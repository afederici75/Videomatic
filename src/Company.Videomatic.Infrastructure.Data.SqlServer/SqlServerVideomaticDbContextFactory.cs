using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Videomatic.Infrastructure.Data.SqlServer;

public class SqlServerVideomaticDbContextFactory : IDbContextFactory<VideomaticDbContext>
{
    public static void Configure(DbContextOptionsBuilder builder, IConfiguration configuration)
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

    public SqlServerVideomaticDbContextFactory(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public IConfiguration Configuration { get; }    

    public VideomaticDbContext CreateDbContext()
    {
        DbContextOptionsBuilder optionsBuilder = new();
        Configure(optionsBuilder, Configuration);
        var options = optionsBuilder.Options;

        return new SqlServerVideomaticDbContext(options);
    }
}