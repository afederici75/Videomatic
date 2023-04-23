using Newtonsoft.Json;

namespace Company.Videomatic.Domain;

public class Thumbnail
{
    public int Id { get; private set; }
    public Video Video { get; private set; }
    public string Url { get; private set; }

    public ThumbnailResolution? Resolution { get; set; }
    public int? Height { get; set; }
    public int? Width { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    public Thumbnail()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }


    public Thumbnail(Video video, string url, ThumbnailResolution? resolution, int? height = null, int? width = null)
    {
        Video = video ?? throw new ArgumentNullException(nameof(video));
        Url = url ?? throw new ArgumentNullException(nameof(url));
        Resolution = resolution;
        Height = height;
        Width = width;
    }

    public void SetVideo(Video video)
    {
        Video = video ?? throw new ArgumentNullException(nameof(video));
    }

    public override string ToString()
    {
        return $"[{Resolution}, {Width}x{Height}] {Url}";
    }
}