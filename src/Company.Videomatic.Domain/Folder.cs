namespace Company.Videomatic.Domain;

public class Folder
{
    public int Id { get; set; }
    public Folder? Parent { get; set; }
    public required string Name { get; set; }
    public IEnumerable<Video> Videos { get; set; } = Array.Empty<Video>();
    public IEnumerable<Folder> Children { get; set; } = Array.Empty<Folder>();
    
}