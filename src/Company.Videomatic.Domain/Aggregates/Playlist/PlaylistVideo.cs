using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Domain.Aggregates.Playlist;

public class PlaylistVideo
{
    internal static PlaylistVideo Create(PlaylistId playlistId, VideoId videoId)
    {
        return new PlaylistVideo
        {
            PlaylistId = playlistId,
            VideoId = videoId
        };
    }

    public PlaylistId PlaylistId { get; private set; } = default!;
    public VideoId VideoId { get; private set; } = default!;

    #region Private

    private PlaylistVideo()
    {

    }

    #endregion
}