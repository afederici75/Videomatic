using Newtonsoft.Json;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubePlaylistsHelper : IYouTubeHelper
{
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

        API.JsonPasteSpecial.GetPlaylistsResponse response;
        do
        {
            var fullUrl = MakeUrlWithQuery("playlists", parameters);

            string json = await _client.GetStringAsync(fullUrl);
            response = JsonConvert.DeserializeObject<API.JsonPasteSpecial.GetPlaylistsResponse>(json)!;

            foreach (var item in response.items)
            {
                yield return new PlaylistDTO(Name: item.snippet.title, Description: item.snippet.description, VideoCount: -1); // TODO: fix video count
            }
            parameters["pageToken"] = response.nextPageToken;
        }
        while (!string.IsNullOrEmpty(response.nextPageToken));
    }

    public async IAsyncEnumerable<VideoDTO> GetVideosOfPlaylist(string playlistId)
    {
        var parameters = new Dictionary<string, string>
        {
            ["key"] = _options.ApiKey,
            ["playlistId"] = playlistId,
            ["part"] = "snippet,contentDetails,id,status",
            ["fields"] = "pageInfo, items/snippet(title, description)",
            ["maxResults"] = "50"
        };

        API.JsonPasteSpecial.GetPlaylistItemsResponse response;
        do
        {
            var fullUrl = MakeUrlWithQuery("playlistItems", parameters);

            string json = await _client.GetStringAsync(fullUrl);
            response = JsonConvert.DeserializeObject<API.JsonPasteSpecial.GetPlaylistItemsResponse>(json)!;

            foreach (var item in response.items)
            {
                yield return new VideoDTO(Id: 0, Location: item.id, Title: item.snippet.title, Description: item.snippet.description);
            }
            parameters["pageToken"] = response.nextPageToken;
        }
        while (!string.IsNullOrEmpty(response.nextPageToken));
    }    

    //public IAsyncEnumerable<Transcript> GetTranscriptionOfVideo(string videoId)
    //{
    //    throw new Exception();
    //    //using (var api = new API.TranscriptAPI.YouTubeTranscriptApi())
    //    //{
    //    //    (Dictionary<string, IEnumerable<API.TranscriptAPI.TranscriptItem>>, IReadOnlyList<string>) allTranscripts = api.GetTranscripts(new[] { videoId });
    //    //    foreach (var t in allTranscripts)
    //    //    {
    //    //        var items = api.GetTranscript(videoId);
    //    //        foreach (var item in items)
    //    //        {
    //    //            yield return new(item.Text, item.Start, item.Duration);
    //    //        }
    //    //    }            
    //    //}
    //}

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