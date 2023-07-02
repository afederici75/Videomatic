using Company.Videomatic.Domain.Entities.VideoAggregate;

namespace Company.Videomatic.Domain.Entities.PlaylistAggregate;

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