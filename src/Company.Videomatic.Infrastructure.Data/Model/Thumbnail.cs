using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Infrastructure.Data.Model;

public class Thumbnail : EntityBase
{
    public Thumbnail(long videoId, string location, ThumbnailResolution resolution, int height, int width)
    {
        VideoId = videoId;
        Location = location;
        Resolution = resolution;
        Height = height;
        Width = width;
    }

    public long VideoId { get; private set; }
    public string Location { get; private set; }
    public ThumbnailResolution Resolution { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Thumbnail()
    { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

