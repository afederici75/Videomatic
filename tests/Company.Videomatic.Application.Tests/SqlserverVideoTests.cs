using Company.Videomatic.Infrastructure.Data.Sqlite;
using Company.Videomatic.Infrastructure.Data.SqlServer;

namespace Company.Videomatic.Application.Tests;

public class SqlserverVideoTests : VideosTestsBase<SqlServerVideomaticDbContext>
{
    public SqlserverVideoTests(RepositoryFixture<SqlServerVideomaticDbContext, Video> fixture) 
        : base(fixture)
    {
    }
}
