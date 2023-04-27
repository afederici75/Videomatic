using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Company.SharedKernel.Queries;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Domain.Queries;
using System.Reflection;

namespace Company.Videomatic.Infrastructure.SqlServer.Tests;

[Collection("Sequence")]
public class GetVideosSpecificationTests : IClassFixture<VideomaticDbContextFixture>
{
    readonly VideomaticDbContextFixture _fixture;
    int[] _videoIds = new int[0] { };

    public GetVideosSpecificationTests(VideomaticDbContextFixture fixture)
    {
        _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        //_fixture.SkipDeletingDatabase();
    }    
   
    IQueryable<Video>  Videos => _fixture.DbContext.Videos;

    [Fact]
    public async Task GetVideosSpecificationPaged()
    {        
        var page1 = await Videos.ToListAsync(new GetVideosQuery(skip: 0, take: 3));        
        var page2 = await Videos.ToListAsync(new GetVideosQuery(skip: 3, take: 1));

        page1.Should().HaveCount(3);
        page2.Should().HaveCount(1);
        page1.Max(x => x.Id).Should().BeLessThan(page2.Max(y => y.Id));
    }

    [Fact]
    public async Task GetVideosByIds()
    {
        var res = await Videos.ToListAsync(new GetVideosQuery(ids: _videoIds));

        res.Should().HaveCount(_videoIds.Length);        
    }

    [Fact]
    public async Task GetVideosByTitleAndDescription()
    {
        var res = await Videos.ToListAsync(new GetVideosQuery(
                titlePrefix: "aldous",
                descriptionPrefix: "aldous",
                providerIdPrefix: "YOUtuBE"
                ));

        res.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetVideoByProviderVideoId()
    {
        var query = new GetVideoQuery(providerVideoId: YouTubeVideos.AldousHuxley_DancingShiva);

        // Using WithSpecification() extension method
        var res1 = await Videos.ToListAsync(query);
        res1.Should().HaveCount(1);
        
        // Using SingleAsync() extension method
        var res2 = await Videos.SingleAsync(query);
        res2.Should().BeEquivalentTo(res1.First()); 
    }
    
    [Fact]
    public async Task GetVideoByProviderVideoIdAndUrl()
    {
        var info = YouTubeVideos.GetInfoByVideoId(videoId: YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism);

        var res = await Videos.SingleAsync(new GetVideoQuery(            
            providerVideoId: info.ProviderVideoId,
            videoUrl: info.VideoUrl));
    }
}
