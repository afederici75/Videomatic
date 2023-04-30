using Company.Videomatic.Infrastructure.Data.Sqlite;

namespace Company.Videomatic.Infrastructure.Data.Tests;

public class SqlLiteDbContextTests : DbContextTests<SqliteVideomaticDbContext>, 
    IClassFixture<DbContextFixture<SqliteVideomaticDbContext>>
{
    public SqlLiteDbContextTests(DbContextFixture<SqliteVideomaticDbContext> fixture)
        : base(fixture)
    {

    }
}
