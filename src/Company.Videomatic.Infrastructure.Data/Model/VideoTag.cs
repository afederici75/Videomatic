namespace Company.Videomatic.Infrastructure.Data.Model;

public class VideoTag : EntityBase
{
    public VideoTag(long videoId, string name)
    {
        VideoId = videoId;
        Name = name;
    }

    public long VideoId { get; set; }
    public string Name { get; set; } = default!;
}