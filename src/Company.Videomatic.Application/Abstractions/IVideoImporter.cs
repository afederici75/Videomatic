namespace Company.Videomatic.Application.Abstractions;

public interface IVideoImporter
{
    Task ImportPlaylistsAsync(IEnumerable<string> idOrUrls, CancellationToken cancellation = default);

    Task ImportVideosAsync(IEnumerable<string> idOrUrls, CancellationToken cancellation = default);

    Task ImportVideosAsync(PlaylistId playlistId, CancellationToken cancellation = default);

    IAsyncEnumerable<Transcript> ImportTranscriptionsAsync(IEnumerable<VideoId> videoIds, CancellationToken cancellation = default);
}