namespace Company.Videomatic.Domain.Model;

public class Thumbnail : EntityBase
{    
    public string Location { get; private set; }
    public ThumbnailResolution Resolution { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }

    public Thumbnail(string location, ThumbnailResolution resolution, int height, int width)
    {
        Location = Guard.Against.NullOrWhiteSpace(location, nameof(location));
        Resolution = resolution;
        Height = Guard.Against.NegativeOrZero(height, nameof(height));
        Width = Guard.Against.NegativeOrZero(width, nameof(width));
    }

    #region Methods

    public override string ToString()
    {
        return $"[{Resolution}, {Width}x{Height}] {Location}";
    }    

    #endregion

    #region Private

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private Thumbnail()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    #endregion
}