using Application.Model;

namespace Application.Abstractions;

/// <summary>
/// Funtionality to get videos, playlists and channels from a video provider.
/// </summary>
public interface IVideoProvider
{
    IAsyncEnumerable<GenericChannelDTO> GetChannelsAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<GenericPlaylistDTO> GetPlaylistsAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);
    
    IAsyncEnumerable<GenericVideoDTO> GetVideosAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<GenericVideoDTO> GetVideosOfPlaylistAsync(string playlistIdOrUrl, CancellationToken cancellation = default);
}

