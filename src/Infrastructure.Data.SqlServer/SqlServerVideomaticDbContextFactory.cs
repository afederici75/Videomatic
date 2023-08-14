using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.SqlServer;

public class SqlServerVideomaticDbContextFactory : VideoMaticDbContextFactory<SqlServerVideomaticDbContext>
{   
    public SqlServerVideomaticDbContextFactory(IConfiguration configuration) 
        : base(configuration)
    {        
    }

    protected override void ConfigureContext(string connectionString, DbContextOptionsBuilder builder)
    {
        base.ConfigureContext(connectionString, builder);

        builder.UseSqlServer(connectionString, (opts) =>
        {
            opts.MigrationsAssembly(VideomaticConstants.MigrationAssemblyNamePrefix + SqlServerVideomaticDbContext.ProviderName);
        });        
    }
}