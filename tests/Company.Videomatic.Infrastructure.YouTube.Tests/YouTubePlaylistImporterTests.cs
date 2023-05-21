using Xunit.Abstractions;

namespace Company.Videomatic.Infrastructure.YouTube.Tests;

public class YouTubePlaylistImporterTests
{
    private readonly ITestOutputHelper Output;

    public YouTubePlaylistImporterTests(ITestOutputHelper output)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    [Theory]
    [InlineData(null, "https://www.youtube.com/playlist?list=PLLdi1lheZYVKkvX20ihB7Ay2uXMxa0Q5e")]
    public async Task ImportPlaylist([FromServices] IPlaylistImporter importer, string url)
    { 
        var coll = await importer.ImportAsync(new Uri(url));
        Assert.NotNull(coll);
    }
}
