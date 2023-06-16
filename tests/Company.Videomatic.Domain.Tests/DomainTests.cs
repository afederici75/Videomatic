namespace Company.Videomatic.Domain.Tests;

public class DomainTests
{
    [Fact]
    public async Task CanUpdateVideosProperties()
    {
        var video = await VideoDataGenerator.CreateVideoFromFileAsync(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp);
        // Test that the video title and description are updated

        const string Updated = "(Updated)";

        video.UpdateTitle(Updated);
        video.UpdateDescription(Updated);
        video.Title.Should().EndWith(Updated);
        video.Description.Should().EndWith(Updated);
    }

    [Fact]
    public void CreateVideoSyntaxWorks()
    {
        var video = new Video(
            //providerId: "YOUTUBE",
            //providerVideoId: "dQw4w9WgXcQ",
            location: "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
            title: "The title",
            description: "A description");

        // Verfies the video is created with no thumbnails, transcripts or artifacts
        video.Thumbnails.Should().HaveCount(0);
        video.Transcripts.Should().HaveCount(0);
        video.Artifacts.Should().HaveCount(0);
        
        // Verifies thumbnails work
        video.AddThumbnail(new (location: "https://i.ytimg.com/vi/dQw4w9WgXcQ/default.jpg", resolution: ThumbnailResolution.High,  width: 120, height: 90))
            .AddThumbnail(new (location: "https://i.ytimg.com/vi/dQw4w9WgXcQ/mqdefault.jpg", resolution: ThumbnailResolution.Standard, width: 320, height: 180));
        video.Thumbnails.Should().HaveCount(2);

        // Verifies transcripts work
        video.AddTranscript(
            new Transcript("US").AddLine(new TranscriptLine(text: "First line", startsAt: TimeSpan.FromSeconds(0), duration: TimeSpan.FromSeconds(2)))
                                .AddLine(new TranscriptLine(text: "Second line", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(3)))
                                .AddLine(new TranscriptLine(text: "Third line", startsAt: TimeSpan.FromSeconds(5), duration: TimeSpan.FromSeconds(1))));
        video.Transcripts.Should().HaveCount(1);
        video.Transcripts.First().Lines.Should().HaveCount(3);

        // Verifies artifacts work
        video.AddArtifact(new Artifact(title: "Artifact 1", type:"AI", text: "The first text"))
             .AddArtifact(new Artifact(title: "Artifact 2", type:"AI", text: "The second text"));
        video.Artifacts.Should().HaveCount(2);
        
    }
}