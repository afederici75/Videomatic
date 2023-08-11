using Ardalis.Result;
using Company.SharedKernel.Abstractions;
using Company.Videomatic.Domain.Aggregates.Video;
using Hangfire;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.XPath;
//using YoutubeTranscriptApi;

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

    public async Task ImportPlaylistsAsync(IEnumerable<string> idsOrUrls, ImportOptions? options = default, CancellationToken cancellation = default)        
    {
        options ??= new ImportOptions();

        await foreach (var page in Provider.GetPlaylistsAsync(idsOrUrls, cancellation).PageAsync(YouTubeVideoProvider.MaxYouTubeItemsPerPage))
        {
            var playlists = page.Select(gpl => MapToPlaylist(gpl));

            // TODO: figure out why AddRangeAsync does not work: it does not update the Id property!!!
            // IEnumerable<Playlist> res = await PlaylistRepository.AddRangeAsync(playlists, cancellation);

            foreach (var pl in playlists)
            {
                var res = await PlaylistRepository.AddAsync(pl, cancellation);

                switch (options.ExecuteWithoutJobQueue)
                {
                    case true:
                        await this.ImportVideosAsync(res.Id, options, cancellation);
                        break;
                    case false:
                        var jobId = JobClient.Enqueue<IVideoImporter>(imp => imp.ImportVideosAsync(res.Id, options, cancellation));
                        break;
                }                
            }                                    
        }
    }

    public async Task ImportVideosAsync(IEnumerable<string> idsOrUrls, PlaylistId? linkTo, ImportOptions? options = default, CancellationToken cancellation = default)
    {
        options ??= new ImportOptions();

        await foreach (var page in Provider.GetVideosAsync(idsOrUrls, cancellation).PageAsync(YouTubeVideoProvider.MaxYouTubeItemsPerPage))
        {
            var videos = page.Select(gv => MapToVideo(gv)).ToArray();


            // TODO: figure out why AddRangeAsync does not work: it does not update the Id property!!!
            //IEnumerable<Video> res = await VideoRepository.AddRangeAsync(videos, cancellation);           
            foreach (var v in videos)
            {
                var res = await VideoRepository.AddAsync(v, cancellation);

                if (linkTo != null)
                    await PlaylistRepository.LinkPlaylistToVideos(linkTo, new[] { res.Id });

                switch (options.ExecuteWithoutJobQueue)
                {
                    case true:
                        await this.ImportTranscriptionsAsync(new[] { res.Id }, cancellation);
                        break;
                    case false:
                        var jobId = JobClient.Enqueue<IVideoImporter>(imp => imp.ImportTranscriptionsAsync(new[] { res.Id }, cancellation));
                        break;
                }
            }            
        }
    }

    public async Task ImportVideosAsync(PlaylistId playlistId, ImportOptions? options = default, CancellationToken cancellation = default)
    {
        options ??= new ImportOptions();

        var pl = await PlaylistRepository.GetByIdAsync(playlistId, cancellation);
        if (pl?.Origin?.Id == null)
            throw new ArgumentException($"Playlist '{playlistId}' does not exist have an origin or does not exist.", nameof(playlistId));

        await foreach (IEnumerable<GenericVideo> videoPage in Provider.GetVideosOfPlaylistAsync(pl.Origin.Id, cancellation).PageAsync(YouTubeVideoProvider.MaxYouTubeItemsPerPage))
        {
            var videoIds = videoPage.Select(v => v.Id);

            await ImportVideosAsync(videoIds, playlistId, options, cancellation);
        }        
    }

    public async Task ImportTranscriptionsAsync(
        IEnumerable<VideoId> videoIds, CancellationToken cancellation = default)
    {        
        var videoIdsByProviderId = await VideoRepository.GetVideoProviderIds(videoIds, cancellation);
        using var client = new HttpClient();

        foreach (var v in videoIdsByProviderId)
        {
            var videoUrl = $"https://www.youtube.com/watch?v={v.Key}";

            // download the url and parse the transcript
            var ttInfo = await GetTimedTextInformation(client, videoUrl);
            if (!ttInfo.IsSuccess || ttInfo.Value.playerCaptionsTracklistRenderer?.captionTracks == null)
                continue;

            var allTracks = ttInfo.Value.playerCaptionsTracklistRenderer.captionTracks;
            
            var tasks = allTracks.Select(track => ConvertTranscript(client, v.Value, track));
            var results = await Task.WhenAll(tasks);

            var newTranscripts = results.Where(r => r.IsSuccess)
                                        .Select(r => r.Value)
                                        .ToArray();

            await TranscriptRepository.AddRangeAsync(newTranscripts, cancellation);            
        }

    }

    async Task<Result<Transcript>> ConvertTranscript(HttpClient client, VideoId videoId, Captiontrack track)
    {
        var newTranscr = Transcript.Create(videoId, track.languageCode);

        var xml = await client.GetStringAsync(track.baseUrl);
        XmlDocument xmlDoc = new XmlDocument();
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


    async Task<Result<TimedTextInformation>> GetTimedTextInformation(HttpClient client, string videoUrl)
    {
        client.DefaultRequestHeaders.Add("User-Agent", "Videomatic");

        var html = await client.GetStringAsync(videoUrl);

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
        var json = targetHtml.Substring(0, targetHtml.IndexOf(']') + 1) + "}}";

        var ttInfo = JsonConvert.DeserializeObject<TimedTextInformation>(json);
        return ttInfo!;
    }

    //public async Task ImportTranscriptionsAsync2(
    //    IEnumerable<VideoId> videoIds, CancellationToken cancellation = default)
    //{
    //    var videoIdsByProviderId = await VideoRepository.GetVideoProviderIds(videoIds, cancellation);

    //    using var api = new YoutubeTranscriptApi.YouTubeTranscriptApi();

    //    foreach (var responsePage in videoIdsByProviderId.Page(1))//YouTubeVideoProvider.MaxYouTubeItemsPerPage))
    //    {
    //        cancellation.ThrowIfCancellationRequested();

    //        var requestedProviderIds = responsePage.Select(x => x.Key).ToList();

    //        var languages = new List<string>() { "en", "es", "en-GB", "ko", "it" };

    //        var allTranscripts = api.GetTranscripts(
    //            requestedProviderIds, 
    //            languages: languages,
    //            continue_after_error: true); // Returns an ugly (Dictionary<string, IEnumerable<YoutubeTranscriptApi.TranscriptItem>>, IReadOnlyList<string>) 

    //        var badOnes = allTranscripts.Item2!.Select(badId =>
    //        {
    //            var videoId = videoIdsByProviderId[badId];
    //            return Transcript.Create(videoId, "??", new[] { $"Transcripts disabled or no transcript found for video '{badId}' [{videoId}]." });
    //        });

    //        foreach (var badOne in badOnes)
    //        {
    //            try
    //            {
    //                await TranscriptRepository.AddAsync(badOne, cancellation);
    //            }
    //            catch (Exception ex)
    //            { }
    //        }

    //        // ------------------------------
    //        Dictionary<string, IEnumerable<YoutubeTranscriptApi.TranscriptItem>>? goodOnes = allTranscripts.Item1;

    //        foreach (var goodTrans in goodOnes)
    //        {
    //            var videoId = videoIdsByProviderId[goodTrans.Key];

    //            var newTranscr = Transcript.Create(videoId, "US");
    //            foreach (var item in goodTrans.Value)
    //            {
    //                newTranscr.AddLine(item.Text, TimeSpan.FromSeconds(item.Duration), TimeSpan.FromSeconds(item.Start));
    //            }

    //            try 
    //            { 
    //                await TranscriptRepository.AddAsync(newTranscr, cancellation);
    //            }
    //            catch (Exception ex)
    //            { }
    //        }

    //        cancellation.ThrowIfCancellationRequested();
    //    }
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