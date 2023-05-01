using Company.Videomatic.Infrastructure.Data;
using Company.Videomatic.Infrastructure.Data.SqlServer;
using Company.Videomatic.Infrastructure.SqlServer;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    static void Configure(IServiceProvider sp, DbContextOptionsBuilder builder, IConfiguration configuration)
    {
        var connectionName = $"{VideomaticConstants.Videomatic}.SqlServer";
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
    
    public static IServiceCollection AddVideomaticDataForSqlServer(
        this IServiceCollection services, 
        IConfiguration configuration)
        => services.AddDbContext<SqlServerVideomaticDbContext>((sp, builder) => Configure(sp, builder, configuration));

    public static IServiceCollection AddVideomaticSqlServerDbContextForTests(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddDbContext<SqlServerVideomaticDbContext>((sp, builder) => Configure(sp, builder, configuration));
}