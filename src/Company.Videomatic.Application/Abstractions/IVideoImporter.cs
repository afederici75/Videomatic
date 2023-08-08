namespace Company.Videomatic.Application.Abstractions;

public interface IVideoImporter
{
    IAsyncEnumerable<Video> ImportVideos(IEnumerable<string> idOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<Video> ImportVideosOfPlaylist(string playlistId, CancellationToken cancellation = default);

    IAsyncEnumerable<Transcript> ImportTranscriptions(IEnumerable<VideoId> videoIds, CancellationToken cancellation = default);
}