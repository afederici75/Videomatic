using Newtonsoft.Json;

namespace Company.Videomatic.Infrastructure.YouTube.Tests;


public class YouTubeVideoImporterTests
{
    
    [Theory]
    [InlineData(null, YouTubeVideos.RickAstley_NeverGonnaGiveYouUp)]
    [InlineData(null, YouTubeVideos.AldousHuxley_DancingShiva)]
    [InlineData(null, YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism)]
    [InlineData(null, YouTubeVideos.HyonGakSunim_WhatIsZen)]
    public async Task ImportsAndVerifiesVideoFromYoutube([FromServices] IVideoImporter importer, string videoId)
    {
        var uri = YouTubeVideos.GetUri(videoId);
        var info = YouTubeVideos.Tips.VideosTips[videoId];

        var video = await importer.ImportAsync(uri);
        
        video.ProviderId.Should().Be(YouTubeVideoImporter.ProviderId);
        video.ProviderVideoId.Should().Be(videoId);
        video.Title.Should().Be(info.Title);
        video.Transcripts.Should().HaveCount(info.TransctriptCount); 
        video.Thumbnails.Should().HaveCount(info.ThumbnailsCount);
        video.Artifacts.Should().HaveCount(0); // Important. Artifacts are generated after the import.
    }

    [Theory(Skip = "This method regenerates the data for Domain.Tests\\TestData.")]
    [InlineData(null, YouTubeVideos.RickAstley_NeverGonnaGiveYouUp)]
    [InlineData(null, YouTubeVideos.AldousHuxley_DancingShiva)]
    [InlineData(null, YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism)]
    [InlineData(null, YouTubeVideos.HyonGakSunim_WhatIsZen)]
    public async Task RegeneratesTestDataUnderBinDebug([FromServices] IVideoImporter importer, string videoId)
    {
        // This method is used to regenerate the test data used by the Domain tests.
        // On Windows, the files will be saved under \bin\Debug\net7.0\TestData.
        // Once the files are generated, they can be copied to the Domain.Tests\TestData folder.
        var uri = YouTubeVideos.GetUri(videoId);
        var info = YouTubeVideos.Tips.VideosTips[videoId];

        var video = await importer.ImportAsync(uri);

        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        // Serializes        
        var json = JsonConvert.SerializeObject(video, settings);

        // The files will be saved under \bin\Debug\net7.0\TestData.
        var outputPath = VideoDataGenerator.FolderName;
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        await File.WriteAllTextAsync($"{outputPath}\\{videoId}.json", json);
    }
}