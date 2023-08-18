using Domain.Videos;

namespace Domain.Playlists;

public class PlaylistVideo : ValueObject
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

    public override string ToString()
    {
        return $"{PlaylistId} {VideoId}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PlaylistId;
        yield return VideoId;
    }

    #endregion
}