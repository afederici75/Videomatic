using Company.Videomatic.Application.Model;

namespace Company.Videomatic.Application.Abstractions;

/// <summary>
/// This interface provides access to the YouTube APIs we are interested in.
/// </summary>
public interface IVideoProvider
{
    IAsyncEnumerable<GenericChannel> GetChannelInformationAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<GenericPlaylist> GetPlaylistInformationAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<GenericVideo> GetVideoInformationAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);
}

public static class IVideoHostingProviderExtensions
{
    public static async Task<Result<GenericPlaylist>> GetPlaylistInformationAsync(this IVideoProvider instance, string idsOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.GetPlaylistInformationAsync(new[] { idsOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }

    public static async Task<Result<GenericChannel>> GetChannelInformationAsync(this IVideoProvider instance, string idsOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.GetChannelInformationAsync(new[] { idsOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }

    public static async Task<Result<GenericVideo>> GetVideoInformationAsync(this IVideoProvider instance, string idsOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.GetVideoInformationAsync(new[] { idsOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }
}

