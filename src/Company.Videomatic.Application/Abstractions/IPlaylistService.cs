namespace Company.Videomatic.Application.Abstractions;

public interface IPlaylistService
{
    Task<Result<int>> LinkPlaylistToVideos(PlaylistId playlistId, IEnumerable<VideoId> videoIds, CancellationToken cancellationToken = default);
}
