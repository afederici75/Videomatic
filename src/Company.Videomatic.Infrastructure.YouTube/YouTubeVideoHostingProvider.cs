using Ardalis.Result;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
//using Google.Apis.YouTube.v3.Data;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubeVideoHostingProvider : IVideoProvider
{
    public const string ProviderId = "YOUTUBE";

    public YouTubeVideoHostingProvider(IOptions<YouTubeOptions> options, ISender sender)
    {
        Options = options.Value;
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));

        YouTube  = CreateYouTubeService();
    }

    readonly YouTubeOptions Options;
    readonly ISender Sender;
    readonly YouTubeService YouTube;

    public async IAsyncEnumerable<GenericChannel> GetChannelInformationAsync(IEnumerable<string> idsOrUrls, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var page in idsOrUrls.Page(MaxYouTubeItemsPerPage))
        {
            var request = YouTube.Channels.List("snippet,contentDetails,statistics,topicDetails,status,contentOwnerDetails");
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

                //ContentOwnerDetail? contDetails = null;
                //if (channel.ContentOwnerDetails != null)
                //    contDetails = new ContentOwnerDetail(channel.ContentOwnerDetails.ContentOwner, channel.ContentOwnerDetails.TimeLinked);

                //GenericChannelTopics? genericChannelTopics = null;
                //if (channel.TopicDetails != null)
                //    genericChannelTopics = new GenericChannelTopics(channel.TopicDetails.TopicIds, channel.TopicDetails.TopicCategories);

                //GenericChannelStatistcs? genericChannelStatistcs = null; 
                //if (channel.Statistics != null)
                //    genericChannelStatistcs = new GenericChannelStatistcs(
                //        VideoCount: channel.Statistics.VideoCount,
                //        SuscriberCount: channel.Statistics.SubscriberCount,
                //        ViewCount: channel.Statistics.ViewCount);

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

    public async IAsyncEnumerable<GenericPlaylist> GetPlaylistInformationAsync(IEnumerable<string> idsOrUrls, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var page in idsOrUrls.Page(MaxYouTubeItemsPerPage))
        {
            var request = YouTube.Playlists.List("snippet,contentDetails,status,player");
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
                    PublishedAt: playlist.Snippet.PublishedAt ?? DateTime.UtcNow,
                    ThumbnailUrl: playlist.Snippet.Thumbnails.Default__.Url,
                    PictureUrl: playlist.Snippet.Thumbnails.Maxres.Url,
                    EmbedHtml: playlist.Player.EmbedHtml,
                    DefaultLanguage: playlist.Snippet.DefaultLanguage,
                    LocalizationInfo: new NameAndDescription(playlist.Snippet.Localized?.Title, playlist.Snippet.Localized?.Description), 
                    PrivacyStatus: playlist.Status.PrivacyStatus,
                    VideoCount: Convert.ToInt32(playlist.ContentDetails.ItemCount));

                yield return pl;
            };
        }        
    }

    public async IAsyncEnumerable<GenericVideo> GetVideoInformationAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default)
    {
        foreach (var page in idsOrUrls.Page(MaxYouTubeItemsPerPage))
        {
            var request = YouTube.Videos.List("snippet,contentDetails,status,player");
            request.Id = string.Join(",", page.Select(id => FromStringOrQueryString(id, "v")));
            
            var response = await request.ExecuteAsync(cancellation);

            foreach (var video in response.Items)
            {
                var pl = new GenericVideo(
                    Id: video.Id,
                    ETag: video.ETag,
                    ChannelId: video.Snippet.ChannelId,
                    Name: video.Snippet.Title,
                    Description: video.Snippet.Description,
                    PublishedAt: video.Snippet.PublishedAt ?? DateTime.UtcNow,
                    ThumbnailUrl: video.Snippet.Thumbnails.Default__.Url,
                    PictureUrl: video.Snippet.Thumbnails.Maxres.Url,
                    EmbedHtml: video.Player.EmbedHtml,
                    DefaultLanguage: video.Snippet.DefaultLanguage,
                    LocalizationInfo: new NameAndDescription(video.Snippet.Localized.Title, video.Snippet.Localized.Description),
                    PrivacyStatus: video.Status.PrivacyStatus);

                yield return pl;
            };
        }

    }

    // ------------------ OLD ------------------

    public IAsyncEnumerable<GenericPlaylist> GetPlaylistsOfChannel(string channelId, CancellationToken cancellation)

    {

        //public async IAsyncEnumerable<PlaylistDTO> GetPlaylistsOfChannel(string channelId, CancellationToken cancellation)
        //{
        //    using YouTubeService service = CreateYouTubeService();

        //    var request = service.Playlists.List("snippet,contentDetails,status");
        //    request.ChannelId = channelId;
        //    request.MaxResults = 50;

        //    do
        //    {
        //        var response = await request.ExecuteAsync(cancellation);

        //        foreach (var playlist in response.Items)
        //        {
        //            var pl = new PlaylistDTO(-1, playlist.Snippet.Title, playlist.Snippet.Description, Convert.ToInt32(playlist.ContentDetails.ItemCount));
        //            yield return pl;
        //        };

        //        // Pages
        //        request.PageToken = response.NextPageToken;
        //    }
        //    while (!string.IsNullOrEmpty(request.PageToken));
        //}
        throw new NotImplementedException();
    }


    public async Task<GenericPlaylist> GetPlaylistInformation(string playlistId, CancellationToken cancellation)
    {
        throw new Exception("OLD CODE");
        //playlistId = FromStringOrQueryString(playlistId, "list");

        //using YouTubeService service = CreateYouTubeService();

        //var request = service.Playlists.List("snippet,contentDetails,status");
        //request.Id = playlistId;

        //var response = await request.ExecuteAsync(cancellation);
        //var pl = response.Items.Single();

        //return new GenericPlaylist(pl.Id, pl.Snippet.Title, pl.Snippet.Description);
    }


   
    //public async Task<IEnumerable<GenericVideo>> GetPlaylistVideos(string playlistId, CancellationToken cancellation)
    //{
    //    playlistId = FromStringOrQueryString(playlistId, "list");

    //    using YouTubeService service = CreateYouTubeService();
    //    var request = service.PlaylistItems.List("contentDetails,status,snippet");
    //    request.PlaylistId = playlistId;
    //    request.MaxResults = 50;

    //    var videoIds = new List<GenericVideo>();
    //    do
    //    {
    //        var response = await request.ExecuteAsync(cancellation);

    //        videoIds.AddRange(response.Items.Select(i => new GenericVideo(
    //            Id: i.Id,
    //            Name: i.Snippet.Title,
    //            Description: i.Snippet.Description)));
            
            
    //        // Pages
    //        request.PageToken = response.NextPageToken;
    //    }
    //    while (!string.IsNullOrEmpty(request.PageToken));

    //    return videoIds;
    //}

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


    YouTubeService CreateYouTubeService()
    {
        var certificate = new X509Certificate2(@"VideomaticService.p12", Options.CertificatePassword, X509KeyStorageFlags.Exportable);

        ServiceAccountCredential credential = new (
           new ServiceAccountCredential.Initializer(Options.ServiceAccountEmail)
           {
               Scopes = new[] { YouTubeService.Scope.Youtube }
           }.FromCertificate(certificate));

        // Create the service.
        var service = new YouTubeService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = AppDomain.CurrentDomain.FriendlyName
        });

        return service;
    }
}