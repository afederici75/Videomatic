using Google.Apis.YouTube.v3;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubeVideoProvider : IVideoProvider
{
    public const string ProviderId = "YOUTUBE";

    public YouTubeVideoProvider(YouTubeService youTubeService, ISender sender)
    {        
        YouTubeService = youTubeService ?? throw new ArgumentNullException(nameof(youTubeService));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));     
    }

    readonly ISender Sender;
    readonly YouTubeService YouTubeService;

    public async IAsyncEnumerable<GenericChannel> GetChannelsAsync(IEnumerable<string> idsOrUrls, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var page in idsOrUrls.Page(MaxYouTubeItemsPerPage))
        {
            var request = YouTubeService.Channels.List("snippet,contentDetails,statistics,topicDetails,status,contentOwnerDetails");
            request.Id = string.Join(",", page.Select(id => FromStringOrQueryString(id, "@")));

            var response = await request.ExecuteAsync(cancellation);

            foreach (var channel in response.Items)
            {
                var thumbs = channel.Snippet!.Thumbnails!;

                string? thubUrl = thumbs.Default__?.Url ?? thumbs.Medium?.Url ?? thumbs.High?.Url ?? thumbs.Standard?.Url ?? thumbs.Maxres?.Url;
                string? pictUrl = thumbs.Maxres?.Url ?? thumbs.Standard?.Url ?? thumbs.High?.Url ?? thumbs.Medium?.Url ?? thumbs.Default__?.Url;

                NameAndDescription? locInfo = null;
                if (channel.Snippet.Localized != null)
                    locInfo = new(channel.Snippet.Localized.Title, channel.Snippet.Localized.Description);
                
                var pl = new GenericChannel(
                    Id: channel.Id,
                    ETag: channel.ETag,
                    Name: channel.Snippet.Title,
                    Description: channel.Snippet.Description,
                    PublishedAt: channel.Snippet.PublishedAtDateTimeOffset?.UtcDateTime,
                    ThumbnailUrl: thubUrl,
                    PictureUrl: pictUrl,
                    DefaultLanguage: channel.Snippet.DefaultLanguage,
                    LocalizationInfo: locInfo,
                    Owner: channel.ContentOwnerDetails?.ContentOwner,
                    TimeCreated: channel.ContentOwnerDetails?.TimeLinkedDateTimeOffset?.UtcDateTime,
                    TopicCategories: channel.TopicDetails?.TopicCategories ?? Enumerable.Empty<string>(),
                    TopicIds: channel.TopicDetails?.TopicIds ?? Enumerable.Empty<string>(),
                    VideoCount: channel.Statistics?.VideoCount,
                    SuscriberCount: channel.Statistics?.SubscriberCount,
                    ViewCount: channel.Statistics?.ViewCount);

                yield return pl;
            };
        }
    }

    const int MaxYouTubeItemsPerPage = 50;

    public async IAsyncEnumerable<GenericPlaylist> GetPlaylistsAsync(IEnumerable<string> idsOrUrls, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var page in idsOrUrls.Page(MaxYouTubeItemsPerPage))
        {
            var request = YouTubeService.Playlists.List("snippet,contentDetails,status,player");
            request.Id = string.Join(",", page.Select(id => FromStringOrQueryString(id, "list")));

            var response = await request.ExecuteAsync(cancellation);

            foreach (var playlist in response.Items)
            {
                var pl = new GenericPlaylist(
                    Id: playlist.Id,
                    ETag: playlist.ETag,
                    ChannelId: playlist.Snippet.ChannelId,
                    Name: playlist.Snippet.Title,
                    Description: playlist.Snippet.Description,
                    PublishedAt: playlist.Snippet.PublishedAtDateTimeOffset?.UtcDateTime ?? DateTime.UtcNow,
                    ThumbnailUrl: playlist.Snippet.Thumbnails.Default__.Url,
                    PictureUrl: playlist.Snippet.Thumbnails.Maxres.Url,
                    EmbedHtml: playlist.Player.EmbedHtml,
                    DefaultLanguage: playlist.Snippet.DefaultLanguage,
                    LocalizationInfo: new NameAndDescription(playlist.Snippet.Localized?.Title ?? "??", playlist.Snippet.Localized?.Description), 
                    PrivacyStatus: playlist.Status.PrivacyStatus,
                    VideoCount: Convert.ToInt32(playlist.ContentDetails.ItemCount));

                yield return pl;
            };
        }        
    }

    public async IAsyncEnumerable<GenericVideo> GetVideosAsync(IEnumerable<string> idsOrUrls, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var page in idsOrUrls.Page(MaxYouTubeItemsPerPage))
        {
            var request = YouTubeService.Videos.List("snippet,contentDetails,status,player");
            request.Id = string.Join(",", page.Select(id => FromStringOrQueryString(id, "v")));
            
            var response = await request.ExecuteAsync(cancellation);

            foreach (var video in response.Items)
            {               
                var picUrl = video.Snippet.Thumbnails.Maxres?.Url ?? video.Snippet.Thumbnails.Standard?.Url ?? video.Snippet.Thumbnails.High?.Url ?? video.Snippet.Thumbnails.Medium?.Url ?? video.Snippet.Thumbnails.Default__?.Url
                              ?? "http://nodata";
                var thumbUrl  = video.Snippet.Thumbnails.Default__?.Url ?? video.Snippet.Thumbnails.Medium?.Url ?? video.Snippet.Thumbnails.High?.Url ?? video.Snippet.Thumbnails.Standard?.Url ?? video.Snippet.Thumbnails.Maxres?.Url
                              ?? "http://nodata";

                var pl = new GenericVideo(
                    Id: video.Id,
                    ETag: video.ETag,
                    ChannelId: video.Snippet.ChannelId,
                    Name: video.Snippet.Title,
                    Description: video.Snippet.Description,
                    PublishedAt: video.Snippet.PublishedAtDateTimeOffset?.UtcDateTime ?? DateTime.UtcNow,
                    ThumbnailUrl: thumbUrl,
                    PictureUrl: picUrl,
                    EmbedHtml: video.Player.EmbedHtml,
                    DefaultLanguage: video.Snippet.DefaultLanguage,
                    LocalizationInfo: new NameAndDescription(video.Snippet.Localized.Title, video.Snippet.Localized.Description),
                    PrivacyStatus: video.Status.PrivacyStatus,
                    Tags: video.Snippet.Tags ?? Array.Empty<string>());

                yield return pl;
            };
        }
    }

    public async IAsyncEnumerable<GenericVideo> GetVideosOfPlaylistAsync(string playlistIdOrUrl, [EnumeratorCancellation] CancellationToken cancellation = default)
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