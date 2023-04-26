
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Company.Videomatic.Domain.Tests;

public static class VideoDataGenerator
{
    public const string FolderName = "TestData";

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
        => LoadFromFile(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp, includes);
}
