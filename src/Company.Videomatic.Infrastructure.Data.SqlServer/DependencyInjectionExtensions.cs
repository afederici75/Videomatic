using Company.Videomatic.Infrastructure.Data;
using Company.Videomatic.Infrastructure.Data.SqlServer;
using Company.Videomatic.Infrastructure.SqlServer;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVideomaticDataForSqlServer(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<VideomaticDbContext, SqlServerVideomaticDbContext>(builder =>
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
        });

        return services;
    }

    public static IServiceCollection AddVideomaticSqlServerDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SqlServerVideomaticDbContext>(builder =>
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
        });

        return services;
    }
}