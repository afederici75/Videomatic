using FluentAssertions;
using Newtonsoft.Json;

namespace Company.Videomatic.Domain.Tests;

public class DomainTests
{
    [Fact]
    public void MockDataGeneratorCreatesRickAstleyVideoWithoutDetails()
    {
        var video = MockDataGenerator.CreateRickAstleyVideo(false, false);
        video.Transcripts.Count().Should().Be(0);
        video.Thumbnails.Count().Should().Be(0);

        video.ProviderId.Should().Be("YOUTUBE");
        video.VideoUrl.Should().Contain("youtube.com");
        video.Title.Should().Contain("Rick");
        video.Description.Should().Contain("2009");
    }

    [Fact]
    public void CanUpdateVideosProperties()
    {
        var video = MockDataGenerator.CreateRickAstleyVideo(false, false);
        // Test that the video title and description are updated
        
        const string Updated = "(Updated)";

        video.Title += Updated;
        video.Description += Updated;
        video.Title.Should().EndWith(Updated);
        video.Description.Should().EndWith(Updated);
    }

    [Fact]
    public void MockDataGeneratorCreatesRickAstleyVideoWithRightDetails()
    {
        var newVideo = MockDataGenerator.CreateRickAstleyVideo(true, true);
        newVideo.Thumbnails.Count().Should().Be(2);
        newVideo.Transcripts.Count().Should().Be(1);
    }

    [Fact]
    public void SerializesNicely()
    {
        var video = MockDataGenerator.CreateRickAstleyVideo(true, true);
        
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

        newVideo!.Thumbnails.Count().Should().Be(video.Thumbnails.Count());
        newVideo!.Transcripts.Count().Should().Be(video.Transcripts.Count());
        
        
    }
}