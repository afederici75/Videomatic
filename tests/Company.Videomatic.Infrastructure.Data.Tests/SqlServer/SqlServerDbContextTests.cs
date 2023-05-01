using Company.Videomatic.Infrastructure.Data.SqlServer;
using Company.Videomatic.Infrastructure.Data.Tests.Base;

namespace Company.Videomatic.Infrastructure.Data.Tests.SqlServer;

public class SqlServerDbContextTests : DbContextTestsBase<SqlServerVideomaticDbContext>,
    IClassFixture<DbContextFixture<SqlServerVideomaticDbContext>>
{
    public SqlServerDbContextTests(DbContextFixture<SqlServerVideomaticDbContext> fixture)
        : base(fixture)
    {

    }
}
