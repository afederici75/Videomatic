namespace Company.Videomatic.Domain.Entities.VideoAggregate;

public class Thumbnail : ValueObject
{
    public Thumbnail(string location, ThumbnailResolution resolution, int height, int width)
    {
        Location = location;
        Resolution = resolution;
        Height = height;
        Width = width;        
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Location;
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