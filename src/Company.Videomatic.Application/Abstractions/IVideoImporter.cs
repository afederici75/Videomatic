namespace Company.Videomatic.Application.Abstractions;

public interface IVideoImporter
{
    IAsyncEnumerable<Playlist> ImportPlaylistsAsync(IEnumerable<string> idOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<Video> ImportVideosAsync(IEnumerable<string> idOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<Transcript> ImportTranscriptionsAsync(IEnumerable<VideoId> videoIds, CancellationToken cancellation = default);
}

public static class IVideoImporterExtensions
{
    public static async Task<Result<Video>> ImportVideoAsync(this IVideoImporter instance, string idOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.ImportVideosAsync(new[] { idOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }

    public static async Task<Result<Playlist>> ImportPlaylistAsync(this IVideoImporter instance, string idOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.ImportPlaylistsAsync(new[] { idOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }
}