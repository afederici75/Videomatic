using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests;

public abstract class VideosTestsBase : RepositoryTestsBase<Video>    
{
    public VideosTestsBase(RepositoryFixture<Video> fixture) 
        : base(fixture)
    {
        //Fixture.SkipDeletingDatabase = false;
    }

    #region Queries 

    [Theory(DisplayName = nameof(GetVideosDTOQuery_AllVideoDTOs))]
    [InlineData(null)]
    public virtual async Task GetVideosDTOQuery_AllVideoDTOs(
            [FromServices] ISender sender)
    {
        var cmd = new GetVideosQuery();
        var response = await sender.Send(cmd);
        Fixture.Output.WriteLine(JsonHelper.Serialize(response));

        // Should be all videos
        response.Items.Should().HaveCount(YouTubeVideos.HintsCount);

        var lastId = 0;
        foreach (var item in response.Items)
        {
            // Check they are in sequence by Id
            item.Id.Should().BeGreaterThan(lastId);
            lastId = item.Id;

            // Check they have basic properties
            item.Title.Should().NotBeNullOrWhiteSpace();    
            item.Description.Should().NotBeNullOrWhiteSpace();
            item.ProviderId.Should().NotBeNullOrWhiteSpace();
            item.VideoUrl.Should().NotBeNullOrWhiteSpace();
            item.ProviderId.Should().NotBeNullOrWhiteSpace();            

            // Check they don't include anything
            item.Artifacts.Should().BeEmpty();
            item.Thumbnails.Should().BeEmpty();
            item.Transcripts.Should().BeEmpty();            
        }
    }

    [Theory(DisplayName = nameof(GetVideosDTOQuery_Only2BVideosFromHttp))]
    [InlineData(null)]
    public virtual async Task GetVideosDTOQuery_Only2BVideosFromHttp(
            [FromServices] ISender sender)
    {
        var cmd = new GetVideosQuery(
            ProviderVideoIdPrefix: "B",
            VideoUrlPrefix: "http"
            );
        
        var response = await sender.Send(cmd);
        Fixture.Output.WriteLine(JsonHelper.Serialize(response));

        // Should be all videos
        response.Items.Should().HaveCount(2); // 2 videos: BBd3aHnVnuE and BFfb2P5wxC0        
    }

    [Theory(DisplayName = nameof(GetTranscriptDTOQuery_Aldous))]
    [InlineData(null)]
    public virtual async Task GetTranscriptDTOQuery_Aldous(
            [FromServices] ISender sender)
    {
        var getVideosQry = new GetVideosQuery(Take: 1, Includes: new[] { nameof(Video.Transcripts) } );
        QueryResponse<GetVideosResult> firstVideo = await sender.Send(getVideosQry);

        var getTranscriptQry = new GetTranscriptQuery(
            TranscriptId: firstVideo.Items!.First()!.Transcripts!.First().Id
            );

        var transcript = await sender.Send(getTranscriptQry);
        transcript.LineCount.Should().BeGreaterThan(0);

        Fixture.Output.WriteLine(JsonHelper.Serialize(transcript));
    }

