using Ardalis.Result;
using Company.SharedKernel.Abstractions;
using Hangfire;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubeImporter : IVideoImporter
{
    public const string ProviderId = "YOUTUBE";

    public YouTubeImporter(
        IVideoProvider provider,
        ILogger<YouTubeImporter> logger,
        IRepository<Video> videoRepository,
        IRepository<Playlist> playlistRepository,
        IRepository<Transcript> transcriptRepository,
        IBackgroundJobClient jobClient)
    {
        Provider = provider;
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
    readonly ILogger<YouTubeImporter> Logger;

    public async Task ImportPlaylistsAsync(IEnumerable<string> idsOrUrls, ImportOptions? options = default, CancellationToken cancellation = default)        
    {
        options ??= new ImportOptions();

        Logger.LogDebug("Importing {PlaylistCount} playlist(s) from YouTube. {@IdsOrUrls}", idsOrUrls.Count(), idsOrUrls);

        var doneCount = 0;
        await foreach (var page in Provider.GetPlaylistsAsync(idsOrUrls, cancellation).PageAsync(YouTubeVideoProvider.MaxYouTubeItemsPerPage))
        {
            var playlists = page.Select(gpl => gpl.ToPlaylist()).ToArray();

            IEnumerable<Playlist> storedPlaylists = await PlaylistRepository.AddRangeAsync(playlists, cancellation); // Saves to database
            doneCount += storedPlaylists.Count();
            
            Logger.LogDebug("Imported {DoneCount}/{PlaylistCount} playlist(s) from YouTube.", doneCount, idsOrUrls.Count());

#if DEBUG
            if (storedPlaylists.Any(x => x.Id == null))
                throw new Exception("Video Id is null");
#endif

            foreach (var pl in storedPlaylists)
            {
                //var res = await PlaylistRepository.AddAsync(pl, cancellation);

                switch (options.ExecuteWithoutJobQueue)
                {
                    case true:
                        await this.ImportVideosAsync(pl.Id, options, cancellation);
                        break;
                    case false:
                        var jobId = JobClient.Enqueue<IVideoImporter>(imp => imp.ImportVideosAsync(pl.Id, options, cancellation));
                        break;
                }                
            }                                    
        }
    }

    public async Task ImportVideosAsync(IEnumerable<string> idsOrUrls, PlaylistId? linkTo, ImportOptions? options = default, CancellationToken cancellation = default)
    {
        options ??= new ImportOptions();

        Logger.LogDebug("Importing {VideoCount} video(s) from YouTube. {@IdsOrUrls}", idsOrUrls.Count(), idsOrUrls);

        var doneCount = 0;
        await foreach (var page in Provider.GetVideosAsync(idsOrUrls, cancellation).PageAsync(YouTubeVideoProvider.MaxYouTubeItemsPerPage))
        {
            var videos = page.Select(gv => gv.ToVideo()).ToArray();             
            IEnumerable<Video> storedVideos = await VideoRepository.AddRangeAsync(videos, cancellation); // Saves to database
            doneCount += storedVideos.Count();

            Logger.LogDebug("Imported {DoneCount}/{VideoCount} playlist(s) from YouTube.", doneCount, idsOrUrls.Count());
#if DEBUG
            if (storedVideos.Any(x => x.Id == null))
                throw new Exception("Video Id is null");            
#endif

            var videoIds = storedVideos.Select(x => x.Id).ToArray();
            if (linkTo != null)
            {                
                await PlaylistRepository.LinkPlaylistToVideos(linkTo, videoIds, cancellation);
            }


            switch (options.ExecuteWithoutJobQueue)
            {
                case true:
                    await this.ImportTranscriptionsAsync(videoIds, cancellation);
                    break;
                case false:
                    var jobId = JobClient.Enqueue<IVideoImporter>(imp => imp.ImportTranscriptionsAsync(videoIds, cancellation));
                    break;
            }      
        }
    }    

    public async Task ImportVideosAsync(PlaylistId playlistId, ImportOptions? options = default, CancellationToken cancellation = default)
    {
        options ??= new ImportOptions();

        var pl = await PlaylistRepository.GetByIdAsync(playlistId, cancellation);
        if (pl?.Origin?.ProviderItemId == null)
            throw new ArgumentException($"Playlist '{playlistId}' does not exist have an origin or does not exist.", nameof(playlistId));

        await foreach (IEnumerable<GenericVideo> videoPage in Provider.GetVideosOfPlaylistAsync(pl.Origin.ProviderItemId, cancellation).PageAsync(YouTubeVideoProvider.MaxYouTubeItemsPerPage))
        {
            var videoIds = videoPage.Select(v => v.ProviderItemId);

            await ImportVideosAsync(videoIds, playlistId, options, cancellation);
        }        
    }

    public async Task ImportTranscriptionsAsync(
        IEnumerable<VideoId> videoIds, CancellationToken cancellation = default)
    {
        var sw = Stopwatch.StartNew();

        var videoIdsByProviderId = await VideoRepository.GetVideoProviderIds(videoIds, cancellation);        
        Logger.LogDebug("GetVideoProviderIds [{elapsed}ms]", sw.Elapsed);

        using var client = new HttpClient();

        
        var tasks = videoIdsByProviderId.Select(v => GetTimedTextInformation(client, v.Value, $"https://www.youtube.com/watch?v={v.Key}"));
        var results = await Task.WhenAll(tasks);
        Logger.LogDebug("GetTimedTextInformation [{elapsed}ms]", sw.Elapsed);

        var goodResults = results.Where(r => r.IsSuccess && r.Value.TimedTextInformation.PlayerCaptionsTracklistRenderer?.CaptionTracks != null)
                                 .Select(r => r.Value)
                                 .ToArray();
        
        var tasks2 = goodResults.Select(async result =>
        {
            var allTracks = result.TimedTextInformation.PlayerCaptionsTracklistRenderer.CaptionTracks;

            var tasks2 = allTracks.Select(track => ConvertTranscript(client, result.VideoId, track));
            var newTranscripts = await Task.WhenAll(tasks2);

            return newTranscripts;
        });

        Transcript[][] results2 = await Task.WhenAll(tasks2);
        Logger.LogDebug("Converted transcripts [{elapsed}ms]", sw.Elapsed);

        var newTranscripts = results2.SelectMany(x => x).ToArray();

        await TranscriptRepository.AddRangeAsync(newTranscripts, cancellation); // Saves to database
        Logger.LogDebug("Stored transcripts [{elapsed}ms]", sw.Elapsed);

    }

    static async Task<Transcript> ConvertTranscript(HttpClient client, VideoId videoId, Captiontrack track)
    {
        var newTranscr = Transcript.Create(videoId, track.LanguageCode);

        var xml = await client.GetStringAsync(track.BaseUrl);
        XmlDocument xmlDoc = new ();
        xmlDoc.LoadXml(xml);

        XmlNodeList? textNodes = xmlDoc.SelectNodes("//text"); // Select all 'text' nodes
        if (textNodes != null)
        {
            foreach (XmlNode textNode in textNodes)
            {
                var text = textNode.InnerText;
                var startTxt = textNode.Attributes?["start"]?.Value ?? "0";
                var durTxt = textNode.Attributes?["dur"]?.Value ?? "0";

                newTranscr.AddLine(
                    text,
                    TimeSpan.FromSeconds(double.Parse(durTxt)),
                    TimeSpan.FromSeconds(double.Parse(startTxt)));
            }
        }

        return newTranscr;
    }


    record TimedTextInfoForVideo(VideoId VideoId, TimedTextInformation TimedTextInformation);

    async Task<Result<TimedTextInfoForVideo>> GetTimedTextInformation(HttpClient client, VideoId videoId, string videoUrl)
    {
        client.DefaultRequestHeaders.Add("User-Agent", "Videomatic");

        try
        {
            string html = await client.GetStringAsync(videoUrl);

            var tmp = html.Split("\"captions\":");
            if (tmp.Length <= 1)
            {
                if (html.Contains("class=\"g-recaptcha\""))
                {
                    return Result.Error($"Too many requests for video {videoUrl}.");
                }

                if (!html.Contains("\"playabilityStatus\":"))
                {
                    return Result.Error($"Unavailable video {videoUrl}.");
                }

                return Result.Error($"Transcripts disabled in video {videoUrl}.");
            }

            var targetHtml = tmp[1];
            var json = targetHtml[..(targetHtml.IndexOf(']') + 1)] + "}}";

            var ttInfo = JsonConvert.DeserializeObject<TimedTextInformation>(json);
            return new TimedTextInfoForVideo(videoId, ttInfo!);
        }
        catch (HttpRequestException ex)
        {
            var msg = $"Error while getting video {videoUrl}.";
#pragma warning disable CA2254 // Template should be a static expression
            Logger.LogError(ex, msg, videoUrl);
#pragma warning restore CA2254 // Template should be a static expression

            return Result.Error(msg);
        }
    }
      

}