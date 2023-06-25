namespace Company.Videomatic.Domain.Videos;

public class VideoTag : EntityBase
{
    internal static VideoTag Create(long videoId, string name)
    {
        return new VideoTag()
        {
            VideoId = videoId,
            Name = name
        };

    }

    public long VideoId { get; private set; }
    public string Name { get; private set; } = default!;

    private VideoTag()
    { }
}