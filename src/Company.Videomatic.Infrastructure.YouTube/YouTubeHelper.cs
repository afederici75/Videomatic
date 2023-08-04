using Company.Videomatic.Application.Features.Videos.Queries;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Aggregates.Video;
using Company.Videomatic.Infrastructure.YouTube.API.JsonPasteSpecial;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MediatR;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubeHelper : IYouTubeHelper
{
    public const string ProviderId = "YOUTUBE";

    public YouTubeHelper(IOptions<YouTubeOptions> options, HttpClient client, ISender sender)
    {
        // TODO: messy / unfinished
        _options = options.Value;
        _client = client;
        _client.BaseAddress = new Uri("https://www.googleapis.com/youtube/v3/");
        _sender = sender;        
    }

    readonly YouTubeOptions _options;
    readonly HttpClient _client;
    ISender _sender;

    record AuthResponse();

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

    
    public async IAsyncEnumerable<Video> ImportVideosOfPlaylist(string playlistId)
    {
        using YouTubeService service = CreateYouTubeService();
        var request = service.PlaylistItems.List("contentDetails,status");
        request.PlaylistId = playlistId;
        request.MaxResults = 50;

        do
        {
            var response = await request.ExecuteAsync();

            var videoIds = response.Items.Select(i => i.ContentDetails.VideoId).ToArray();

            await foreach (var video in ImportVideos(videoIds))
            {
                yield return video;
            };

            // Pages
            request.PageToken = response.NextPageToken;
        }
        while (!string.IsNullOrEmpty(request.PageToken));


        //API.JsonPasteSpecial.PlaylistItemListResponse response;
        //do
        //{
        //    // API call
        //    var fullUrl = MakeUrlWithQuery("playlistItems", parameters);
        //    string json = await _client.GetStringAsync(fullUrl);
        //    response = JsonConvert.DeserializeObject<API.JsonPasteSpecial.PlaylistItemListResponse>(json)!;
        //
        //    // Gets all video Ids
        //    var publicVideos = response.items.Where(i => i.status.privacyStatus == "public")
        //                                     .Select(i => i.contentDetails.videoId);
        //
        //    var videoIds = new HashSet<string>(publicVideos);
        //    
        //    await foreach (var video in ImportVideos(videoIds.ToArray()))
        //    { 
        //        yield return video;
        //    };
        //
        //    // Pages
        //    parameters["pageToken"] = response.nextPageToken;
        //}
        //while (!string.IsNullOrEmpty(response.nextPageToken));
    }    

    public async IAsyncEnumerable<Video> ImportVideos(IEnumerable<string> youtubeVideoIds)
    {
        // TODO: should page, e.g. if we send 200 ids it should page in 50 items blocks

        var parameters = new Dictionary<string, string>
        {
            //["key"] = _options.ApiKey,
            ["id"] = string.Join(',', youtubeVideoIds),
            ["part"] = "snippet,contentDetails,id,status",
            //["maxResults"] = "50"
        };

        API.JsonPasteSpecial.VideosListResponse response;
        var fullUrl = MakeUrlWithQuery("videos", parameters);

        string json = await _client.GetStringAsync(fullUrl);
        response = JsonConvert.DeserializeObject<VideosListResponse>(json)!;

        var videoIds = response.items.Select(v => v.id).ToArray();

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

            // Tags
            var tags = (item.snippet.tags ?? Enumerable.Empty<string>()).ToArray();
            video.AddTags(tags);

            // Thumbnails
            var t = item.snippet.thumbnails;
            video.SetThumbnail(ThumbnailResolution.Default, t.@default?.url ?? "", t.@default?.height ?? -1, t.@default?.width ?? -1);
            video.SetThumbnail(ThumbnailResolution.High, t.high?.url ?? "", t.high?.height ?? -1, t.high?.width ?? -1);
            video.SetThumbnail(ThumbnailResolution.Standard, t.standard?.url ?? "", t.standard?.height ?? -1, t.standard?.width ?? -1);
            video.SetThumbnail(ThumbnailResolution.Medium, t.medium?.url ?? "", t.medium?.height ?? -1, t.medium?.width ?? -1);
            video.SetThumbnail(ThumbnailResolution.MaxRes, t.maxres?.url ?? "", t.maxres?.height ?? -1, t.maxres?.width ?? -1);

            yield return video;
        }                
    }

    public async IAsyncEnumerable<Transcript> ImportTranscriptions(IEnumerable<VideoId> videoIds)    
    {
        // TODO: should page, e.g. if we send 200 ids it should page in 50 items blocks

        var response = await _sender.Send(new GetVideoIdsOfProviderQuery(videoIds));
        var videoIdsByVideoId = response.Value;

        using var api = new YoutubeTranscriptApi.YouTubeTranscriptApi();

        foreach (IEnumerable<string> responsePage in response.Value.Page(50))
        {
            var currentSet = responsePage.ToHashSet();
            var badTranscripts = new List<Transcript>();
            var allTranscripts = api.GetTranscripts(currentSet.ToList(), continue_after_error: true); // Returns an ugly (Dictionary<string, IEnumerable<YoutubeTranscriptApi.TranscriptItem>>, IReadOnlyList<string>) 
            
            var badOnes = allTranscripts.Item2!.Select(badId =>
            {
                var videoId = videoIdsByVideoId.First(x => x.ProviderVideoId == badId).VideoId;
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
                if (!fetchedTranscripts!.TryGetValue(v.ProviderVideoId, out var ytTranscriptItems))
                {
                    // No transcript?
                    continue;
                }

                // ------------------------------
                var newTranscr = Transcript.Create(v.VideoId, "US");
                foreach (var item in ytTranscriptItems)
                {
                    newTranscr.AddLine(item.Text, TimeSpan.FromSeconds(item.Duration), TimeSpan.FromSeconds(item.Start));
                }

                yield return newTranscr;
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

    YouTubeService CreateYouTubeService()
    {
        var certificate = new X509Certificate2(@"VideomaticService.p12", _options.CertificatePassword, X509KeyStorageFlags.Exportable);

        ServiceAccountCredential credential = new ServiceAccountCredential(
           new ServiceAccountCredential.Initializer(_options.ServiceAccountEmail)
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