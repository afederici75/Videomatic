namespace Company.Videomatic.Infrastructure.YouTube;

/// <summary>
/// This niterface provides access to the YouTube APIs we are interested in.
/// </summary>
public interface IYouTubeHelper
{
    /// <summary>
    /// Returns all the play lists of a given YouTube channel.
    /// </summary>
    /// <param name="channelId"></param>
    /// <returns></returns>
    IAsyncEnumerable<Playlist> GetPlaylistsByChannel(string channelId);

    /// <summary>
    /// Returns all the videos in a given YouTube playlist.
    /// </summary>
    /// <param name="playlistId"></param>
    /// <returns></returns>
    IAsyncEnumerable<Video> GetVideosOfPlaylist(string playlistId);

    /// <summary>
    /// Gets the full transcription of a YouTube video.
    /// </summary>
    /// <param name="videoId"></param>
    /// <returns></returns>
    IAsyncEnumerable<TranscriptionItem> GetTranscriptionOfVideo(string videoId);
}