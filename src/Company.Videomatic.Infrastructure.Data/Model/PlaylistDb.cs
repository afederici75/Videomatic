namespace Company.Videomatic.Infrastructure.Data.Model;

public class PlaylistDb : EntityBaseDb
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;

    public List<VideoDb> Videos { get; } = new();
    public List<PlaylistDbVideoDb> PlaylistVideos { get; } = new();
}
