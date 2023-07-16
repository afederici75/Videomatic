using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Video;
using Company.Videomatic.Infrastructure.YouTube;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Xunit.Abstractions;

namespace Infrastructure.Tests.YouTube;

[Collection("DbContextTests")]
public class YouTubePlaylistsHelperTests : IClassFixture<DbContextFixture>
{
    public YouTubePlaylistsHelperTests(ITestOutputHelper output,
        DbContextFixture fixture,
        IRepository<Artifact> repository,
        ISender sender,
        IYouTubeHelper helper)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        Helper = helper ?? throw new ArgumentNullException(nameof(helper));

        Fixture.SkipDeletingDatabase = true;
    }

    public ITestOutputHelper Output { get; }
    public DbContextFixture Fixture { get; }
    public IRepository<Artifact> Repository { get; }
    public ISender Sender { get; }
    public IYouTubeHelper Helper { get; }

    [Theory]
    [InlineData("PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb")] // Alternative Living, Sustainable Future
    [InlineData("PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu")] // Google I/O Keynote Films
    public async Task ImportVideosOfPlaylist(string playlistId)
    {        
        await foreach (var item in Helper.ImportVideosOfPlaylist(playlistId))
        {
            Output.WriteLine($"[{item.Id}]: {item.Name}");

            item.Id.Should().BeNull();
            item.Name.Should().NotBeEmpty();
        }
    }

    [Fact]
    public async Task ImportTranscriptionsOfSeededVideos()
    {
        await foreach (var transcript in Helper.ImportTranscriptions(new VideoId[] { 1, 2 }))
        {
            Output.WriteLine($"[{transcript.Language}]: {transcript.Lines.Count} Line(s))");
        }
    }

    [Theory]
    [InlineData(null, "PLLdi1lheZYVKkvX20ihB7Ay2uXMxa0Q5e")]
    public async Task ImportPlaylistFromYouTube([FromServices] IYouTubeHelper helper, string playlistId)
    {
        List<Video> videos = new();
        await foreach (var video in helper.ImportVideosOfPlaylist(playlistId))
        {
            videos.Add(video);
        }

        // Saves a file with the list of videos
        var json = JsonSerializer.Serialize(videos);
        await File.WriteAllTextAsync($"Playlist-{playlistId}.json", json);
    }

    [Theory]
    [InlineData(null, new[] { "BBd3aHnVnuE" })]
    [InlineData(null, new[] { "4Y4YSpF6d6w", "tWZQPCU4LJI", "BBd3aHnVnuE", "BFfb2P5wxC0", "dQw4w9WgXcQ", "n1kmKpjk_8E" })]
    public async Task ImportVideosFromYouTube([FromServices] IYouTubeHelper helper, string[] ids)
    {
        await foreach (var video in helper.ImportVideos(ids))
        {
            video.Name.Should().NotBeNullOrEmpty();
            video.Description.Should().NotBeNullOrEmpty();
            video.Location.Should().NotBeNullOrEmpty();
            video.Thumbnails.Should().HaveCount(5);
            video.Tags.Should().NotBeEmpty();

            video.Details.Provider.Should().Be(YouTubePlaylistsHelper.ProviderId);
            video.Details.VideoPublishedAt.Should().NotBe(DateTime.MinValue);
            video.Details.VideoOwnerChannelTitle.Should().NotBeEmpty();
            video.Details.VideoOwnerChannelId.Should().NotBeEmpty();


            // Saves a file with the video information
            var json = JsonSerializer.Serialize(video);
            await File.WriteAllTextAsync($"Video-{video.Details.ProviderVideoId}.json", json);
        }
    }

    [Fact]
    public async Task ImportTranscriptionsFromYouTubeVideos()
    {
        var videos = new Dictionary<VideoId, string>()
        {
            { 1, "n1kmKpjk_8E" }, // Aldous Huxley - The Dancing Shiva
            { 2, "BBd3aHnVnuE" }  // If Reality is NON-DUAL, Why are there so...
        };

        await foreach (var transcript in Helper.ImportTranscriptions(videos.Select(x => x.Key)))
        {
            string src = videos[transcript.VideoId];

            // Saves a file with the video information
            var json = JsonSerializer.Serialize(transcript);
            await File.WriteAllTextAsync($"Transcription-{src}.json", json);
        }
    }
}
