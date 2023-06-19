namespace Company.Videomatic.Infrastructure.Data.Model;

public class Tag : EntityBase
{
    public string Name { get; set; } = default!;

    public List<Video> Videos { get; } = new();
    public List<VideoTag> VideoTags { get; } = new ();
}