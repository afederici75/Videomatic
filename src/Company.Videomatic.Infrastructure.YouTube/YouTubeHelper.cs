using Company.Videomatic.Application.Features.Videos.Queries;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Aggregates.Video;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubeHelper : IYouTubeHelper
{
    public const string ProviderId = "YOUTUBE";

    public YouTubeHelper(IOptions<YouTubeOptions> options, HttpClient client, ISender sender)
    {
        Options = options.Value;
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));        
    }

    readonly YouTubeOptions Options;
    readonly ISender Sender;

    record AuthResponse();


    public async Task<PlaylistInfo> GetPlaylistInformation(string playlistId)
    {
        playlistId = FromStringOrQueryString(playlistId, "list");

        using YouTubeService service = CreateYouTubeService();

        var request = service.Playlists.List("snippet,contentDetails,status");
        request.Id = playlistId;

        var response = await request.ExecuteAsync();
        var pl = response.Items.Single();

        return new PlaylistInfo(pl.Id, pl.Snippet.Title, pl.Snippet.Description);
    }

    public async IAsyncEnumerable<PlaylistDTO> GetPlaylistsOfChannel(string channelId)
    {
        using YouTubeService service = CreateYouTubeService();
        
        var request = service.Playlists.List("snippet,contentDetails,status");
        request.ChannelId = channelId;
        request.MaxResults = 50;
        
        do
        {
            var response = await request.ExecuteAsync();

            foreach (var playlist in response.Items)
            {
                var pl = new PlaylistDTO(-1, playlist.Snippet.Title, playlist.Snippet.Description, Convert.ToInt32(playlist.ContentDetails.ItemCount));
                yield return pl;
            };

            // Pages
            request.PageToken = response.NextPageToken;
        }
        while (!string.IsNullOrEmpty(request.PageToken));
    }

    string FromStringOrQueryString(string text, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException(nameof(text));

        if (string.IsNullOrWhiteSpace(parameterName))
            throw new ArgumentException($"'{nameof(parameterName)}' cannot be null or whitespace.", nameof(parameterName));
        
        var idx = text.IndexOf("?");
        if (idx <0)
            return text;

        // https://www.youtube.com/watch?v=V8WuljiJFBI&something=else&more=here
        text = text.Substring(idx);

        var args = QueryHelpers.ParseQuery(text);
        if (!args.TryGetValue(parameterName, out var res))
            throw new Exception($"Parameter '{parameterName}' not found in '{text}'");

        return res!;        
    }

    public async Task<IEnumerable<string>> GetPlaylistVideoIds(string playlistId)
    {
        playlistId = FromStringOrQueryString(playlistId, "list");

        using YouTubeService service = CreateYouTubeService();
        var request = service.PlaylistItems.List("contentDetails,status");
        request.PlaylistId = playlistId;
        request.MaxResults = 50;

        var videoIds = new List<string>();
        do
        {
            var response = await request.ExecuteAsync();

            videoIds.AddRange(response.Items.Select(i => i.ContentDetails.VideoId));
            
            // Pages
            request.PageToken = response.NextPageToken;
        }
        while (!string.IsNullOrEmpty(request.PageToken));

        return videoIds;
    }

    public async IAsyncEnumerable<Video> ImportVideosOfPlaylist(string playlistId)
    {
        playlistId = FromStringOrQueryString(playlistId, "list");

        using YouTubeService service = CreateYouTubeService();
        var request = service.PlaylistItems.List("contentDetails,status");
        request.PlaylistId = playlistId;
        request.MaxResults = 50;

        do
        {
            var response = await request.ExecuteAsync();

            var videoIds = response.Items.Select(i => i.ContentDetails.VideoId).ToArray();

            await foreach (var video in ImportVideosById(videoIds))
            {
                yield return video;
            };

            // Pages
            request.PageToken = response.NextPageToken;
        }
        while (!string.IsNullOrEmpty(request.PageToken));       
    }

    public IAsyncEnumerable<Video> ImportVideosByUrl(IEnumerable<string> youtubeVideoUrls)
    {
        // TODO: this is ghetto code
        var idsOnly = youtubeVideoUrls
            .Select(url => url.Replace("https://www.youtube.com/watch?v=", string.Empty))
            .ToArray();

        return ImportVideosById(idsOnly);
    }

    public async IAsyncEnumerable<Video> ImportVideosById(IEnumerable<string> youtubeVideoIds)
    {        
        // TODO: should page, e.g. if we send 200 ids it should page in 50 items blocks
        using YouTubeService service = CreateYouTubeService();
        var request = service.Videos.List("snippet,contentDetails,id,status");
        request.Id = string.Join(',', youtubeVideoIds);
        request.MaxResults = 50;

        do
        {
            var response = await request.ExecuteAsync();
        
            var videoIds = response.Items.Select(v => v.Id).ToArray();

            foreach (var item in response.Items)
            {
                var video = Video.Create(
                    location: $"https://www.youtube.com/watch?v={item.Id}",
                    name: item.Snippet.Title,
                    description: item.Snippet.Description,
                    details: new VideoDetails(
                        Provider: ProviderId,
                        ProviderVideoId: item.Id,
                        VideoPublishedAt: item.Snippet.PublishedAt ?? new DateTime(),
                        VideoOwnerChannelTitle: item.Snippet.ChannelTitle,
                        VideoOwnerChannelId: item.Snippet.ChannelId));

                // Tags
                var tags = (item. Snippet.Tags ?? Enumerable.Empty<string>()).ToArray();
                video.AddTags(tags);

                // Thumbnails
                var t = item.Snippet.Thumbnails;
                video.SetThumbnail(ThumbnailResolution.Default, t.Default__?.Url ?? "", Convert.ToInt32(t.Default__?.Height ?? -1), Convert.ToInt32(t.Default__?.Width ?? -1));
                video.SetThumbnail(ThumbnailResolution.High, t.High?.Url ?? "", Convert.ToInt32(t.High?.Height ?? -1), Convert.ToInt32(t.High?.Width ?? -1));
                video.SetThumbnail(ThumbnailResolution.Standard, t.Standard?.Url ?? "", Convert.ToInt32(t.Standard?.Height ?? -1), Convert.ToInt32(t.Standard?.Width ?? -1));
                video.SetThumbnail(ThumbnailResolution.Medium, t.Medium?.Url ?? "", Convert.ToInt32(t.Medium?.Height ?? -1), Convert.ToInt32(t.Medium?.Width ?? -1));
                video.SetThumbnail(ThumbnailResolution.MaxRes, t.Maxres?.Url ?? "", Convert.ToInt32(t.Maxres?.Height ?? -1), Convert.ToInt32(t.Maxres?.Width ?? -1));

                yield return video;
            }            // Pages
            request.PageToken = response.NextPageToken;
        }
        while (!string.IsNullOrEmpty(request.PageToken));
    }

    public async IAsyncEnumerable<Transcript> ImportTranscriptions(IEnumerable<VideoId> videoIds)
    {
        /*
         using YouTubeService service = CreateYouTubeService();

    CaptionsResource.DownloadRequest response = service.Captions.Download("BBd3aHnVnuE");

    service.

    // TODO: should page, e.g. if we send 200 ids it should page in 50 items blocks
    var stream = new MemoryStream();
    var resp = await response.DownloadAsync(stream);

    var s = Encoding.UTF8.GetString(stream.ToArray());

    var t = Transcript.Create(1, "xx");
    yield return t;
        */
        // TODO: should page, e.g. if we send 200 ids it should page in 50 items blocks

        var response = await Sender.Send(new GetVideoIdsOfProviderQuery(videoIds));
        Dictionary<int, string> videoIdsByVideoId = response.Value.ToDictionary(x => x.VideoId, x => x.ProviderVideoId);

        using var api = new YoutubeTranscriptApi.YouTubeTranscriptApi();

        foreach (var responsePage in response.Value.Page(50))
        {
            var currentSet = responsePage.ToHashSet();
            var badTranscripts = new List<Transcript>();
            var requestedIds = currentSet.Select(x => x.ProviderVideoId).ToList();
            var languages = new List<string>() { "en", "es", "en-GB", "ko", "it" };
            var allTranscripts = api.GetTranscripts(
                requestedIds, 
                languages: languages,
                continue_after_error: true); // Returns an ugly (Dictionary<string, IEnumerable<YoutubeTranscriptApi.TranscriptItem>>, IReadOnlyList<string>) 

            var badOnes = allTranscripts.Item2!.Select(badId =>
            {
                var videoId = videoIdsByVideoId.First(x => x.Value == badId).Key;
                return Transcript.Create(videoId, "??", new[] { $"Transcripts disabled or no transcript found for video '{badId}' [{videoId}]." });
            });

            foreach (var badOne in badOnes)
            {
                yield return badOne;
            }

            // ------------------------------
            Dictionary<string, IEnumerable<YoutubeTranscriptApi.TranscriptItem>>? fetchedTranscripts = allTranscripts.Item1;
            IReadOnlyList<string>? failedToDownload = allTranscripts!.Item2;

            foreach (var v in videoIdsByVideoId)
            {
                if (!fetchedTranscripts!.TryGetValue(v.Value, out var ytTranscriptItems))
                {
                    // No transcript?
                    continue;
                }

                // ------------------------------
                var newTranscr = Transcript.Create(v.Key, "US");
                foreach (var item in ytTranscriptItems)
                {
                    newTranscr.AddLine(item.Text, TimeSpan.FromSeconds(item.Duration), TimeSpan.FromSeconds(item.Start));
                }

                yield return newTranscr;
            }
        }
    }

    YouTubeService CreateYouTubeService()
    {
        var certificate = new X509Certificate2(@"VideomaticService.p12", Options.CertificatePassword, X509KeyStorageFlags.Exportable);

        ServiceAccountCredential credential = new ServiceAccountCredential(
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