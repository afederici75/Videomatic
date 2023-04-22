namespace Company.Videomatic.Domain;

public class Thumbnail
{ 
    public ThumbnailResolution Resolution { get; set; }
    public required string Url { get; set; }
    
    public string? ETag { get; set; }

    public long? Height { get; set; }

    public long? Width { get; set; }
}

