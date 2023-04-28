using Company.SharedKernel.Specifications;

namespace Company.Videomatic.Application.Tests.Features.Videos;

[Collection("Sequence")]
public class ApplicationTests
{        
    [Theory]
    [InlineData(null, null, null, YouTubeVideos.HyonGakSunim_WhatIsZen)]
    public virtual async Task ImportVideoCommandWorks(
            [FromServices] ISender sender,
            [FromServices] IRepository<Video> repository,
            [FromServices] IRepository<Video> repository2,
            string videoId)
    {
        // Imports a video
        string url = YouTubeVideos.GetUrl(videoId);
        ImportVideoResponse response = await sender.Send(new ImportVideoCommand(url));

        // Verifies
        response.Should().NotBeNull();
        response.VideoId.Should().BeGreaterThan(0);
        response.ThumbNailCount.Should().BeGreaterThan(0);
        response.ArtifactsCount.Should().BeGreaterThan(0);
        response.ArtifactsCount.Should().BeGreaterThan(0);

        Video? video2 = await repository.FirstOrDefaultAsync(
            new GetOneSpecification<Video>(response.VideoId, new[] 
            { 
                nameof(Video.Artifacts),
                nameof(Video.Thumbnails),
                nameof(Video.Transcripts),
                nameof(Video.Transcripts)+'.'+nameof(Transcript.Lines),
            }));

        video2.Should().NotBeNull();    
        response.VideoId.Should().Be(response.VideoId);
        
        // Cleans up
        await repository2.DeleteAsync(video2!);
    }

    [Theory]
    [InlineData(null, null)]
    public virtual async Task ImportVideoCommandWorksForAllVideos(
            [FromServices] ISender sender,
            [FromServices] IRepository<Video> repository)
    {
        var newIds = new HashSet<int>();        
        foreach (var videoId in YouTubeVideos.GetVideoIds())
        {            
            ImportVideoResponse response = await sender.Send(
                new ImportVideoCommand(YouTubeVideos.GetUrl(videoId)));

            newIds.Add(response.VideoId).Should().BeTrue();                        
        }

        // Queries 
        IEnumerable<Video> videos = await repository.ListAsync(new GetVideosQuery(newIds.ToArray()));
        videos.Should().HaveCount(newIds.Count);

        await repository.DeleteRangeAsync(videos);        
    }

    [Theory]
    [InlineData(null, null)]
    public virtual async Task DeleteVideoCommandWorksForAllVideos(
            [FromServices] ISender sender,
            [FromServices] IRepository<Video> repository)
    {        
        // Imports 4 videos
        var videoIds = YouTubeVideos.GetVideoIds();
        var newIds = new HashSet<int>();
        foreach (var videoId in videoIds)
        {
            ImportVideoResponse response = await sender.Send(
                new ImportVideoCommand(YouTubeVideos.GetUrl(videoId)));

            response.VideoId.Should().BeGreaterThan(0);
            newIds.Add(response.VideoId);
        }

        // Queries 
        IEnumerable<Video> videos = await repository.ListAsync(new GetVideosQuery(newIds.ToArray()));        
        videos.Should().HaveCount(newIds.Count);

        // Deletes
        foreach (var video in videos)
        {
            DeleteVideoResponse response = await sender.Send(new DeleteVideoCommand(video.Id));
            response.Deleted.Should().BeTrue(); 
        }        
    }
}
