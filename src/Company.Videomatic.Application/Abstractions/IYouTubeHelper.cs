namespace Company.Videomatic.Application.Abstractions;

/// <summary>
/// This niterface provides access to the YouTube APIs we are interested in.
/// </summary>
public interface IYouTubeHelper
{
    /// <summary>
    /// Returns all the videos in a given YouTube playlist.
    /// </summary>
    /// <param name="playlistId"></param>
    /// <returns></returns>
    IAsyncEnumerable<Video> ImportVideosOfPlaylist(string playlistId);// TODO: add cancellation  


    // TODO: add cancellation    
    IAsyncEnumerable<Video> ImportVideos(IEnumerable<string> youtubeVideoIds);
    // TODO: add cancellation
    IAsyncEnumerable<Transcript> ImportTranscriptions(IEnumerable<VideoId> videoIds);    
}