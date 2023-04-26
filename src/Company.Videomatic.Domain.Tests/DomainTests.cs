namespace Company.Videomatic.Domain.Tests;

public class DomainTests
{
    [Fact]
    public async Task CanUpdateVideosProperties()
    {
        var video = await VideoDataGenerator.CreateVideoFromFile(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp);
        // Test that the video title and description are updated

        const string Updated = "(Updated)";

        video.Title += Updated;
        video.Description += Updated;
        video.Title.Should().EndWith(Updated);
        video.Description.Should().EndWith(Updated);
    }

    [Fact]
    public void CreateVideoSyntaxWorks()
    {
        var video = new Video(
            providerId: "YOUTUBE",
            providerVideoId: "dQw4w9WgXcQ",
            videoUrl: "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
            title: "The title",
            description: "A description");

        // Verfies the video is created with no thumbnails, transcripts or artifacts
        video.Thumbnails.Should().HaveCount(0);
        video.Transcripts.Should().HaveCount(0);
        video.Artifacts.Should().HaveCount(0);
        
        // Verifies thumbnails work
        video.AddThumbnails(
            new Thumbnail(url: "https://i.ytimg.com/vi/dQw4w9WgXcQ/default.jpg", width: 120, height: 90),
            new Thumbnail(url: "https://i.ytimg.com/vi/dQw4w9WgXcQ/mqdefault.jpg", width: 320, height: 180));
        video.Thumbnails.Should().HaveCount(2);

        // Verifies transcripts work
        video.AddTranscripts(
            new Transcript("US").AddLines(
                new TranscriptLine(text: "First line", startsAt: TimeSpan.FromSeconds(0), duration: TimeSpan.FromSeconds(2)),
                new TranscriptLine(text: "Second line", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(3)),
                new TranscriptLine(text: "Third line", startsAt: TimeSpan.FromSeconds(5), duration: TimeSpan.FromSeconds(1))));
        video.Transcripts.Should().HaveCount(1);
        video.Transcripts.First().Lines.Should().HaveCount(3);

        // Verifies artifacts work
        video.AddArtifacts(
            new Artifact(title: "Artifact 1", text: "The first text"),
            new Artifact(title: "Artifact 2", text: "The second text")
            );
        video.Artifacts.Should().HaveCount(2);
        
    }
}