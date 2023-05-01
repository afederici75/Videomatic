using Company.Videomatic.Infrastructure.Data.Sqlite;

namespace Company.Videomatic.Application.Tests.Sqlite;

public class SqliteVideoTests : VideosTestsBase
{
    public SqliteVideoTests(RepositoryFixture<Video> fixture)
        : base(fixture)
    {
    }
}