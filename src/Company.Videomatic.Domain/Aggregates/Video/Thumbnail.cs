namespace Company.Videomatic.Domain.Aggregates.Video;

public class Thumbnail : ValueObject
{
    public Thumbnail(ThumbnailResolution resolution, string location, int height, int width)
    {
        Location = location;
        Resolution = resolution;
        Height = height;
        Width = width;        
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Location.ToUpper(); // Case insensitive
        yield return Resolution;
        yield return Height;
        yield return Width;
    }

    public string Location { get; private set; } = default!;
    public ThumbnailResolution Resolution { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }

    #region Private 

    private Thumbnail()
    { }

    #endregion
}