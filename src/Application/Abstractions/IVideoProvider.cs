using Application.Model;

namespace Application.Abstractions;

/// <summary>
/// Funtionality to get videos, playlists and channels from a video provider.
/// </summary>
public interface IVideoProvider
{
    IAsyncEnumerable<GenericChannel> GetChannelsAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<GenericPlaylist> GetPlaylistsAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);
    
    IAsyncEnumerable<GenericVideo> GetVideosAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<GenericVideo> GetVideosOfPlaylistAsync(string playlistIdOrUrl, CancellationToken cancellation = default);
}

