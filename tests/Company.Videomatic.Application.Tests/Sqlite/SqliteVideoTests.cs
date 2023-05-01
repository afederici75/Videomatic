using Company.Videomatic.Infrastructure.Data.Sqlite;

namespace Company.Videomatic.Application.Tests.Sqlite;

public class SqliteVideoTests : VideosTestsBase<SqliteVideomaticDbContext>
{
    public SqliteVideoTests(RepositoryFixture<SqliteVideomaticDbContext, Video> fixture)
        : base(fixture)
    {
    }
}