using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using Company.Videomatic.Drivers.YouTube.Options;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using System.Web;
using YoutubeTranscriptApi;

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
        var video = new Video
        {
            ProviderId = videoId,
            VideoUrl = location.ToString(),
            Title = videoItem.Snippet.Title,
            Description = videoItem.Snippet.Description,
            Thumbnails = ImportThumbnails(videoItem.Snippet.Thumbnails),
            Transcript = ImportTranscript(videoId)
        };

        return video;
    }

    private Domain.Transcript ImportTranscript(string videoId)
    {
        // Retrieve the captions for the video
        using (var youTubeTranscriptApi = new YouTubeTranscriptApi())
        {
            var transcriptItems = youTubeTranscriptApi.GetTranscript(videoId);
            
            var newLines = transcriptItems
                .Select(ti => new TranscriptLine
                {                
                    Text = ti.Text,
                    Duration = TimeSpan.FromSeconds(ti.Duration),
                    StartsAt = TimeSpan.FromSeconds(ti.Start) // There can be 'mismatches': https://github.com/jdepoix/youtube-transcript-api/issues/21
                })
                .ToList();

            return new Domain.Transcript()
            {
                Lines = newLines
            };
        }
    }   

    IList<Thumbnail> ImportThumbnails(Google.Apis.YouTube.v3.Data.ThumbnailDetails thumbnails)
    {
        // create a thumbnail for each resolution
        var thumbnailList = new List<Thumbnail>();
        if (thumbnails.Default__ != null)
        {
            thumbnailList.Add(new Thumbnail
            {
                Resolution = ThumbnailResolution.Default,
                Url = thumbnails.Default__.Url,
                Height = Convert.ToInt32(thumbnails.Default__.Height),
                Width = Convert.ToInt32(thumbnails.Default__.Width),
            });
        }
        
        if (thumbnails.High != null)
        {
            thumbnailList.Add(new Thumbnail
            {
                Resolution = ThumbnailResolution.High,
                Url = thumbnails.High.Url,
                Height = Convert.ToInt32(thumbnails.High.Height),
                Width = Convert.ToInt32(thumbnails.High.Width),
            });
        }

        if (thumbnails.Medium != null)
        {
            thumbnailList.Add(new Thumbnail
            {
                Resolution = ThumbnailResolution.Medium,
                Url = thumbnails.Medium.Url,
                Height = Convert.ToInt32(thumbnails.Medium.Height),
                Width = Convert.ToInt32(thumbnails.Medium.Width),
            });
        }

            
        if (thumbnails.Standard != null)
        {
            thumbnailList.Add(new Thumbnail
            {
                Resolution = ThumbnailResolution.Standard,
                Url = thumbnails.Standard.Url,
                Height = Convert.ToInt32(thumbnails.Standard.Height),
                Width = Convert.ToInt32(thumbnails.Standard.Width),
            });
        }

        if (thumbnails.Maxres != null)
        {
            thumbnailList.Add(new Thumbnail
            {
                Resolution = ThumbnailResolution.MaxRes,
                Url = thumbnails.Maxres.Url,
                Height = Convert.ToInt32(thumbnails.Maxres.Height),
                Width = Convert.ToInt32(thumbnails.Maxres.Width),
            });
        }

        return thumbnailList;       
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
