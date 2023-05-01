using Company.Videomatic.Infrastructure.Data;
using Company.Videomatic.Infrastructure.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public const string SqliteProviderName = "Sqlite";

    static void Configure(IServiceProvider sp, DbContextOptionsBuilder builder, IConfiguration configuration)
    {
        var connectionName = $"{VideomaticConstants.Videomatic}.Sqlite";
        var connString = configuration.GetConnectionString(connectionName);
        if (string.IsNullOrWhiteSpace(connString))
        {
            var logger = sp.GetRequiredService<ILogger<IConfiguration>>();
            logger.LogWarning("Configuration '{ConnectionName}' is missing. Using default configuration.", connectionName);

            // https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/connection-strings
            // https://github.com/dotnet/efcore/issues/9842 // Why I cannot use in memory in Videomatic
            connString = $"Data Source={VideomaticConstants.Videomatic}.db;Cache=Shared";
        }

        builder.EnableSensitiveDataLogging()
               .UseSqlite(connString, (opts) => {
                   opts.MigrationsAssembly(VideomaticConstants.MigrationAssemblyNamePrefix + SqliteVideomaticDbContext.ProviderName);
               });
    }

    public static IServiceCollection AddVideomaticDataForSqlite(
        this IServiceCollection services, 
        IConfiguration configuration)
        => services.AddDbContext<VideomaticDbContext>((sp, builder) => Configure(sp, builder, configuration));
   
    public static IServiceCollection AddVideomaticSqliteDbContextForTests(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddDbContext<VideomaticDbContext>((sp, builder) => Configure(sp, builder, configuration));

}