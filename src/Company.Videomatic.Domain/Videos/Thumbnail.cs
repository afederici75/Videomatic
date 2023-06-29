namespace Company.Videomatic.Domain.Videos;

public class Thumbnail : EntityBase
{
    internal static Thumbnail Create(VideoId videoId, string location, ThumbnailResolution resolution, int height, int width)
    {
        return new Thumbnail
        {
            VideoId = videoId,
            Location = location,
            Resolution = resolution,
            Height = height,
            Width = width,
        };
    }

    public VideoId VideoId { get; private set; } = default!;
    public string Location { get; private set; } = default!;
    public ThumbnailResolution Resolution { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }

    #region Private 

    private Thumbnail()
    { }

    #endregion
}