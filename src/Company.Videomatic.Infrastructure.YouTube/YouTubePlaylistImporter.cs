using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain.Model;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Options;
using System.Web;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubePlaylistImporter : IPlaylistImporter
{
    private readonly YouTubeOptions _options;
    private readonly IVideoImporter _importer;

    public YouTubePlaylistImporter(IOptions<YouTubeOptions> options, IVideoImporter importer)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _importer = importer ?? throw new ArgumentNullException(nameof(importer));
    }

    public async Task<Collection> ImportAsync(Uri location)
    {
        // Extract the video ID from the YouTube URL (e.g. 'dQw4w9WgXcQ' from https://www.youtube.com/watch?v=dQw4w9WgXcQ)
        var playlistId = ExtractPlaylistId(location);

        var youtubeService = new YouTubeService(new BaseClientService.Initializer
        {
            ApiKey = _options.ApiKey,
            ApplicationName = _options.ApplicationName
        });

        // https://developers.google.com/youtube/v3/docs/playlists/list
        var playlistRequest = youtubeService.Playlists.List("snippet,contentDetails");
        playlistRequest.Id = playlistId;
        
        var playlistResponse = await playlistRequest.ExecuteAsync();

        Playlist actual = playlistResponse.Items.First();
        var actualPlaylist = playlistResponse.Items.First().Snippet;
        var result = new Collection(
            actualPlaylist.Title,
            $"https://www.youtube.com/playlist?list={location.ToString()}");


        // https://developers.google.com/youtube/v3/docs/playlistItems
        string nextPageToken = string.Empty;
        do
        {
            var playlistItemsRequest = youtubeService.PlaylistItems.List("snippet,contentDetails,id,status");
            playlistItemsRequest.PlaylistId = playlistId;
            playlistItemsRequest.MaxResults = 50;
            playlistItemsRequest.PageToken = nextPageToken;

            var playlistResp = await playlistItemsRequest.ExecuteAsync();

            nextPageToken = playlistResp.NextPageToken; // Ready for next

            var publishedVideos = playlistResp.Items
                .Where(v => v.ContentDetails.VideoPublishedAt is not null); // The video has been removed or similar.

            var requests = playlistResp.Items.Select(v => ImportVideoAsync(v));            
            Domain.Model.Video[] videos = await Task.WhenAll(requests);
            
            result.AddVideos(videos);

        } while (!string.IsNullOrEmpty(nextPageToken));
        

        return result;
    }

    private async Task<Domain.Model.Video> ImportVideoAsync(PlaylistItem? v)
    {
        Domain.Model.Video newVideo;
        try
        {
            newVideo = await _importer.ImportAsync(new Uri($"https://www.youtube.com/watch?v={v.ContentDetails.VideoId}"));            
        }
        catch (Exception ex)
        {
            newVideo = new Domain.Model.Video(
                "YOUTUBE",
                v.ContentDetails.VideoId,
                $"https://www.youtube.com/watch?v={v.ContentDetails.VideoId}");
        }

        return newVideo;
    }

    private string ExtractPlaylistId(Uri location)
    {
        if (location is null)
            throw new ArgumentNullException(nameof(location));

        // https://www.youtube.com/playlist?list=PLLdi1lheZYVKkvX20ihB7Ay2uXMxa0Q5e
        var queryString = HttpUtility.ParseQueryString(location.Query);                
        var videoId = queryString["list"];
        if (string.IsNullOrWhiteSpace(videoId))
        {
            throw new ArgumentException("Invalid YouTube playlist URL: missing playlist ID", nameof(location));
        }
        return videoId;
    }
}