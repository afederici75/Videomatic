using Ardalis.Specification.EntityFrameworkCore;
using Company.Videomatic.Infrastructure.Data.Tests.Base;
using Microsoft.EntityFrameworkCore.Design;

namespace Company.Videomatic.Infrastructure.Data.Tests;

[Collection("Sequence")]
public abstract class GetVideosSpecificationTestsBase<TDbContext> : DbContextTestsBase<TDbContext>
    where TDbContext : VideomaticDbContext
{
    int[] _videoIds = new int[0] { };

    protected GetVideosSpecificationTestsBase(DbContextFixture<TDbContext> fixture) : base(fixture)
    {
    }

    IQueryable<Video>  Videos => Fixture.DbContext.Videos;

    [Fact]
    public async Task GetVideosSpecificationPaged()
    {        
        var page1 = await Videos.ToListAsync(new GetVideosSpecification(skip: 0, take: 3));
        var page2 = await Videos.ToListAsync(new GetVideosSpecification(skip: 3, take: 1));

        page1.Should().HaveCount(3);
        page2.Should().HaveCount(1);

        page1.Max(x => x.Id).Should().BeLessThan(page2.Max(y => y.Id));

        var allPages = page1.Concat(page2).ToList();
        allPages.ForEach(x => x.Thumbnails.Should().BeEmpty());
        allPages.ForEach(x => x.Artifacts.Should().BeEmpty());
        allPages.ForEach(x => x.Transcripts.Should().BeEmpty());        
    }

    [Fact]
    public async Task GetVideosPagedWithJustThumbnails()
    {
        string[] includes = new [] { 
            nameof(Video.Thumbnails)
        };        

        var page1 = await Videos.ToListAsync(new GetVideosSpecification(skip: 0, take: 3, includes: includes));
        var page2 = await Videos.ToListAsync(new GetVideosSpecification(skip: 3, take: 1, includes: includes));

        page1.Should().HaveCount(3);
        page1.ForEach(x => x.Thumbnails.Should().HaveCountGreaterThan(0));
        page1.ForEach(x => x.Artifacts.Should().BeEmpty());
        page1.ForEach(x => x.Transcripts.Should().BeEmpty());

        page2.Should().HaveCount(1);
        page2.ForEach(x => x.Thumbnails.Should().HaveCountGreaterThan(0));
        page1.ForEach(x => x.Artifacts.Should().BeEmpty());
        page1.ForEach(x => x.Transcripts.Should().BeEmpty());

        page1.Max(x => x.Id).Should().BeLessThan(page2.Max(y => y.Id));
    }

    [Fact]
    public async Task GetVideosByIds()
    {
        var query = new GetVideosSpecification(ids: _videoIds);

        var res = await Videos.ToListAsync(query);

        res.Should().HaveCount(_videoIds.Length);        
    }

    [Fact]
    public async Task GetVideosByTitleAndDescription()
    {
        var query = new GetVideosSpecification(
            take: 10, 
            titlePrefix: "aldous",
            descriptionPrefix: "aldous",
            providerIdPrefix: "YOUtuBE");

        var res = await Videos.ToListAsync(query);

        res.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetVideoByProviderVideoId()
    {
        var query = new GetVideoSpecification(providerVideoId: YouTubeVideos.AldousHuxley_DancingShiva);

        // Using WithSpecification() extension method
        var res1 = await Videos.WithSpecification(query).ToListAsync();
        res1.Should().HaveCount(1);
        
        // Using SingleAsync() extension method
        var res2 = await Videos.SingleAsync(query);
        res2.Should().BeEquivalentTo(res1.First()); 
    }
    
    [Fact]
    public async Task GetVideoByProviderVideoIdAndUrl()
    {
        var info = YouTubeVideos.GetInfoByVideoId(videoId: YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism);

        var res = await Videos.SingleAsync(new GetVideoSpecification(            
            providerVideoId: info.ProviderVideoId,
            videoUrl: info.VideoUrl));
    }
}
