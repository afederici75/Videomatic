using Company.Videomatic.Infrastructure.Data.Sqlite;
using Company.Videomatic.Infrastructure.Data.SqlServer;

namespace Company.Videomatic.Application.Tests;

public class SqliteVideoTests : VideosTestsBase<SqliteVideomaticDbContext>
{
    public SqliteVideoTests(RepositoryFixture<SqliteVideomaticDbContext, Video> fixture) 
        : base(fixture)
    {
    }
}
