using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Application.Abstractions;

public interface IVideoService
{
    Task<int> LinkToPlaylists(VideoId videoId, PlaylistId[] playlistIds, CancellationToken cancellationToken = default);
}
