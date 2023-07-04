using Company.Videomatic.Domain.Aggregates.Playlist;

namespace Company.Videomatic.Domain.Aggregates.Video;

public class VideoPlaylist
{
    internal static VideoPlaylist Create(PlaylistId playlistId, VideoId videoId)
    {
        return new VideoPlaylist
        {
            PlaylistId = playlistId,
            VideoId = videoId
        };
    }

    public PlaylistId PlaylistId { get; private set; } = default!;
    public VideoId VideoId { get; private set; } = default!;

    #region Private

    private VideoPlaylist()
    {

    }

    #endregion
}