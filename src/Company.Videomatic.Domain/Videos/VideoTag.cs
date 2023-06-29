namespace Company.Videomatic.Domain.Videos;

public class VideoTag : EntityBase
{
    internal static VideoTag Create(VideoId videoId, string name)
    {
        return new VideoTag()
        {
            VideoId = videoId,
            Name = name
        };

    }

    public VideoId VideoId { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    private VideoTag()
    { }
}