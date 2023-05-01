using Company.Videomatic.Infrastructure.Data.Sqlite;
using Company.Videomatic.Infrastructure.Data.Tests.Base;

namespace Company.Videomatic.Infrastructure.Data.Tests.Sqlite;

public class SqlLiteDbContextTests : DbContextTestsBase<SqliteVideomaticDbContext>,
    IClassFixture<DbContextFixture<SqliteVideomaticDbContext>>
{
    public SqlLiteDbContextTests(DbContextFixture<SqliteVideomaticDbContext> fixture)
        : base(fixture)
    {

    }
}
