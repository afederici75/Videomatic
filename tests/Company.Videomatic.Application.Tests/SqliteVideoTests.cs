using Company.Videomatic.Infrastructure.Data.Sqlite;

namespace Company.Videomatic.Application.Tests;

public class SqliteVideoTests : VideosTestsBase<SqliteVideomaticDbContext>
{
    public SqliteVideoTests(RepositoryFixture<SqliteVideomaticDbContext, Video> fixture) 
        : base(fixture)
    {
    }
}
