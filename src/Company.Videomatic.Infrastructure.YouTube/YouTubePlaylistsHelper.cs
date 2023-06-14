using Company.Videomatic.Infrastructure.YouTube.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Transactions;
using YoutubeTranscriptApi;
using static System.Net.WebRequestMethods;

namespace Company.Videomatic.Infrastructure.YouTube;

public record Playlist(string Id, string Title, string Description);

public record Video(string Id,string Title, string Description);

public record TranscriptionItem(string Text, float Start, float duration);

public interface IPlaylistsHelper
{
    IAsyncEnumerable<Playlist> GetPlaylists(string channelId);
    IAsyncEnumerable<Video> GetVideosOfPlaylist(string playlistId);
    IAsyncEnumerable<TranscriptionItem> GetTranscriptionOfVideo(string videoId);
}

public class YouTubePlaylistsHelper : IPlaylistsHelper
{
    public YouTubePlaylistsHelper(IOptions<YouTubeOptions> options, HttpClient client)
    {
        _options = options.Value;
        _client = client;
        _client.BaseAddress = new Uri("https://www.googleapis.com/youtube/v3/");
    }

    readonly YouTubeOptions _options;
    readonly HttpClient _client;

    public async IAsyncEnumerable<Playlist> GetPlaylists(string channelId)
    {
        var parameters = new Dictionary<string, string>
        {
            ["key"] = _options.ApiKey,
            ["channelId"] = channelId,
            ["part"] = "snippet,contentDetails",
            //["fields"] = "pageInfo, items/snippet(title, description)",
            ["maxResults"] = "50"
        };

        GetPlaylistsResponse response;
        do
        {
            var fullUrl = MakeUrlWithQuery("playlists", parameters);

            string json = await _client.GetStringAsync(fullUrl);
            response = JsonConvert.DeserializeObject<GetPlaylistsResponse>(json)!;

            foreach (var item in response.items)
            {
                yield return new Playlist(item.id, item.snippet.title, item.snippet.description);
            }
            parameters["pageToken"] = response.nextPageToken;
        }
        while (!string.IsNullOrEmpty(response.nextPageToken));
    }

    public async IAsyncEnumerable<Video> GetVideosOfPlaylist(string playlistId)
    {
        var parameters = new Dictionary<string, string>
        {
            ["key"] = _options.ApiKey,
            ["playlistId"] = playlistId,
            ["part"] = "snippet,contentDetails,id,status",
            //["fields"] = "pageInfo, items/snippet(title, description)",
            ["maxResults"] = "50"
        };

        GetPlaylistItemsResponse response;
        do
        {
            var fullUrl = MakeUrlWithQuery("playlistItems", parameters);

            string json = await _client.GetStringAsync(fullUrl);
            response = JsonConvert.DeserializeObject<GetPlaylistItemsResponse>(json)!;

            foreach (var item in response.items)
            {
                yield return new Video(item.id, item.snippet.title, item.snippet.description);
            }
            parameters["pageToken"] = response.nextPageToken;
        }
        while (!string.IsNullOrEmpty(response.nextPageToken));
    }    

    public async IAsyncEnumerable<TranscriptionItem> GetTranscriptionOfVideo(string videoId)
    {
        using (var api = new YouTubeTranscriptApi())
        {
            var items = api.GetTranscript(videoId);
            foreach (var item in items)
            {
                yield return new(item.Text, item.Start, item.Duration);
            }
        }
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