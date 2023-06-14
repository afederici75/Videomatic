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
    [InlineData(null)]
    public async Task GetMyPlaylists([FromServices] IPlaylistsHelper helper)
    {
        await foreach (var item in helper.GetPlaylists())
        {
            Output.WriteLine(item.Id);
        }
    }
}
