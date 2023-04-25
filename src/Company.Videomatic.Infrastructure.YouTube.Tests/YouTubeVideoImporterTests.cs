using Company.Videomatic.Application.Abstractions;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Infrastructure.YouTube.Tests;

public class YouTubeVideoImporterTests
{
    [Theory]
    [InlineData(null)]
    public async Task GetRickAsley([FromServices] IVideoImporter importer)
    {
        var video = await importer.ImportAsync(
            new Uri("https://www.youtube.com/watch?v=dQw4w9WgXcQ&pp=ygUkcmljayBhc3RsZXkgbmV2ZXIgZ29ubmEgZ2l2ZSB5b3UgdXAg"));

        video.ProviderId.Should().Be("dQw4w9WgXcQ");
        video.Title.Should().Be("Rick Astley - Never Gonna Give You Up (Official Music Video)");
        video.Transcripts.Should().HaveCount(1); 
        video.Thumbnails.Should().HaveCount(5);
    }

    [Theory]
    [InlineData(null)]
    public async Task GetHuxleysDancingShiva([FromServices] IVideoImporter importer)
    {
        var video = await importer.ImportAsync(
            new Uri("https://www.youtube.com/watch?v=n1kmKpjk_8E"));

        video.ProviderId.Should().Be("n1kmKpjk_8E");
        video.Title.Should().Be("Aldous Huxley - The Dancing Shiva");
        video.Transcripts.Should().HaveCount(1);
        video.Thumbnails.Should().HaveCount(5);
    }
}
