namespace Company.Videomatic.Infrastructure.Data.SqlServer;

public class SqlServerVideomaticDbContext : VideomaticDbContext
{
    public const string ProviderName = "SqlServer";

    public SqlServerVideomaticDbContext(DbContextOptions options) : base(options)
    {
    }
}
