namespace Company.Videomatic.Domain;

public class Thumbnail
{ 
    public int Id { get; set; }

    public int VideoId { get; set; }

    public ThumbnailResolution Resolution { get; set; }
    public required string Url { get; set; }
    
    public int? Height { get; set; }

    public int? Width { get; set; }
}

