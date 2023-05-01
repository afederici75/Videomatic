using Company.Videomatic.Infrastructure.Data.SqlServer;
using Company.Videomatic.Infrastructure.Data.Tests.Base;

namespace Company.Videomatic.Infrastructure.Data.Tests.SqlServer;

public class SqlServerDbContextTests : DbContextTestsBase,
    IClassFixture<DbContextFixture>
{
    public SqlServerDbContextTests(DbContextFixture fixture)
        : base(fixture)
    {

    }
}
