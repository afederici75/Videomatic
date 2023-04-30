using Company.Videomatic.Infrastructure.Data.SqlServer;

namespace Company.Videomatic.Infrastructure.Data.Tests;

public class SqlServerDbContextTests : DbContextTests<SqlServerVideomaticDbContext>, 
    IClassFixture<DbContextFixture<SqlServerVideomaticDbContext>>
{
    public SqlServerDbContextTests(DbContextFixture<SqlServerVideomaticDbContext> fixture)
        : base(fixture)
    {
        
    }
}
