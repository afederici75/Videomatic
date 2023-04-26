using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using Company.Videomatic.Infrastructure.YouTube.Options;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using System.Web;
using YoutubeTranscriptApi;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubeVideoImporter : IVideoImporter
{
    public const string ProviderId = "YOUTUBE";

    private readonly YouTubeOptions _options;

    public YouTubeVideoImporter(IOptions<YouTubeOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }
    public async Task<Video> ImportAsync(Uri location)
    {
        // Extract the video ID from the YouTube URL (e.g. 'dQw4w9WgXcQ' from https://www.youtube.com/watch?v=dQw4w9WgXcQ)
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

        // Create and return the Video object
        var video = new Video(
            providerId: ProviderId,
            providerVideoId: videoId,   
            videoUrl: location.ToString(),
            title: videoItem.Snippet.Title,
            description: videoItem.Snippet.Description);
        
        video
            .AddThumbnails(ImportThumbnails(video, videoItem.Snippet.Thumbnails))
            .AddTranscripts(ImportTranscript(video));

        return video;
    }

    private Domain.Transcript ImportTranscript(Video video)
    {
        // Retrieve the captions for the video
        using (var youTubeTranscriptApi = new YouTubeTranscriptApi())
        {
            var transcriptItems = youTubeTranscriptApi.GetTranscript(video.ProviderVideoId);
            
            var transcript = new Domain.Transcript(language: "US");
            
            var lines = transcriptItems
                .Select(ti => new TranscriptLine(
                    text: ti.Text,
                    startsAt: TimeSpan.FromSeconds(ti.Start),
                    duration: TimeSpan.FromSeconds(ti.Duration)))
                .ToArray();

            transcript.AddLines(lines);

            return transcript;
        }
    }   

    Thumbnail[] ImportThumbnails(Video video, Google.Apis.YouTube.v3.Data.ThumbnailDetails thumbnails)
    {
        // create a thumbnail for each resolution
        var thumbnailList = new List<Thumbnail>();
        if (thumbnails.Default__ != null)
        {
            thumbnailList.Add(new Thumbnail(
                resolution: ThumbnailResolution.Default,
                url: thumbnails.Default__.Url,
                height: Convert.ToInt32(thumbnails.Default__.Height),
                width: Convert.ToInt32(thumbnails.Default__.Width)));
        }
        
        if (thumbnails.High != null)
        {
            thumbnailList.Add(new Thumbnail(
                resolution: ThumbnailResolution.High,
                url: thumbnails.High.Url,
                height: Convert.ToInt32(thumbnails.High.Height),
                width: Convert.ToInt32(thumbnails.High.Width)));
        }

        if (thumbnails.Medium != null)
        {
            thumbnailList.Add(new Thumbnail(
                resolution: ThumbnailResolution.Medium,
                url: thumbnails.Medium.Url,
                height: Convert.ToInt32(thumbnails.Medium.Height),
                width: Convert.ToInt32(thumbnails.Medium.Width)));
        }

            
        if (thumbnails.Standard != null)
        {
            thumbnailList.Add(new Thumbnail(
                resolution: ThumbnailResolution.Standard,
                url: thumbnails.Standard.Url,
                height: Convert.ToInt32(thumbnails.Standard.Height),
                width: Convert.ToInt32(thumbnails.Standard.Width)));
        }

        if (thumbnails.Maxres != null)
        {
            thumbnailList.Add(new Thumbnail(
                resolution: ThumbnailResolution.MaxRes,
                url: thumbnails.Maxres.Url,
                height:  Convert.ToInt32(thumbnails.Maxres.Height),
                width:  Convert.ToInt32(thumbnails.Maxres.Width)));
        }

        return thumbnailList.ToArray();       
    }

    private string ExtractVideoId(Uri location)
    {
        if (location is null)
            throw new ArgumentNullException(nameof(location));        

        var queryString = HttpUtility.ParseQueryString(location.Query);
        var videoId = queryString["v"];
        if (string.IsNullOrWhiteSpace(videoId))
        {
            throw new ArgumentException("Invalid YouTube URL: missing video ID", nameof(location));
        }
        return videoId;
    }
}
