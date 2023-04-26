
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Company.Videomatic.Domain.Tests;

public static class YouTubeVideos
{
    public const string RickAstley_NeverGonnaGiveYouUpLink = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
    public const string AldousHuxley_DancingShivaLink = "https://www.youtube.com/watch?v=n1kmKpjk_8E";
    public const string SwamiTadatmananda_WhySoManyGodsInHinduism = "https://www.youtube.com/watch?v=BBd3aHnVnuE";
    public const string HyonGakSunim_WhatIsZenLink = "https://www.youtube.com/watch?v=BFfb2P5wxC0";

    public const string RickAstley_NeverGonnaGiveYouUpId = "dQw4w9WgXcQ";
    public const string AldousHuxley_DancingShivaId = "n1kmKpjk_8E";
    public const string SwamiTadatmananda_WhySoManyGodsInHinduismId = "BBd3aHnVnuE";
    public const string HyonGakSunim_WhatIsZenId = "BFfb2P5wxC0";

    public record VideoInfo(string Id, string Title, int TransctriptCount, int ThumbnailsCount);

    public static IReadOnlyDictionary<string, VideoInfo> Videos = new Dictionary<string, VideoInfo>
    {
        { RickAstley_NeverGonnaGiveYouUpLink, new VideoInfo("dQw4w9WgXcQ", "Rick Astley - Never Gonna Give You Up (Official Music Video)", 1, 5) },
        { AldousHuxley_DancingShivaLink, new VideoInfo("n1kmKpjk_8E", "Aldous Huxley - The Dancing Shiva", 1, 5) },
        { SwamiTadatmananda_WhySoManyGodsInHinduism, new VideoInfo("BBd3aHnVnuE", "If Reality is NON-DUAL, Why are there so many GODS in Hinduism?", 1, 5) },
        { HyonGakSunim_WhatIsZenLink, new VideoInfo("BFfb2P5wxC0", "What is ZEN ? - Hyon Gak Sunim", 1, 5) }
    };
}

public static class MockDataGenerator
{    
    static async Task<Video> LoadFromFile(string videoId, params string[] includes)
    {
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        var json = await File.ReadAllTextAsync($"TestData\\{videoId}.json");
        JObject jobj = (JObject)JsonConvert.DeserializeObject(json, settings)!;

        var arrayProps = jobj.Properties()
            .Where(x => x.Value.Type == JTokenType.Array)
            .ToList();

        arrayProps.ForEach(p =>
        {
            if (!includes.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                p.Remove();
        });
                
        Video video = jobj.ToObject<Video>()!;

        if (includes.Contains(nameof(Video.Artifacts), StringComparer.OrdinalIgnoreCase))
        {
            video.AddArtifacts(
                new Artifact("SUMMARY", "Not a summary. Just test data..."),
                new Artifact("REVIEW", "Not a review. Just test data...")
            );
        }

        //var video = JsonConvert.DeserializeObject<Video>(json, settings);

        return video!;
    }
    
    public static Task<Video> CreateRickAstleyVideo(params string[] includes)
        => LoadFromFile(YouTubeVideos.RickAstley_NeverGonnaGiveYouUpId, includes);
}
