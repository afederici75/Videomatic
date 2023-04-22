namespace Company.Videomatic.Domain;

public class Folder
{
    public Folder? Parent { get; set; }
    public required string Name { get; set; }
    public IEnumerable<VideoLink> Videos { get; set; } = Array.Empty<VideoLink>();
    public IEnumerable<Folder> Children { get; set; } = Array.Empty<Folder>();
}