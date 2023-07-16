using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain.Aggregates.Video;
using Company.Videomatic.Infrastructure.YouTube;
using FluentAssertions;
using System.Text.Json;

namespace Infrastructure.Tests.YouTube;

public class YouTubePlaylistImporterTests
{
    private readonly ITestOutputHelper Output;

    public YouTubePlaylistImporterTests(ITestOutputHelper output)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    [Theory]
    [InlineData(null, "PLLdi1lheZYVKkvX20ihB7Ay2uXMxa0Q5e")]
    public async Task ImportPlaylistFromYouTube([FromServices] IYouTubeHelper helper , string playlistId)
    {
        List<Video> videos = new ();
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

    [Theory]
    [InlineData(null)]
    public async Task ImportTranscriptionsFromYouTubeVideos([FromServices] IYouTubeHelper helper)
    {
        var videos = new Dictionary<VideoId, string>()
        {
            { 1, "n1kmKpjk_8E" }, // Aldous Huxley - The Dancing Shiva
            { 2, "BBd3aHnVnuE" }  // If Reality is NON-DUAL, Why are there so...
        };

        await foreach (var transcript in helper.ImportTranscriptions(videos.Select(x => x.Key)))
        {
            string src = videos[transcript.VideoId];

            // Saves a file with the video information
            var json = JsonSerializer.Serialize(transcript);
            await File.WriteAllTextAsync($"Transcription-{src}.json", json);
        }
    }
}