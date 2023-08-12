using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Domain.Aggregates.Playlist;

public class PlaylistVideo
{
    public PlaylistVideo(PlaylistId playlistId, VideoId videoId)
    {        
        PlaylistId = playlistId; // It's possible the Playlist Id is not yet available (e.g. we just created the Playlist)
        VideoId = Guard.Against.Null(videoId, nameof(videoId));        
    }

    public PlaylistId PlaylistId { get; private set; } = default!;
    public VideoId VideoId { get; private set; } = default!;

    #region Private

    private PlaylistVideo() { }

    #endregion
}