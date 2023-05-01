using Company.Videomatic.Infrastructure.Data.SqlServer;
using Company.Videomatic.Infrastructure.Data.Tests.Base;

namespace Company.Videomatic.Infrastructure.Data.Tests.SqlServer;

public class SqlServerGetVideosSpecificationTests : DbContextTestsBase<SqlServerVideomaticDbContext>,
    IClassFixture<DbContextFixture<SqlServerVideomaticDbContext>>
{
    public SqlServerGetVideosSpecificationTests(DbContextFixture<SqlServerVideomaticDbContext> fixture)
        : base(fixture)
    {

    }
}
