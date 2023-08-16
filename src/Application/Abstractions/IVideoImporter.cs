using Domain.Videos;

namespace Application.Abstractions;

public readonly record struct ImportOptions(bool ExecuteImmediate = false);

/// <summary>
/// Funtionality to import videos, playlists and channels from video providers.
/// </summary>
public interface IVideoImporter
{
    Task ImportPlaylistsAsync(IEnumerable<string> idOrUrls, ImportOptions? options = default, CancellationToken cancellation = default);

    Task ImportVideosAsync(IEnumerable<string> idOrUrls, PlaylistId? linkTo = null, ImportOptions? options = default, CancellationToken cancellation = default);

    Task ImportVideosAsync(PlaylistId playlistId, ImportOptions? options = default, CancellationToken cancellation = default);

    Task ImportTranscriptionsAsync(IEnumerable<VideoId> videoIds, CancellationToken cancellation = default);
}