using FluentAssertions;
using Newtonsoft.Json;

namespace Company.Videomatic.Domain.Tests;

public class DomainTests
{
    [Fact]
    public async Task MockDataGeneratorCreatesRickAstleyVideoWithoutDetails()
    {
        var video = await VideoDataGenerator.CreateRickAstleyVideo();
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
    public async Task CanUpdateVideosProperties()
    {
        var video = await VideoDataGenerator.CreateRickAstleyVideo();
        // Test that the video title and description are updated
        
        const string Updated = "(Updated)";

        video.Title += Updated;
        video.Description += Updated;
        video.Title.Should().EndWith(Updated);
        video.Description.Should().EndWith(Updated);
    }

    [Fact]
    public async Task MockDataGeneratorCreatesRickAstleyVideoWithRightDetails()
    {
        var newVideo = await VideoDataGenerator.CreateRickAstleyVideo(
            nameof(Video.Thumbnails),
            nameof(Video.Transcripts),
            nameof(Video.Artifacts));
        newVideo.Thumbnails.Count().Should().Be(5);
        newVideo.Transcripts.Count().Should().Be(1);
        newVideo.Artifacts.Count().Should().Be(2);        
    }

    [Fact]
    public async Task SerializesProperlyWithJSONConver()
    {
        var video = await VideoDataGenerator.CreateRickAstleyVideo(nameof(Video.Thumbnails),
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