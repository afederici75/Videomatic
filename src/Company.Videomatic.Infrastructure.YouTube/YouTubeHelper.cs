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

    public async IAsyncEnumerable<PlaylistDTO> GetPlaylistsByChannel(string channelId)
    {
        var parameters = new Dictionary<string, string>
        {
            ["key"] = _options.ApiKey,
            ["channelId"] = channelId,
            ["part"] = "snippet,contentDetails",
            ["fields"] = "pageInfo, items/snippet(title, description)",
            ["maxResults"] = "50"
        };

        API.JsonPasteSpecial.PlaylistListResponse response;
        do
        {
            var fullUrl = MakeUrlWithQuery("playlists", parameters);

            string json = await _client.GetStringAsync(fullUrl);
            response = JsonConvert.DeserializeObject<API.JsonPasteSpecial.PlaylistListResponse>(json)!;

            foreach (var item in response.items)
            {
                yield return new PlaylistDTO(Name: item.snippet.title, Description: item.snippet.description, VideoCount: -1); // TODO: fix video count
            }
            parameters["pageToken"] = response.nextPageToken;
        }
        while (!string.IsNullOrEmpty(response.nextPageToken));
    }

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
            
            await foreach (var video in ImportVideoDetails(videoIds.ToArray()))
            {
                yield return video;
            };

            // Pages
            parameters["pageToken"] = response.nextPageToken;
        }
        while (!string.IsNullOrEmpty(response.nextPageToken));
    }

    // ------------------------------

    public async IAsyncEnumerable<Video> ImportVideoDetails(IEnumerable<string> youtubeVideoIds)
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

            video.SetThumbnail(ThumbnailResolution.Default, item.snippet.thumbnails.@default.url, item.snippet.thumbnails.@default.height, item.snippet.thumbnails.@default.width);
            video.SetThumbnail(ThumbnailResolution.High, item.snippet.thumbnails.high.url, item.snippet.thumbnails.high.height, item.snippet.thumbnails.high.width);
            video.SetThumbnail(ThumbnailResolution.Standard, item.snippet.thumbnails.standard.url, item.snippet.thumbnails.standard.height, item.snippet.thumbnails.standard.width);
            video.SetThumbnail(ThumbnailResolution.Medium, item.snippet.thumbnails.medium.url, item.snippet.thumbnails.medium.height, item.snippet.thumbnails.medium.width);
            video.SetThumbnail(ThumbnailResolution.MaxRes, item.snippet.thumbnails.maxres.url, item.snippet.thumbnails.maxres.height, item.snippet.thumbnails.maxres.width);

            yield return video;
        }
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