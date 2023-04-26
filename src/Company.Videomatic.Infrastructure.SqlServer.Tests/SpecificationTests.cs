using Ardalis.Specification.EntityFrameworkCore;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Domain.Specifications;
using System.Reflection;

namespace Company.Videomatic.Infrastructure.SqlServer.Tests;

public class SpecificationTests : IClassFixture<VideomaticDbContextFixture>, IAsyncLifetime
{
    readonly VideomaticDbContextFixture _fixture;
    int[] _videoIds = new int[0] { };

    public SpecificationTests(VideomaticDbContextFixture fixture)
    {
        _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        _fixture.SkipDeletingDatabase();
    }    

    public async Task InitializeAsync()
    {
        _videoIds = await CommitAllYouTubeVideos(_fixture.DbContext);
    }

    public Task DisposeAsync() => Task.CompletedTask;

    async Task<int[]> CommitAllYouTubeVideos(VideomaticDbContext db)
    {
        string[] videoIds = YouTubeVideos.GetVideoIds();
        Task<Video>[] tasks = videoIds
            .Select(v => VideoDataGenerator.CreateVideoFromFileAsync(v, includeAll: true))
            .ToArray();

        Video[] videos = await Task.WhenAll(tasks);

        db.AddRange(videos);
        await db.SaveChangesAsync();

        var ids = videos.Select(v => v.Id).ToArray();
        return ids;
    }

    [Theory]
    [InlineData(null)]
    public async Task GetVideosSpecificationPaged([FromServices] VideomaticDbContext db)
    {        
        var page1 = await db.Videos
            .WithSpecification(new GetVideosSpecification(
                skip: 0,
                take: 3))
            .ToListAsync();

        page1.Should().HaveCount(3);

        var page2 = await db.Videos
            .WithSpecification(new GetVideosSpecification(
                skip: 3,
                take: 1))
            .ToListAsync();
        page2.Should().HaveCount(1);
    }

    [Theory]
    [InlineData(null)]
    public async Task GetVideosByIds([FromServices] VideomaticDbContext db)
    {
        var res = await db.Videos
            .WithSpecification(new GetVideosSpecification(
                ids: _videoIds))
            .ToListAsync();

        res.Should().HaveCount(_videoIds.Length);        
    }

    [Theory]
    [InlineData(null)]
    public async Task GetVideosByTitleAndDescription([FromServices] VideomaticDbContext db)
    {
        var res = await db.Videos
            .WithSpecification(new GetVideosSpecification(
                titlePrefix: "aldous",
                descriptionPrefix: "aldous"
                ))
            .ToListAsync();

        res.Should().HaveCount(1);
    }

}
