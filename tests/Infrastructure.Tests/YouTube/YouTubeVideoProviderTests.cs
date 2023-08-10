using Company.Videomatic.Application.Abstractions;

namespace Infrastructure.Tests.YouTube;

public class YouTubeVideoProviderTests
{
    public YouTubeVideoProviderTests(
        ITestOutputHelper output,
        IVideoProvider videoHostingProvider)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
        Provider = videoHostingProvider ?? throw new ArgumentNullException(nameof(videoHostingProvider));
    }

    public ITestOutputHelper Output { get; }
    public IVideoProvider Provider { get; }

    [Theory]
    [InlineData(new[] { "PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb" }, 1, null)] // Alternative Living, Sustainable Future
    [InlineData(new[] { "PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu" }, 1, null)] // Google I/O Keynote Films
    [InlineData(new[] { "https://www.youtube.com/watch?v=WlFz5olN3V8&list=PL20mfA9efrmMmLEy1fhFDvB_OmUpNUFqB" }, 1, null)] // Semantic Kernel
    [InlineData(new[] { "PLLdi1lheZYVJHCx7igCJIUmw6eGmpb4kb", "PLOU2XLYxmsIKsEnF6CdfRK1Vd6XUn_QMu" }, 2, null)]

    public async Task GetPlaylistInformation(string[] idsOrUrls, int expectedCount, [FromServices] CancellationToken token)
    {
        var count = 0;

        await foreach (var p in Provider.GetPlaylistsAsync(idsOrUrls, token))
        {
            count++;
            Output.WriteLine(p.ToString());

            p.Id.Should().NotBeNullOrEmpty();
            p.Name.Should().NotBeNullOrEmpty();
            //p.Description.Should().NotBeNullOrEmpty();            
        }

        count.Should().Be(expectedCount);
    }

    [Theory]
    [InlineData(new[] { "UC0YvoAYGgdOfySQSLcxtu1w" }, 1, null)] // https://www.youtube.com/@BeauoftheFifthColumn
    [InlineData(new[] { "UCso1uUOa86mottj1jrawysg" }, 1, null)] // https://www.youtube.com/@alexchaomander
    [InlineData(new[] { "UC0YvoAYGgdOfySQSLcxtu1w", "UCso1uUOa86mottj1jrawysg" }, 2, null)]

    public async Task GetChannelInformation(string[] idsOrUrls, int expectedCount, [FromServices] CancellationToken token)
    {
        var count = 0;

        await foreach (var p in Provider.GetChannelsAsync(idsOrUrls, token))
        {
            count++;
            Output.WriteLine(p.ToString());

            p.Id.Should().NotBeNullOrEmpty();
            p.Name.Should().NotBeNullOrEmpty();
            //p.Description.Should().NotBeNullOrEmpty();            
        }

        count.Should().Be(expectedCount);
    }

    [Theory]
    [InlineData(new[] { "BBd3aHnVnuE" }, 1, null)]
    [InlineData(new[] { "4Y4YSpF6d6w", "BBd3aHnVnuE" }, 2, null)]
    [InlineData(new[] { "BFfb2P5wxC0", "dQw4w9WgXcQ", "BBd3aHnVnuE" }, 3, null)]
    public async Task GetVideoInformation(string[] idsOrUrls, int expectedCount, [FromServices] CancellationToken token)
    {
        var count = 0;

        await foreach (var p in Provider.GetVideosAsync(idsOrUrls, token))
        {
            count++;
            Output.WriteLine(p.ToString());

            p.Id.Should().NotBeNullOrEmpty();
            p.Name.Should().NotBeNullOrEmpty();
            //p.Description.Should().NotBeNullOrEmpty();
        }

        count.Should().Be(expectedCount);
    }
}