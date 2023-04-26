namespace Company.Videomatic.Domain.Tests;

public class DomainTests
{
    [Fact]
    public async Task CanUpdateVideosProperties()
    {
        var video = await VideoDataGenerator.LoadVideoFromFileAsync(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp);
        // Test that the video title and description are updated

        const string Updated = "(Updated)";

        video.Title += Updated;
        video.Description += Updated;
        video.Title.Should().EndWith(Updated);
        video.Description.Should().EndWith(Updated);
    }
}