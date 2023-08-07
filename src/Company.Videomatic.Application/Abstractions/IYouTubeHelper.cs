namespace Company.Videomatic.Application.Abstractions;

/// <summary>
/// This interface provides access to the YouTube APIs we are interested in.
/// </summary>
public interface IYouTubeHelper
{
    IAsyncEnumerable<PlaylistDTO> GetPlaylistsOfChannel(string channelId);
    Task<IEnumerable<string>> GetPlaylistVideoIds(string playlistId);
    Task<PlaylistInfo> GetPlaylistInformation(string playlistId);
}

public record PlaylistInfo(string Id, string Name, string Description);