﻿namespace Company.Videomatic.Domain.Aggregates.Video;

public class Thumbnail : ValueObject
{
    public Thumbnail(string location, int height, int width)
    {
        Location = location;
        Height = height;
        Width = width;        
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Location.ToUpper(); // Case insensitive
        yield return Height;
        yield return Width;
    }

    public string Location { get; private set; } = default!;
    public int Height { get; private set; }
    public int Width { get; private set; }

    #region Private 

    private Thumbnail()
    { }

    #endregion
}