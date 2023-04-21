namespace Company.Videomatic.Domain;

public class Video
{
    public required string Id { get; set; }
    public required string VideoUrl { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? OriginalDescription { get; set; }
    public string? ThumbnailUrl { get; set; }    
}