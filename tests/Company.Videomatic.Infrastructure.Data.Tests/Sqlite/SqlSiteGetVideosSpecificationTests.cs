using Company.Videomatic.Infrastructure.Data.Sqlite;
using Company.Videomatic.Infrastructure.Data.Tests.Base;

namespace Company.Videomatic.Infrastructure.Data.Tests.Sqlite;

public class SqlSiteGetVideosSpecificationTests : DbContextTestsBase,
    IClassFixture<DbContextFixture>
{
    public SqlSiteGetVideosSpecificationTests(DbContextFixture fixture)
        : base(fixture)
    {

    }
}
