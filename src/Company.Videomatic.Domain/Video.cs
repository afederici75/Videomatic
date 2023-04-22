namespace Company.Videomatic.Domain;

public class Video
{
    public int Id { get; set; }

    public required string ProviderId { get; set; }
    public required string VideoUrl { get; set; }
    public required string Title { get; set; }    
    
    public string? Description { get; set; }
    public IList<Thumbnail>? Thumbnails { get; } = new List<Thumbnail>();
    public VideoTranscript? Transcript { get; set; }
}