    [Theory(DisplayName = nameof(ImportVideoAndPersistToRepository))]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.RickAstley_NeverGonnaGiveYouUp}", null)]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.AldousHuxley_DancingShiva}", null)]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism}", null)]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.HyonGakSunim_WhatIsZen}", null)]
    public async Task ImportVideoAndPersistToRepository(
       string url,
       [FromServices] IVideoImporter importer)
    {
        // Imports 
        Video video = await importer.ImportAsync(new Uri(url));

        video.Transcripts.Should().HaveCountGreaterThan(0);
        video.Transcripts.First().Lines.Should().HaveCountGreaterThan(0);
        video.Thumbnails.Should().HaveCountGreaterThan(0);

        // Persists
        throw new NotImplementedException();
        //await Fixture.Repository.UpdateRangeAsync(new[] { video }); // Will add a new record
        //await Fixture.Repository.SaveChangesAsync();        

        //// Now reads
        //video.Id.Should().BeGreaterThan(0);

        ////var record = await Fixture.DbContext.Videos.FirstAsync(v => v.Id == video.Id);

        //var record = await Fixture.Repository.GetByIdAsync(video.Id);

        //record.Should().NotBeNull();
        //record!.Id.Should().Be(video.Id);
        //record!.Title.Should().Be(video.Title);
        //record!.Description.Should().Be(video.Description);

        ////record!.Thumbnails.Should().BeEquivalentTo(video.Thumbnails);
        ////record!.Transcripts.Should().BeEquivalentTo(video.Transcripts);
        ////Fixture.DbContext.Entry(video).State = EntityState.Deleted;
        ////Fixture.DbContext.Remove(video);
        //await Fixture.Repository.DeleteRangeAsync(new[] { record });

        //record = await Fixture.Repository.GetByIdAsync(video.Id);
        //record.Should().BeNull();

    }

    #endregion

    #region Commands

    [Theory(DisplayName = nameof(ImportVideoCommandWorks))]
    [InlineData(null, null, null, YouTubeVideos.HyonGakSunim_WhatIsZen)]
    public virtual async Task ImportVideoCommandWorks(
            [FromServices] ISender sender,
            [FromServices] IRepository<Video> repository,
            [FromServices] IRepository<Video> repository2,
            string videoId)
    {
        //// Imports a video
        //string url = YouTubeVideos.GetUrl(videoId);
        //ImportVideoResponse response = await sender.Send(new ImportVideoCommand(-1, url));

        //// Verifies
        //response.Should().NotBeNull();
        //response.VideoId.Should().BeGreaterThan(0);

        //Video? dbVideo = await repository.GetByIdAsync(
        //    id: response.VideoId, 
        //    includes: new[]
        //    {
        //        nameof(Video.Artifacts),
        //        nameof(Video.Thumbnails),
        //        nameof(Video.Transcripts),
        //        nameof(Video.Transcripts)+'.'+nameof(Transcript.Lines),
        //    });

        //dbVideo!.Should().NotBeNull();
        //dbVideo!.Thumbnails.Count().Should().BeGreaterThan(0);
        //dbVideo!.Transcripts.Count().Should().BeGreaterThan(0);
        //dbVideo!.Artifacts.Count().Should().BeGreaterThan(0);

        //// Cleans up
        //await repository2.DeleteRangeAsync(new[] { dbVideo! });
        throw new NotImplementedException();
    }

    [Theory(DisplayName = "ImportVideoCommandWorksForAllVideos")]
    [InlineData(null, null)]
    public virtual async Task ImportVideoCommandWorksForAllVideos(
            [FromServices] ISender sender,
            [FromServices] IRepository<Video> repository)
    {
        throw new NotImplementedException();
        //var newIds = new HashSet<int>();
        //foreach (var videoId in YouTubeVideos.GetVideoIds())
        //{
        //    ImportVideoResponse response = await sender.Send(
        //        new ImportVideoCommand(-1, YouTubeVideos.GetUrl(videoId)));

        //    newIds.Add(response.VideoId).Should().BeTrue();
        //}

        //// Queries 
        //IEnumerable<Video> videos = await repository.ListAsync(new GetVideosSpecification(newIds.ToArray()));
        //videos.Should().HaveCount(newIds.Count);

        //await repository.DeleteRangeAsync(videos);
    }

    [Theory(DisplayName = nameof(DeleteVideoCommandWorksForAllVideos))]
    [InlineData(null)]
    public virtual async Task DeleteVideoCommandWorksForAllVideos(
            [FromServices] ISender sender)
    {
        throw new NotImplementedException();
        //// Imports 4 videos
        //var videoIds = YouTubeVideos.GetVideoIds();
        //var newIds = new HashSet<int>();
        //foreach (var videoId in videoIds)
        //{
        //    ImportVideoResponse response = await sender.Send(
        //        new ImportVideoCommand(-1,YouTubeVideos.GetUrl(videoId)));

        //    response.VideoId.Should().BeGreaterThan(0);
        //    newIds.Add(response.VideoId);
        //}

        //// Queries 
        //IEnumerable<Video> videos = await Fixture.Repository.ListAsync(new GetVideosSpecification(newIds.ToArray()));
        //videos.Should().HaveCount(newIds.Count);

        //// Deletes
        //foreach (var video in videos)
        //{
        //    DeleteVideoResponse response = await sender.Send(new DeleteVideoCommand(video.Id));
        //    response.Deleted.Should().BeTrue();
        //}
    }
    #endregion
}