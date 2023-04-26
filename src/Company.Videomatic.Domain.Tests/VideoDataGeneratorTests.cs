using FluentAssertions;
using Newtonsoft.Json;

namespace Company.Videomatic.Domain.Tests;

public class VideoDataGeneratorTests
{
    [Fact]
    public async Task DataGeneratorReturnsRickAstleyVideoWithoutDetails()
    {
        var video = await VideoDataGenerator.LoadVideoFromFileAsync(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp);
        video.Transcripts.Count().Should().Be(0);
        video.Thumbnails.Count().Should().Be(0);

        video.ProviderId.Should().Be("YOUTUBE");
        video.VideoUrl.Should().Contain("youtube.com");
        video.Title.Should().Contain("Rick");
        video.Description.Should().Contain("#RickAstley");
        video.Description.Should().Contain("#NeverGonnaGiveYouUp");
        video.Description.Should().Contain("#WheneverYouNeedSomebody");
        video.Description.Should().Contain("#OfficialMusicVideo");
    }

    [Fact]
    public async Task MockDataGeneratorCreatesRickAstleyVideoWithRightDetails()
    {
        var video = await VideoDataGenerator.LoadVideoFromFileAsync(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp,
            nameof(Video.Thumbnails),
            nameof(Video.Transcripts),
            nameof(Video.Artifacts));
        video.Thumbnails.Count().Should().Be(5);
        video.Transcripts.Count().Should().Be(1);
        video.Artifacts.Count().Should().Be(2);        
    }

    [Fact]
    public async Task SerializesProperlyWithJSONConver()
    {
        var video = await VideoDataGenerator.LoadVideoFromFileAsync(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp,
            nameof(Video.Transcripts),
            nameof(Video.Artifacts));

        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        var json = JsonConvert.SerializeObject(video, settings);

        json.Should().Contain("Rick Astley - Never Gonna Give You Up"); 
        
        var newVideo = JsonConvert.DeserializeObject<Video>(json, settings); 

        newVideo.Should().NotBeNull();
        
        newVideo!.ProviderId.Should().Be(video.ProviderId);
        newVideo!.VideoUrl.Should().Be(video.VideoUrl);
        newVideo!.Title.Should().Be(video.Title);   
        newVideo!.Description.Should().Be(video.Description);

        var newJson = JsonConvert.SerializeObject(newVideo, settings);
        newJson.Should().Be(json);
    }
}