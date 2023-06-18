namespace Company.Videomatic.Infrastructure.Data.Model;

public class PlaylistDbVideoDb
{
    public long PlaylistId { get; set; }
    public long VideoId { get; set; } = default!;

    public VideoDb Video { get; set; } = default!;
    public PlaylistDb Playlist { get; set; } = default!;
}