using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Domain.Aggregates.Video;
using Company.Videomatic.Infrastructure.YouTube;
using FluentAssertions;

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
        List<Video> videos = new ();
        await foreach (var video in helper.ImportVideosOfPlaylist(url))
        {
            videos.Add(video);
        }
    }

    [Theory]
    [InlineData(null, new[] { "4Y4YSpF6d6w", "tWZQPCU4LJI" })]
    public async Task ImportVideos([FromServices] IYouTubeHelper helper, string[] ids)
    {
        await foreach (var video in helper.ImportVideoDetails(ids))
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


            // TODO: add more tests for the thumbnails and tags

        }
    }
}