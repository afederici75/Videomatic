using System.Runtime.CompilerServices;
using Xunit.Abstractions;

namespace Company.Videomatic.Infrastructure.YouTube.Tests;

public class YouTubePlaylistsHelperTests
{
    private readonly ITestOutputHelper Output;

    public YouTubePlaylistsHelperTests(ITestOutputHelper output)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    [Theory]
    [InlineData(null, "UCqiZA4pUT5RxrMCddeKdpGw")] // Mine
    [InlineData(null, "UC_x5XG1OV2P6uZZ5FSM9Ttw")] // Google
    public async Task GetMyPlaylists([FromServices] IPlaylistsHelper helper, string channelId)
    {
        await foreach (var item in helper.GetPlaylists(channelId))
        {
            Output.WriteLine($"[{item.Id}]: {item.Title}");

            item.Id.Should().NotBeEmpty();
            item.Title.Should().NotBeEmpty();
        }
    }

    [Theory]
    [InlineData(null, "PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb")] // Alternative Living, Sustainable Future
    [InlineData(null, "PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu")] // Google I/O Keynote Films
    public async Task GetPlaylistVideos([FromServices] IPlaylistsHelper helper, string playlistId)
    {        
        await foreach (var item in helper.GetVideosOfPlaylist(playlistId))
        {
            Output.WriteLine($"[{item.Id}]: {item.Title}");

            item.Id.Should().NotBeEmpty();
            item.Title.Should().NotBeEmpty();
        }
    }

    [Theory]
    [InlineData(null, "GJLlxj_dtq8&")] // Surface Go Review - It’s Awesome
    [InlineData(null, "5fj7wRSbCPQ&")] // What do Buddhists believe happens after death?
    public async Task GetVideoTranscript([FromServices] IPlaylistsHelper helper, string videoId)
    {
        await foreach (var item in helper.GetTranscriptionOfVideo(videoId))
        {
            Output.WriteLine($"[{item.Text}]: {item.Start}/{item.duration}");
            
        }
    }
}
