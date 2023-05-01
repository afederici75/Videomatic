using Company.Videomatic.Infrastructure.Data.SqlServer;
using Company.Videomatic.Infrastructure.Data.Tests.Base;

namespace Company.Videomatic.Infrastructure.Data.Tests.SqlServer;

public class SqlServerGetVideosSpecificationTests : DbContextTestsBase,
    IClassFixture<DbContextFixture>
{
    public SqlServerGetVideosSpecificationTests(DbContextFixture fixture)
        : base(fixture)
    {

    }
}
