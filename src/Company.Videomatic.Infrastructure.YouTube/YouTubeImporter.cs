using Company.SharedKernel.Abstractions;
using Google.Apis.YouTube.v3;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubeImporter : IVideoImporter
{
    public const string ProviderId = "YOUTUBE";

    public YouTubeImporter(
        IVideoProvider provider,
        IRepository<Video> videoRepository,
        IRepository<Playlist> playlistRepository,
        ISender sender)
    {
        Provider = provider;
        VideoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        PlaylistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }
    
    readonly YouTubeService YouTubeService;
    readonly IVideoProvider Provider;
    readonly IRepository<Video> VideoRepository;
    readonly IRepository<Playlist> PlaylistRepository;
    readonly ISender Sender;

    public async Task ImportPlaylistsAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation)        
    {
        await foreach (var page in Provider.GetPlaylistsAsync(idsOrUrls, cancellation).PageAsync(50))
        {
            var playlists = page.Select(gpl => MapToPlaylist(gpl));
            IEnumerable<Playlist> res = await PlaylistRepository.AddRangeAsync(playlists, cancellation);

            foreach (var pl in res)
            { 
                await ImportVideosAsync(pl.Id, cancellation);
            }
        }
    }    

    public Task ImportVideosAsync(IEnumerable<string> idsOrUrls, PlaylistId? linkTo, CancellationToken cancellation)
      =>  InternalImportVideosAsync(idsOrUrls, linkTo, cancellation);  

    public async Task ImportVideosAsync(PlaylistId playlistId, CancellationToken cancellation)
    {
        var pl = await PlaylistRepository.GetByIdAsync(playlistId, cancellation);
        if (pl?.Origin?.Id == null)
            throw new ArgumentException($"Playlist '{playlistId}' does not exist have an origin or does not exist.", nameof(playlistId));

        await foreach (IEnumerable<GenericVideo> videoPage in Provider.GetVideosOfPlaylistAsync(pl.Origin.Id, cancellation).PageAsync(50))
        {
            var videoIds = videoPage.Select(v => v.Id);

            await InternalImportVideosAsync(videoIds, playlistId, cancellation);
        }        
    }


    public async IAsyncEnumerable<Transcript> ImportTranscriptionsAsync(
        IEnumerable<VideoId> videoIds, [EnumeratorCancellation] CancellationToken cancellation)
    {        
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

    private static Playlist MapToPlaylist(GenericPlaylist gpl)
    {
        return Playlist.Create(
                        new PlaylistOrigin(
                            Id: gpl.Id,
                            ETag: gpl.ETag,
                            ChannelId: gpl.ChannelId,
                            Name: gpl.Name,
                            Description: gpl.Description,
                            PublishedAt: gpl.PublishedAt,
                            ThumbnailUrl: gpl.ThumbnailUrl,
                            PictureUrl: gpl.PictureUrl,
                            EmbedHtml: gpl.EmbedHtml,
                            DefaultLanguage: gpl.DefaultLanguage));
    }

    private static Video MapToVideo(GenericVideo item)
    {
        var video = Video.Create(
                            location: $"https://www.youtube.com/watch?v={item.Id}",
                            name: item.Name,
                            pictureUrl: item.PictureUrl,
                            thumbnailUrl: item.ThumbnailUrl,
                            description: item.Description,                            
                            details: new VideoDetails(
                                Provider: ProviderId,
                                ProviderVideoId: item.Id,
                                VideoPublishedAt: item.PublishedAt ?? DateTime.UtcNow, // TODO: smell
                                VideoOwnerChannelTitle: "",//item.ChannelTitle, // TODO: finish
                                VideoOwnerChannelId: item.ChannelId));

        // Tags
        var tags = (item.Tags ?? Enumerable.Empty<string>()).ToArray();
        video.AddTags(tags);

        // Thumbnails
        //video.ThumbnailUrl = item.ThumbnailUrl;
        //var t = item.Thumbnails;
        //video.SetThumbnail(ThumbnailResolution.Default, t.Default__?.Url ?? "", Convert.ToInt32(t.Default__?.Height ?? -1), Convert.ToInt32(t.Default__?.Width ?? -1));
        //video.SetThumbnail(ThumbnailResolution.High, t.High?.Url ?? "", Convert.ToInt32(t.High?.Height ?? -1), Convert.ToInt32(t.High?.Width ?? -1));
        //video.SetThumbnail(ThumbnailResolution.Standard, t.Standard?.Url ?? "", Convert.ToInt32(t.Standard?.Height ?? -1), Convert.ToInt32(t.Standard?.Width ?? -1));
        //video.SetThumbnail(ThumbnailResolution.Medium, t.Medium?.Url ?? "", Convert.ToInt32(t.Medium?.Height ?? -1), Convert.ToInt32(t.Medium?.Width ?? -1));
        //video.SetThumbnail(ThumbnailResolution.MaxRes, t.Maxres?.Url ?? "", Convert.ToInt32(t.Maxres?.Height ?? -1), Convert.ToInt32(t.Maxres?.Width ?? -1));

        return video;
    }

    async Task InternalImportVideosAsync(IEnumerable<string> idsOrUrls, PlaylistId? playlistId, CancellationToken cancellation)
    {
        await foreach (var page in Provider.GetVideosAsync(idsOrUrls, cancellation).PageAsync(50))
        {
            var videos = page.Select(gv => MapToVideo(gv));

            IEnumerable<Video> res = await VideoRepository.AddRangeAsync(videos, cancellation);

            if (playlistId != null)
                await PlaylistRepository.LinkPlaylistToVideos(playlistId, res.Select(x => x.Id).ToList());
        }
    }
}