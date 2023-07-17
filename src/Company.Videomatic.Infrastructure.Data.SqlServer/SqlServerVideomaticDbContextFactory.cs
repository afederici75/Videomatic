using Microsoft.Extensions.DependencyInjection;

namespace Company.Videomatic.Infrastructure.Data.SqlServer;

public class SqlServerVideomaticDbContextFactory : IDbContextFactory<VideomaticDbContext>
{
    public SqlServerVideomaticDbContextFactory(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public IServiceProvider ServiceProvider { get; }

    public VideomaticDbContext CreateDbContext()
    {
        //var options = ServiceProvider.GetRequiredService<DbContextOptions<SqlServerVideomaticDbContext>>();
        return new SqlServerVideomaticDbContext();
    }
}