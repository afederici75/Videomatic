using Company.Videomatic.Domain.Video;

namespace Company.Videomatic.Application.Abstractions;

public record ImportOptions(bool ExecuteWithoutJobQueue = false);

public interface IVideoImporter
{
    Task ImportPlaylistsAsync(IEnumerable<string> idOrUrls, ImportOptions? options = default, CancellationToken cancellation = default);

    Task ImportVideosAsync(IEnumerable<string> idOrUrls, PlaylistId? linkTo = null, ImportOptions? options = default, CancellationToken cancellation = default);

    Task ImportVideosAsync(PlaylistId playlistId, ImportOptions? options = default, CancellationToken cancellation = default);

    Task ImportTranscriptionsAsync(IEnumerable<VideoId> videoIds, CancellationToken cancellation = default);
}