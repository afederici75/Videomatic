using Newtonsoft.Json;

namespace Company.Videomatic.Infrastructure.YouTube.Tests;

public class YouTubeVideoImporterTests
{
    [Theory]
    [InlineData(null, YouTubeVideos.RickAstley_NeverGonnaGiveYouUpLink)]
    [InlineData(null, YouTubeVideos.AldousHuxley_DancingShivaLink)]
    [InlineData(null, YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism)]
    [InlineData(null, YouTubeVideos.HyonGakSunim_WhatIsZenLink)]
    public async Task ImportVideoFromYoutube([FromServices] IVideoImporter importer, string url)
    {
        var video = await importer.ImportAsync(new Uri(url));
        var info = YouTubeVideos.Videos[url];

        video.ProviderId.Should().Be(YouTubeVideoImporter.ProviderId);
        video.ProviderVideoId.Should().Be(info.Id);
        video.Title.Should().Be(info.Title);
        video.Transcripts.Should().HaveCount(info.TransctriptCount); 
        video.Thumbnails.Should().HaveCount(info.ThumbnailsCount);
    }

    [Theory(Skip = "This method regenerates the data for Domain.Tests\\TestData.")]
    [InlineData(null, YouTubeVideos.RickAstley_NeverGonnaGiveYouUpLink)]
    [InlineData(null, YouTubeVideos.AldousHuxley_DancingShivaLink)]
    [InlineData(null, YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism)]
    [InlineData(null, YouTubeVideos.HyonGakSunim_WhatIsZenLink)]
    public async Task RegenerateTestData([FromServices] IVideoImporter importer, string url)
    {
        // This method is used to regenerate the test data used by the Domain tests.
        // On Windows, the files will be saved under \bin\Debug\net7.0\TestData.
        // Once the files are generated, they can be copied to the Domain.Tests\TestData folder.

        var video = await importer.ImportAsync(new Uri(url));
        var info = YouTubeVideos.Videos[url];

        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        // Serializes        
        var json = JsonConvert.SerializeObject(video, settings);

        var path = "TestData";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        await File.WriteAllTextAsync($"{path}\\{info.Id}.json", json);
    }
}