using Company.Videomatic.Infrastructure.Data.Sqlite;
using Company.Videomatic.Infrastructure.Data.Tests.Base;

namespace Company.Videomatic.Infrastructure.Data.Tests.Sqlite;

public class SqlLiteDbContextTests : DbContextTestsBase,
    IClassFixture<DbContextFixture>
{
    public SqlLiteDbContextTests(DbContextFixture fixture)
        : base(fixture)
    {

    }
}
