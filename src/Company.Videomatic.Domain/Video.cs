namespace Company.Videomatic.Domain;

public class Video
{
    public int Id { get; set; }
    public required string ProviderId { get; set; }
    public required string VideoUrl { get; set; }
    public required string Title { get; set; }    
    public required string Source { get; set; }
    public string? Description { get; set; }
    public IEnumerable<Thumbnail> Thumbnails { get; set; } = Array.Empty<Thumbnail>();
    public Transcript? Transcript { get; set; }
}
