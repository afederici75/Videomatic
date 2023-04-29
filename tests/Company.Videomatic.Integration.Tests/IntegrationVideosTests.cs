namespace Company.Videomatic.Integration.Tests;

[Collection("Sequence")]
public class IntegrationVideosTests : VideosTests, IClassFixture<VideomaticDbContextFixture>
{
    public IntegrationVideosTests(VideomaticDbContextFixture videomaticDbContextFixture, ITestOutputHelper output)
        : base(videomaticDbContextFixture, output)
    {
        Fixture.SkipDeletingDatabase = true;
    }

    [Theory()]
    [InlineData(null, null)]
    public override Task X_DeleteVideoCommandWorksForAllVideos([FromServices] ISender sender, [FromServices] IRepositoryBase<Video> repository)
    {
        return base.X_DeleteVideoCommandWorksForAllVideos(sender, repository);
    }

    [Theory()]
    [InlineData(null, null, null, YouTubeVideos.HyonGakSunim_WhatIsZen)]

    public override Task ImportVideoCommandWorks([FromServices] ISender sender, [FromServices] IRepositoryBase<Video> repository, [FromServices] IRepositoryBase<Video> repository2, string videoId)
    {
        return base.ImportVideoCommandWorks(sender, repository, repository2, videoId);
    }

    [Theory()]
    [InlineData(null, null)]

    public override Task ImportVideoCommandWorksForAllVideos([FromServices] ISender sender, [FromServices] IRepositoryBase<Video> repository)
    {
        return base.ImportVideoCommandWorksForAllVideos(sender, repository);
    }
}
