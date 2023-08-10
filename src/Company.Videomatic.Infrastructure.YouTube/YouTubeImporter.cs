using Company.SharedKernel.Abstractions;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Google.Apis.YouTube.v3;
using Hangfire;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubeImporter : IVideoImporter
{
    public const string ProviderId = "YOUTUBE";

    public YouTubeImporter(
        IVideoProvider provider,
        IRepository<Video> videoRepository,
        IRepository<Playlist> playlistRepository,
        IRepository<Transcript> transcriptRepository,
        IBackgroundJobClient jobClient,
        ISender sender)
    {
        Provider = provider;
        VideoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        PlaylistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
        TranscriptRepository = transcriptRepository ?? throw new ArgumentNullException(nameof(transcriptRepository));
        JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
    }
    
    readonly IVideoProvider Provider;
    readonly IRepository<Video> VideoRepository;
    readonly IRepository<Playlist> PlaylistRepository;
    readonly IRepository<Transcript> TranscriptRepository;
    readonly IBackgroundJobClient JobClient;

    public async Task ImportPlaylistsAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation)        
    {
        await foreach (var page in Provider.GetPlaylistsAsync(idsOrUrls, cancellation).PageAsync(50))
        {
            var playlists = page.Select(gpl => MapToPlaylist(gpl));

            // TODO: figure out why AddRangeAsync does not work: it does not update the Id property!!!
            // IEnumerable<Playlist> res = await PlaylistRepository.AddRangeAsync(playlists, cancellation);

            foreach (var pl in playlists)
            {
                var res = await PlaylistRepository.AddAsync(pl, cancellation);

                var jobId = JobClient.Enqueue<IVideoImporter>(imp => imp.ImportVideosAsync(res.Id, cancellation));
            }                                    
        }
    }

    public async Task ImportVideosAsync(IEnumerable<string> idsOrUrls, PlaylistId? linkTo, CancellationToken cancellation)
    {
        await foreach (var page in Provider.GetVideosAsync(idsOrUrls, cancellation).PageAsync(50))
        {
            var videos = page.Select(gv => MapToVideo(gv));

            IEnumerable<Video> res = await VideoRepository.AddRangeAsync(videos, cancellation);

            if (linkTo != null)
                await PlaylistRepository.LinkPlaylistToVideos(linkTo, res.Select(x => x.Id).ToList());
        }
    }

    public async Task ImportVideosAsync(PlaylistId playlistId, CancellationToken cancellation)
    {
        var pl = await PlaylistRepository.GetByIdAsync(playlistId, cancellation);
        if (pl?.Origin?.Id == null)
            throw new ArgumentException($"Playlist '{playlistId}' does not exist have an origin or does not exist.", nameof(playlistId));

        await foreach (IEnumerable<GenericVideo> videoPage in Provider.GetVideosOfPlaylistAsync(pl.Origin.Id, cancellation).PageAsync(50))
        {
            var videoIds = videoPage.Select(v => v.Id);

            await ImportVideosAsync(videoIds, playlistId, cancellation);
        }        
    }


    public async Task ImportTranscriptionsAsync(
        IEnumerable<VideoId> videoIds, CancellationToken cancellation)
    {        
        var videoIdsByVideoId = await VideoRepository.GetProviderVideoIds(videoIds, cancellation);
                        
        using var api = new YoutubeTranscriptApi.YouTubeTranscriptApi();

        foreach (var responsePage in videoIdsByVideoId.Page(50))
        {
            cancellation.ThrowIfCancellationRequested();

            var currentSet = responsePage.ToHashSet();
            var badTranscripts = new List<Transcript>();

            var requestedIds = currentSet.Select(x => x.Value).ToList();

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

            await TranscriptRepository.AddRangeAsync(badOnes);

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

}