﻿using Company.Videomatic.Domain.Videos;

namespace Company.Videomatic.Domain.Playlists;

public class PlaylistVideo
{
    public static PlaylistVideo Create(PlaylistId playlistId, VideoId videoId)
    {
        return new PlaylistVideo
        {
            PlaylistId = playlistId,
            VideoId = videoId
        };
    }

    public PlaylistId PlaylistId { get; private set; } = default!;
    public VideoId VideoId { get; private set; } = default!;

    public Videos.Video Video { get; private set; } = default!;
    public Playlist Playlist { get; private set; } = default!;

    #region Private

    private PlaylistVideo()
    {
            
    }

    #endregion
}