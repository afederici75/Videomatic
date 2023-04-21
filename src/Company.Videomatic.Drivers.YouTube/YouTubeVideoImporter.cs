using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using System.Web;

namespace Company.Videomatic.Drivers.YouTube;

public class YouTubeVideoImporter : IVideoImporter
{
    private readonly YouTubeOptions _options;

    public YouTubeVideoImporter(IOptions<YouTubeOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<Video> Import(Uri location)
    {
        if (location == null)
        {
            throw new ArgumentNullException(nameof(location));
        }

        // Extract the video ID from the YouTube URL
        var videoId = ExtractVideoId(location);

        // Call the YouTube API to get information about the video
        var youtubeService = new YouTubeService(new BaseClientService.Initializer
        {
            ApiKey = _options.ApiKey,
            ApplicationName = _options.ApplicationName
        });

        var videoRequest = youtubeService.Videos.List("snippet");
        videoRequest.Id = videoId;
        var videoResponse = await videoRequest.ExecuteAsync();

        if (videoResponse.Items.Count == 0)
        {
            throw new Exception($"No video found with ID {videoId}");
        }

        var videoItem = videoResponse.Items[0];
        var video = new Video
        {
            Id = videoId,
            VideoUrl = location.ToString(),
            Title = videoItem.Snippet.Title,
            Description = videoItem.Snippet.Description,
            OriginalDescription = videoItem.Snippet.Description,
            ThumbnailUrl = videoItem.Snippet.Thumbnails.Default__.Url
        };

        return video;
    }

    private string ExtractVideoId(Uri location)
    {
        var queryString = HttpUtility.ParseQueryString(location.Query);
        var videoId = queryString["v"];
        if (string.IsNullOrWhiteSpace(videoId))
        {
            throw new ArgumentException("Invalid YouTube URL: missing video ID", nameof(location));
        }
        return videoId;
    }
}
