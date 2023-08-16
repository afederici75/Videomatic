using Google.Apis.YouTube.v3;
using Infrastructure.YouTube.Extensions;

namespace Infrastructure.YouTube;

public class YouTubeVideoProvider : IVideoProvider
{
    public const string ProviderId = "YOUTUBE";
    public const int MaxYouTubeItemsPerPage = 50;

    public YouTubeVideoProvider(YouTubeService youTubeService)
    {        
        YouTubeService = youTubeService ?? throw new ArgumentNullException(nameof(youTubeService));
    }

    readonly YouTubeService YouTubeService;

    public async IAsyncEnumerable<GenericChannelDTO> GetChannelsAsync(IEnumerable<string> idsOrUrls, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var page in idsOrUrls.Page(MaxYouTubeItemsPerPage))
        {
            var request = YouTubeService.Channels.List("snippet,contentDetails,statistics,topicDetails,status,contentOwnerDetails");
            request.Id = string.Join(",", page.Select(id => FromStringOrQueryString(id, "@")));

            var response = await request.ExecuteAsync(cancellation);

            foreach (var channel in response.Items)
            {
                yield return channel.ToGenericChannel();
            };
        }
    }

    

    public async IAsyncEnumerable<GenericPlaylistDTO> GetPlaylistsAsync(IEnumerable<string> idsOrUrls, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var page in idsOrUrls.Page(MaxYouTubeItemsPerPage))
        {
            var request = YouTubeService.Playlists.List("snippet,contentDetails,status,player");
            request.Id = string.Join(",", page.Select(id => FromStringOrQueryString(id, "list")));

            var response = await request.ExecuteAsync(cancellation);

            foreach (var playlist in response.Items)
            {
                yield return playlist.ToGenericPlaylist();
            };
        }        
    }

    

    public async IAsyncEnumerable<GenericVideoDTO> GetVideosAsync(IEnumerable<string> idsOrUrls, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var page in idsOrUrls.Page(MaxYouTubeItemsPerPage))
        {
            var request = YouTubeService.Videos.List("snippet,contentDetails,status,player");
            request.Id = string.Join(",", page.Select(id => FromStringOrQueryString(id, "v")));
            
            var response = await request.ExecuteAsync(cancellation);

            foreach (var video in response.Items)
            {
                yield return video.ToGenericVideo();
            };
        }
    }

    public async IAsyncEnumerable<GenericVideoDTO> GetVideosOfPlaylistAsync(string playlistIdOrUrl, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        var request = YouTubeService.PlaylistItems.List("snippet");
        request.PlaylistId = FromStringOrQueryString(playlistIdOrUrl, "list");

        Google.Apis.YouTube.v3.Data.PlaylistItemListResponse response;
        do
        {
            response = await request.ExecuteAsync(cancellation);

            var videoIds = response.Items.Select(i => i.Snippet.ResourceId.VideoId);

            await foreach (var video in GetVideosAsync(videoIds, cancellation))
            {
                yield return video;
            }

            request.PageToken = response.NextPageToken;
        }
        while (request.PageToken != null);
    }
 
    static string FromStringOrQueryString(string text, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException(nameof(text));

        if (string.IsNullOrWhiteSpace(parameterName))
            throw new ArgumentException($"'{nameof(parameterName)}' cannot be null or whitespace.", nameof(parameterName));

        var idx = text.IndexOf("?");
        if (idx < 0)
            return text;

        // https://www.youtube.com/watch?v=V8WuljiJFBI&something=else&more=here
        text = text[idx..];

        var args = QueryHelpers.ParseQuery(text);
        if (!args.TryGetValue(parameterName, out var res))
            throw new Exception($"Parameter '{parameterName}' not found in '{text}'");

        return res!;
    }
}
