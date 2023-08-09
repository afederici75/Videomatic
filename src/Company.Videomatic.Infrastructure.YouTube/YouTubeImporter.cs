using Company.SharedKernel.Abstractions;
using Google.Apis.YouTube.v3;
using Hangfire;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubeImporter : IVideoImporter
{
    public const string ProviderId = "YOUTUBE";

    public YouTubeImporter(
        YouTubeService youTubeService,
        IBackgroundJobClient jobClient,
        IPlaylistService playlistService,
        IRepository<Video> videoRepository,
        IRepository<Playlist> playlistRepository)
    {
        YouTubeService = youTubeService ?? throw new ArgumentNullException(nameof(youTubeService));
        JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
        PlaylistService = playlistService;
        VideoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        PlaylistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));        
    }
    
    readonly YouTubeService YouTubeService;
    readonly IBackgroundJobClient JobClient;
    readonly IPlaylistService PlaylistService;
    readonly IRepository<Video> VideoRepository;
    readonly IRepository<Playlist> PlaylistRepository;

    public async IAsyncEnumerable<Playlist> ImportPlaylistsAsync(IEnumerable<string> idOrUrls, [EnumeratorCancellation] CancellationToken cancellation)        
    {
        var ids = idOrUrls.Select(x => FromStringOrQueryString(x, "list")).ToArray();
        
        var request = YouTubeService.Playlists.List("contentDetails,status,snippet,player");
        request.Id = string.Join(',', ids);
        request.MaxResults = 50;

        do
        {
            var response = await request.ExecuteAsync();

            foreach (var ytPlaylist in response.Items)
            { 
                var origin = new PlaylistOrigin(
                    Id: ytPlaylist.Id,
                    ETag: ytPlaylist.ETag,
                    ChannelId: ytPlaylist.Snippet.ChannelId,
                    Name: ytPlaylist.Snippet.Title,
                    Description: ytPlaylist.Snippet.Description,    
                    PublishedAt: ytPlaylist.Snippet.PublishedAtDateTimeOffset?.UtcDateTime,
                    ThumbnailUrl: ytPlaylist.Snippet.Thumbnails.Default__.Url,
                    PictureUrl: ytPlaylist.Snippet.Thumbnails.High.Url,
                    EmbedHtml: ytPlaylist.Player.EmbedHtml,
                    DefaultLanguage: ytPlaylist.Snippet.DefaultLanguage);

                var playlist = Playlist.Create(origin);
                yield return playlist;
            }
           
            // Pages
            request.PageToken = response.NextPageToken;
        }
        while (!string.IsNullOrEmpty(request.PageToken));       
    }
    
    public async IAsyncEnumerable<Video> ImportVideosAsync(IEnumerable<string> idOrUrls, [EnumeratorCancellation] CancellationToken cancellation)
    {
        var ids = idOrUrls.Select(x => FromStringOrQueryString(x, "v")).ToArray();        

        // TODO: should page, e.g. if we send 200 ids it should page in 50 items blocks
        var request = YouTubeService.Videos.List("snippet,contentDetails,id,status");
        request.Id = string.Join(',', ids);
        request.MaxResults = 50;

        do
        {
            var response = await request.ExecuteAsync(cancellation);
        
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
                        VideoPublishedAt: item.Snippet.PublishedAtDateTimeOffset?.UtcDateTime ?? DateTime.UtcNow,
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

                cancellation.ThrowIfCancellationRequested();
            }

            // Pages
            request.PageToken = response.NextPageToken;
        }
        while (!string.IsNullOrEmpty(request.PageToken));
    }

    public async IAsyncEnumerable<Transcript> ImportTranscriptionsAsync(IEnumerable<VideoId> videoIds, [EnumeratorCancellation] CancellationToken cancellation)
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
            cancellation.ThrowIfCancellationRequested();

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

                cancellation.ThrowIfCancellationRequested();
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

                cancellation.ThrowIfCancellationRequested();
            }
        }
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