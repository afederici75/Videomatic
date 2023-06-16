using Company.Videomatic.Domain.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Company.Videomatic.Infrastructure.Data;

public static class VideoDataGenerator
{
    public const string FolderName = "TestData";

    public static bool HasData()
    {
        var dirExists = Directory.Exists(FolderName);
        if (!dirExists)
            return false;

        var files = Directory.GetFiles(FolderName, "*.json", SearchOption.TopDirectoryOnly);
        
        return files.Any();
    }

    public async static Task<Video[]> CreateAllVideos(bool includeAll)
    {
        var tasks = YouTubeVideos
            .GetHints()
            .Select(x => CreateVideoFromFileAsync(x.ProviderVideoId, includeAll))
            .ToArray();

        var videos = await Task.WhenAll(tasks);
        return videos;
    }

    public static Task<Video> CreateVideoFromFileAsync(string videoId, bool includeAll)
    { 
        var includes = includeAll ? new[] 
        { 
            nameof(Video.Artifacts),
            nameof(Video.Thumbnails),
            nameof(Video.Transcripts),
        } : Array.Empty<string>();

        return CreateVideoFromFileAsync(videoId, includes);
    }

    public static async Task<Video> CreateVideoFromFileAsync(string videoId, params string[] includes)
    {
        var json = await File.ReadAllTextAsync($"{FolderName}\\{videoId}.json");
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
            video.AddArtifact(new Artifact("SUMMARY", "Not a summary. Just test data..."))
                 .AddArtifact(new Artifact("REVIEW", "Not a review. Just test data..."));
        }
        
        return video!;
    }   
}
