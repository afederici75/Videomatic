using Company.Videomatic.Application.Features.Transcripts;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Aggregates.Video;
using Company.Videomatic.Infrastructure.YouTube.API.JsonPasteSpecial;
using Newtonsoft.Json;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubePlaylistsHelper : IYouTubeHelper
{
    public const string ProviderId = "YOUTUBE";

    public YouTubePlaylistsHelper(IOptions<YouTubeOptions> options, HttpClient client)
    {
        _options = options.Value;
        _client = client;
        _client.BaseAddress = new Uri("https://www.googleapis.com/youtube/v3/");
    }

    readonly YouTubeOptions _options;
    readonly HttpClient _client;
    
    public async IAsyncEnumerable<Video> ImportVideosOfPlaylist(string playlistId)
    {
        var parameters = new Dictionary<string, string>
        {
            ["key"] = _options.ApiKey,
            ["playlistId"] = playlistId,
            //["part"] = "snippet,contentDetails,id,status",
            ["part"] = "contentDetails,status",
            ["maxResults"] = "50"
        };

        API.JsonPasteSpecial.PlaylistItemListResponse response;
        do
        {
            // API call
            var fullUrl = MakeUrlWithQuery("playlistItems", parameters);
            string json = await _client.GetStringAsync(fullUrl);
            response = JsonConvert.DeserializeObject<API.JsonPasteSpecial.PlaylistItemListResponse>(json)!;

            // Gets all video Ids
            var publicVideos = response.items.Where(i => i.status.privacyStatus == "public")
                                             .Select(i => i.contentDetails.videoId);

            var videoIds = new HashSet<string>(publicVideos);
            
            await foreach (var video in ImportVideos(videoIds.ToArray()))
            {
                yield return video;
            };

            // Pages
            parameters["pageToken"] = response.nextPageToken;
        }
        while (!string.IsNullOrEmpty(response.nextPageToken));
    }

    // ------------------------------

    public async IAsyncEnumerable<Video> ImportVideos(IEnumerable<string> youtubeVideoIds)
    {
        var parameters = new Dictionary<string, string>
        {
            ["key"] = _options.ApiKey,
            ["id"] = string.Join(',', youtubeVideoIds),
            ["part"] = "snippet,contentDetails,id,status",
            //["maxResults"] = "50"
        };

        API.JsonPasteSpecial.VideoListResponse response;
        var fullUrl = MakeUrlWithQuery("videos", parameters);

        string json = await _client.GetStringAsync(fullUrl);
        response = JsonConvert.DeserializeObject<VideoListResponse>(json)!;

        foreach (var item in response.items)
        {
            var video = Video.Create(
                location: $"https://www.youtube.com/watch?v={item.id}",
                name: item.snippet.title,
                description: item.snippet.description,
                details: new VideoDetails(
                    Provider: ProviderId,
                    ProviderVideoId: item.id,
                    VideoPublishedAt: item.snippet.publishedAt,
                    VideoOwnerChannelTitle: item.snippet.channelTitle,
                    VideoOwnerChannelId: item.snippet.channelId));

            var tags = (item.snippet.tags ?? Enumerable.Empty<string>()).ToArray();            
            video.AddTags(tags);

            var t = item.snippet.thumbnails;

            video.SetThumbnail(ThumbnailResolution.Default, t.@default?.url ?? "", t.@default?.height ?? -1, t.@default?.width ?? -1);
            video.SetThumbnail(ThumbnailResolution.High, t.high?.url ?? "", t.high?.height ?? -1, t.high?.width ?? -1);
            video.SetThumbnail(ThumbnailResolution.Standard, t.standard?.url ?? "", t.standard?.height ?? -1, t.standard?.width ?? -1);
            video.SetThumbnail(ThumbnailResolution.Medium, t.medium?.url ?? "", t.medium?.height ?? -1, t.medium?.width ?? -1);
            video.SetThumbnail(ThumbnailResolution.MaxRes, t.maxres?.url ?? "", t.maxres?.height ?? -1, t.maxres?.width ?? -1);

            yield return video;
        }
    }

    void SetVideoThumbnails(Video video, VideoListResponse.Thumbnails thumbnails)
    {
        ThumbnailResolution resolution;
                
    }

    public async Task ImportVideoTranscript(Video[] videos)
    {
        throw new NotImplementedException();
        //using (var api = new YoutubeTranscriptApi.YouTubeTranscriptApi())
        //{
        //    videos.Select(v => v.Details. Id);
        //    
        //    var allTranscripts = api.GetTranscripts(youtubeVideoIds); // Returns an ugly (Dictionary<string, IEnumerable<YoutubeTranscriptApi.TranscriptItem>>, IReadOnlyList<string>) 
        //
        //    foreach (var t in allTranscripts.Item1)
        //    {
        //        IEnumerable<YoutubeTranscriptApi.TranscriptItem> items = api.GetTranscript(videoId);
        //        
        //        var result = Transcript.Create(videoId, t.Key);
        //        
        //        foreach (var item in items)
        //        {
        //            yield return new(item.Text, item.Start, item.Duration);
        //        }
        //    }            
        //}
    }

    static string MakeUrlWithQuery(string endpoint,
            IEnumerable<KeyValuePair<string, string>> parameters)
    {
        if (string.IsNullOrEmpty(endpoint))
            throw new ArgumentNullException(nameof(endpoint));

        if (parameters == null || parameters.Count() == 0)
            return endpoint;

        return parameters.Aggregate(endpoint + '?',
            (accumulated, kvp) => string.Format($"{accumulated}{kvp.Key}={kvp.Value}&"));
    }
}