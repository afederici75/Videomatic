namespace Company.Videomatic.Infrastructure.Data.Model;

public class PlaylistVideo
{
    public long PlaylistId { get; set; }
    public long VideoId { get; set; } = default!;

    public Video Video { get; set; } = default!;
    public Playlist Playlist { get; set; } = default!;
}