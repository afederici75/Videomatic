using FluentAssertions;

namespace Company.Videomatic.Domain.Tests;

public class DomainTests
{
    [Fact]
    public void MockDataGeneratorCreatesRickAstleyVideoWithoutDetails()
    {
        var newVideo = MockDataGenerator.CreateRickAstleyVideo(false, false);
        newVideo.Transcripts.Count.Should().Be(0);
        newVideo.Thumbnails.Count.Should().Be(0);

        newVideo.ProviderId.Should().Be("YOUTUBE");
        newVideo.VideoUrl.Should().Contain("youtube.com");
        newVideo.Title.Should().Contain("Rick");
        newVideo.Description.Should().Contain("2009");
    }

    [Fact]
    public void CanUpdateVideosProperties()
    {
        var newVideo = MockDataGenerator.CreateRickAstleyVideo(false, false);
        // Test that the video title and description are updated
        
        const string Updated = "(Updated)";

        newVideo.Title += Updated;
        newVideo.Description += Updated;
        newVideo.Title.Should().EndWith(Updated);
        newVideo.Description.Should().EndWith(Updated);
    }

    [Fact]
    public void MockDataGeneratorCreatesRickAstleyVideoWithRightDetails()
    {
        var newVideo = MockDataGenerator.CreateRickAstleyVideo(true, true);
        newVideo.Thumbnails.Count.Should().Be(2);
        newVideo.Transcripts.Count.Should().Be(1);
    }
}