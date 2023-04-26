using Company.Videomatic.Domain.Tests;
using Newtonsoft.Json;

namespace Company.Videomatic.Infrastructure.YouTube.Tests;

public class YouTubeVideoImporterTests
{
    record VideoInfo(string Title, int TransctriptCount, int ThumbnailsCount, int ArtifactsCount);

    static class Tips
    {
        public static IReadOnlyDictionary<string, VideoInfo> VideosTips = new Dictionary<string, VideoInfo>
        {
            { 
                YouTubeVideos.RickAstley_NeverGonnaGiveYouUp,
                new VideoInfo("Rick Astley - Never Gonna Give You Up (Official Music Video)", 1, 5, 2) },

            {   
                YouTubeVideos.AldousHuxley_DancingShiva,
                new VideoInfo("Aldous Huxley - The Dancing Shiva", 1, 5, 2) },

            { 
                YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism,
                new VideoInfo("If Reality is NON-DUAL, Why are there so many GODS in Hinduism?", 1, 5, 2) },

            { 
                YouTubeVideos.HyonGakSunim_WhatIsZen,
                new VideoInfo("What is ZEN ? - Hyon Gak Sunim", 1, 5, 2) }
        };
    }

    [Theory]
    [InlineData(null, YouTubeVideos.RickAstley_NeverGonnaGiveYouUp)]
    [InlineData(null, YouTubeVideos.AldousHuxley_DancingShiva)]
    [InlineData(null, YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism)]
    [InlineData(null, YouTubeVideos.HyonGakSunim_WhatIsZen)]
    public async Task ImportsVideoFromYoutube([FromServices] IVideoImporter importer, string videoId)
    {
        var uri = YouTubeVideos.GetUri(videoId);
        var info = Tips.VideosTips[videoId];

        var video = await importer.ImportAsync(uri);
        
        video.ProviderId.Should().Be(YouTubeVideoImporter.ProviderId);
        video.ProviderVideoId.Should().Be(videoId);
        video.Title.Should().Be(info.Title);
        video.Transcripts.Should().HaveCount(info.TransctriptCount); 
        video.Thumbnails.Should().HaveCount(info.ThumbnailsCount);
        video.Artifacts.Should().HaveCount(0); // Important. Artifacts are generated after the import.
    }

    [Theory()]//Skip = "This method regenerates the data for Domain.Tests\\TestData.")]
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
        var info = Tips.VideosTips[videoId];

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