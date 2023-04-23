using Newtonsoft.Json;

namespace Company.Videomatic.Domain;

public class Thumbnail
{
    public int Id { get; init; }
    public string Url { get; init; }

    public ThumbnailResolution? Resolution { get; set; }
    public int? Height { get; set; }
    public int? Width { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private Thumbnail()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }


    public Thumbnail(string url, ThumbnailResolution? resolution = null, int? height = null, int? width = null)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
        Resolution = resolution;
        Height = height;
        Width = width;
    }

    public override string ToString()
    {
        return $"[{Resolution}, {Width}x{Height}] {Url}";
    }
}