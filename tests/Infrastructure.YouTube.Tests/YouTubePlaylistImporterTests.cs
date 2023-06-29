using Company.Videomatic.Application.Features.Model;

namespace Infrastructure.YouTube.Tests;

public class YouTubePlaylistImporterTests
{
    private readonly ITestOutputHelper Output;

    public YouTubePlaylistImporterTests(ITestOutputHelper output)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    [Theory]
    [InlineData(null, "PLLdi1lheZYVKkvX20ihB7Ay2uXMxa0Q5e")]
    public async Task ImportPlaylist([FromServices] IYouTubeHelper helper , string url)
    {
        List<VideoDTO> videos = new ();
        await foreach (var video in helper.GetAllVideosOfPlaylist(url))
        {
            videos.Add(video);

        }
    }
}
