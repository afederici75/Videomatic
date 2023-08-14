using Application.Model;

namespace Application.Abstractions;

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

