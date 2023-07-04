namespace Company.Videomatic.Application.Abstractions;

public interface IVideoService
{
    Task<int> LinkToPlaylists(VideoId videoId,
                              PlaylistId[] playlistIds,
                              CancellationToken cancellationToken = default);
}
