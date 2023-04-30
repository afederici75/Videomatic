using Company.Videomatic.Infrastructure.Data.Sqlite;

namespace Company.Videomatic.Infrastructure.Data.Tests;

public class SqlSiteDbContextTests : DbContextTests<SqliteVideomaticDbContext>, 
    IClassFixture<DbContextFixture<SqliteVideomaticDbContext>>
{
    public SqlSiteDbContextTests(DbContextFixture<SqliteVideomaticDbContext> fixture)
        : base(fixture)
    {

    }
}
