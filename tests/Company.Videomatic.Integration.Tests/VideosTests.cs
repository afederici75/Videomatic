//using Company.Videomatic.Infrastructure.Data;
//using Company.Videomatic.Infrastructure.Data.Sqlite;
//using Company.Videomatic.Infrastructure.Data.SqlServer;

//namespace Company.Videomatic.Integration.Tests;

//public class SqlServerDbContextTests : VideosTests<SqlServerVideomaticDbContext>,
//    IClassFixture<VideomaticDbContextFixture<SqlServerVideomaticDbContext>>
//{
//    public SqlServerDbContextTests(VideomaticDbContextFixture<SqlServerVideomaticDbContext> fixture)
//        : base(fixture)
//    {

//    }
//}

//public class SqlSiteDbContextTests : VideosTests<SqliteVideomaticDbContext>,
//    IClassFixture<VideomaticDbContextFixture<SqliteVideomaticDbContext>>
//{
//    public SqlSiteDbContextTests(VideomaticDbContextFixture<SqliteVideomaticDbContext> fixture)
//        : base(fixture)
//    {

//    }
//}

//[Collection("Videos")]
//public abstract class VideosTests<TDbContext> : VideosTests
//    where TDbContext : VideomaticDbContext
//{
//    public VideosTests(VideomaticDbContextFixture<TDbContext> videomaticDbContextFixture, ITestOutputHelper output)
//        : base(videomaticDbContextFixture, output)
//    {
//        Fixture.SkipDeletingDatabase = true;
//    }

//    [Theory()]
//    [InlineData(null, null)]
//    public override Task DeleteVideoCommandWorksForAllVideos([FromServices] ISender sender, [FromServices] IRepositoryBase<Video> repository)
//    {
//        return base.DeleteVideoCommandWorksForAllVideos(sender, repository);
//    }

//    [Theory()]
//    [InlineData(null, null, null, YouTubeVideos.HyonGakSunim_WhatIsZen)]

//    public override Task ImportVideoCommandWorks([FromServices] ISender sender, [FromServices] IRepositoryBase<Video> repository, [FromServices] IRepositoryBase<Video> repository2, string videoId)
//    {
//        return base.ImportVideoCommandWorks(sender, repository, repository2, videoId);
//    }

//    [Theory()]
//    [InlineData(null, null)]

//    public override Task ImportVideoCommandWorksForAllVideos([FromServices] ISender sender, [FromServices] IRepositoryBase<Video> repository)
//    {
//        return base.ImportVideoCommandWorksForAllVideos(sender, repository);
//    }
//}
