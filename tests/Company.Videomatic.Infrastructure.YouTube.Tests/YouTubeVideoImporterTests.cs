using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Company.Videomatic.Infrastructure.YouTube.Tests;

public class YouTubeVideoImporterTests
{
    private readonly ITestOutputHelper Output;

    public YouTubeVideoImporterTests(ITestOutputHelper output)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }
    
    [Theory(DisplayName = nameof(ImportsAndVerifiesVideoFromYoutube))]
    [InlineData(null, YouTubeVideos.RickAstley_NeverGonnaGiveYouUp)]
    [InlineData(null, YouTubeVideos.AldousHuxley_DancingShiva)]
    [InlineData(null, YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism)]
    [InlineData(null, YouTubeVideos.HyonGakSunim_WhatIsZen)]
    public async Task ImportsAndVerifiesVideoFromYoutube([FromServices] IVideoImporter importer, string videoId)
    {
        var uri = YouTubeVideos.GetUri(videoId);
        var info = YouTubeVideos.GetInfoByVideoId(videoId);

        var video = await importer.ImportAsync(uri);

        video.Id.Should().Be(0);
        video.ProviderId.Should().Be(YouTubeVideoImporter.ProviderId);
        video.ProviderVideoId.Should().Be(videoId);
        video.Title.Should().Be(info.Title);

        video.Transcripts.Should().HaveCount(info.TransctriptCount); 
        video.Thumbnails.Should().HaveCount(info.ThumbnailsCount);
        video.Artifacts.Should().HaveCount(0); // Important. Artifacts are generated after the import.
    }

    [Theory(
        DisplayName = nameof(RegeneratesTestDataUnderBinDebug),
        Skip = "This method regenerates the data for Domain.Tests\\TestData.")]
    //[Theory]
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
        var info = YouTubeVideos.GetInfoByVideoId(videoId);

        var video = await importer.ImportAsync(uri);

        // Serializes        
        var settings = JsonHelper.GetJsonSettings();
        var json = JsonConvert.SerializeObject(video, settings);

        // The files will be saved under \bin\Debug\net7.0\TestData.
        var outputPath = VideoDataGenerator.FolderName;
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        var fileName = $"{outputPath}\\{videoId}.json";
        await File.WriteAllTextAsync(fileName, json); ;

        Output.WriteLine($"Written {fileName}:\n{json}");
    }
}