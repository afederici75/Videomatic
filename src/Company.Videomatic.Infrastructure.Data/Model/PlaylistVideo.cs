namespace Company.Videomatic.Infrastructure.Data.Model;

public class PlaylistVideo
{
    public static PlaylistVideo Create(long playlistId, long videoId)
    {
        return new PlaylistVideo
        {
            PlaylistId = playlistId,
            VideoId = videoId
        };
    }

    public long PlaylistId { get; private set; }
    public long VideoId { get; private set; }

    public Video Video { get; private set; } = default!;
    public Playlist Playlist { get; private set; } = default!;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private PlaylistVideo()
    {
            
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}