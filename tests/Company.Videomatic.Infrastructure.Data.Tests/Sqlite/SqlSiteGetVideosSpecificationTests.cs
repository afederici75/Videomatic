using Company.Videomatic.Infrastructure.Data.Sqlite;
using Company.Videomatic.Infrastructure.Data.Tests.Base;

namespace Company.Videomatic.Infrastructure.Data.Tests.Sqlite;

public class SqlSiteGetVideosSpecificationTests : DbContextTestsBase<SqliteVideomaticDbContext>,
    IClassFixture<DbContextFixture<SqliteVideomaticDbContext>>
{
    public SqlSiteGetVideosSpecificationTests(DbContextFixture<SqliteVideomaticDbContext> fixture)
        : base(fixture)
    {

    }
}
