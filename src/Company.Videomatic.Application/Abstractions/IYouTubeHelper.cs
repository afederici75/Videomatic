using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Application.Features.Videos;

namespace Company.Videomatic.Application.Abstractions;

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
    IAsyncEnumerable<PlaylistDTO> GetPlaylistsByChannel(string channelId);

    /// <summary>
    /// Returns all the videos in a given YouTube playlist.
    /// </summary>
    /// <param name="playlistId"></param>
    /// <returns></returns>
    IAsyncEnumerable<VideoDTO> GetAllVideosOfPlaylist(string playlistId);

    /// <summary>
    /// Gets the full transcription of a YouTube video.
    /// </summary>
    /// <param name="videoId"></param>
    /// <returns></returns>
    //IAsyncEnumerable<Domain.Model.Transcript> GetTranscriptionOfVideo(string videoId);

    //
    IAsyncEnumerable<Video> ImportVideoDetails(string[] youtubeVideoIds);
}