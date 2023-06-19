namespace Company.Videomatic.Infrastructure.Data.Model;

public class Playlist : EntityBase
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;

    public List<Video> Videos { get; } = new();
    public List<PlaylistVideo> PlaylistVideos { get; } = new();
}
