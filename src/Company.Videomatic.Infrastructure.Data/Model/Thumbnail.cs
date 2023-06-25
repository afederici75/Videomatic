using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Infrastructure.Data.Model;

public class Thumbnail : EntityBase
{
    internal static Thumbnail Create(long videoId, string location, ThumbnailResolution resolution, int height, int width)
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

    public long VideoId { get; private set; }
    public string Location { get; private set; } = default!;
    public ThumbnailResolution Resolution { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }

    #region Private 

    private Thumbnail()
    { }

    #endregion
}