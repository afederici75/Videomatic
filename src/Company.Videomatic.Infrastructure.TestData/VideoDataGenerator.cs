using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Company.Videomatic.Infrastructure.TestData;

public static class VideoDataGenerator
{
    public const string FolderName = "TestData";

    public static async Task<Video> CreateVideoFromFile(string videoId, params string[] includes)
    {
        var json = await File.ReadAllTextAsync($"TestData\\{videoId}.json");
        JObject jobj = (JObject)JsonConvert.DeserializeObject(json, JsonHelper.GetJsonSettings())!;

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
        
        return video!;
    }   
}
