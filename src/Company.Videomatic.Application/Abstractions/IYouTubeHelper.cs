namespace Company.Videomatic.Application.Abstractions;

/// <summary>
/// This niterface provides access to the YouTube APIs we are interested in.
/// </summary>
public interface IYouTubeHelper
{
    IAsyncEnumerable<PlaylistDTO> GetPlaylistsOfChannel(string channelId); // TODO: add cancellation  

    IAsyncEnumerable<Video> ImportVideos(IEnumerable<string> idOrUrls, CancellationToken cancellation = default); // TODO: add cancellation  

    IAsyncEnumerable<Video> ImportVideosOfPlaylist(string playlistId); // TODO: add cancellation  

    //IAsyncEnumerable<Video> ImportVideosById(IEnumerable<string> youtubeVideoIds); // TODO: add cancellation    
    //IAsyncEnumerable<Video> ImportVideosByUrl(IEnumerable<string> youtubeVideoUrls); // TODO: should be an extension method

    Task<IEnumerable<string>> GetPlaylistVideoIds(string playlistId);
    IAsyncEnumerable<Transcript> ImportTranscriptions(IEnumerable<VideoId> videoIds); // TODO: add cancellation    

    Task<PlaylistInfo> GetPlaylistInformation(string playlistId);
}

public record PlaylistInfo(string Id, string Name, string Description);