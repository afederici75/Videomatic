using Company.Videomatic.Domain.Videos;

namespace Company.Videomatic.Domain.Playlists;

public class PlaylistVideo
{
    internal static PlaylistVideo Create(long playlistId, long videoId)
    {
        return new PlaylistVideo
        {
            PlaylistId = playlistId,
            VideoId = videoId
        };
    }

    public long PlaylistId { get; private set; }
    public long VideoId { get; private set; }

    public Videos.Video Video { get; private set; } = default!;
    public Playlist Playlist { get; private set; } = default!;

    #region Private

    private PlaylistVideo()
    {
            
    }

    #endregion
}