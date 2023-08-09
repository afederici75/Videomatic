using Company.Videomatic.Application.Model;

namespace Company.Videomatic.Application.Abstractions;

/// <summary>
/// This interface provides access to the YouTube APIs we are interested in.
/// </summary>
public interface IVideoProvider
{
    IAsyncEnumerable<GenericChannel> GetChannelsAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<GenericPlaylist> GetPlaylistsAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);
    
    IAsyncEnumerable<GenericVideo> GetVideosAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<GenericVideo> GetVideosOfPlaylistAsync(string playlistIdOrUrl, CancellationToken cancellation = default);
}

public static class IVideoHostingProviderExtensions
{
    public static async Task<Result<GenericPlaylist>> GetPlaylistAsync(this IVideoProvider instance, string idsOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.GetPlaylistsAsync(new[] { idsOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }

    public static async Task<Result<GenericChannel>> GetChannelAsync(this IVideoProvider instance, string idsOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.GetChannelsAsync(new[] { idsOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }

    public static async Task<Result<GenericVideo>> GetVideoAsync(this IVideoProvider instance, string idsOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.GetVideosAsync(new[] { idsOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }
}

