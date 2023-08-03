namespace Company.Videomatic.Application.Abstractions;

public interface IPlaylistService
{
    Task<Result<int>> LinkToPlaylists(PlaylistId playlistId,
                                      IEnumerable<VideoId> videoIds,
                                      CancellationToken cancellationToken = default);
}
