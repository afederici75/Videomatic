using Company.SharedKernel.Abstractions;
using Company.Videomatic.Application.Features.Videos.Queries;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Aggregates.Video;
using Company.Videomatic.Infrastructure.YouTube.API.JsonPasteSpecial;
using MediatR;
using Newtonsoft.Json;

namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubePlaylistsHelper : IYouTubeHelper
{
    public const string ProviderId = "YOUTUBE";

    public YouTubePlaylistsHelper(IOptions<YouTubeOptions> options, HttpClient client, ISender sender, IRepository<Video> videoRepository, IRepository<Transcript> transcriptRepository)
    {
        // TODO: messy / unfinished
        _options = options.Value;
        _client = client;
        _videoRepository = videoRepository;
        _transcriptRepository = transcriptRepository;
        _client.BaseAddress = new Uri("https://www.googleapis.com/youtube/v3/");
        _sender = sender;        
    }

    readonly YouTubeOptions _options;
    readonly HttpClient _client;
    IRepository<Video> _videoRepository;
    IRepository<Transcript> _transcriptRepository;
    ISender _sender;

    public async IAsyncEnumerable<Video> ImportVideosOfPlaylist(string playlistId)
    {
        var parameters = new Dictionary<string, string>
        {
            ["key"] = _options.ApiKey,
            ["playlistId"] = playlistId,
            //["part"] = "snippet,contentDetails,id,status",
            ["part"] = "contentDetails,status",
            ["maxResults"] = "50"
        };

        API.JsonPasteSpecial.PlaylistItemListResponse response;
        do
        {
            // API call
            var fullUrl = MakeUrlWithQuery("playlistItems", parameters);
            string json = await _client.GetStringAsync(fullUrl);
            response = JsonConvert.DeserializeObject<API.JsonPasteSpecial.PlaylistItemListResponse>(json)!;

            // Gets all video Ids
            var publicVideos = response.items.Where(i => i.status.privacyStatus == "public")
                                             .Select(i => i.contentDetails.videoId);

            var videoIds = new HashSet<string>(publicVideos);
            
            await foreach (var video in ImportVideos(videoIds.ToArray()))
            { 
                yield return video;
            };

            // Pages
            parameters["pageToken"] = response.nextPageToken;
        }
        while (!string.IsNullOrEmpty(response.nextPageToken));
    }    

    public async IAsyncEnumerable<Video> ImportVideos(IEnumerable<string> youtubeVideoIds)
    {
        // TODO: should page, e.g. if we send 200 ids it should page in 50 items blocks

        var parameters = new Dictionary<string, string>
        {
            ["key"] = _options.ApiKey,
            ["id"] = string.Join(',', youtubeVideoIds),
            ["part"] = "snippet,contentDetails,id,status",
            //["maxResults"] = "50"
        };

        API.JsonPasteSpecial.VideoListResponse response;
        var fullUrl = MakeUrlWithQuery("videos", parameters);

        string json = await _client.GetStringAsync(fullUrl);
        response = JsonConvert.DeserializeObject<VideoListResponse>(json)!;

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

        var response = await _sender.Send(new GetProviderVideoIdsQuery(videoIds));
        IReadOnlyDictionary<long, string> videoIdsByVideoId = response.Value;

        using var api = new YoutubeTranscriptApi.YouTubeTranscriptApi();

        foreach (IEnumerable<string> responsePage in response.Value.Values.Page(50))
        {
            (Dictionary<string, IEnumerable<YoutubeTranscriptApi.TranscriptItem>>, IReadOnlyList<string>) allTranscripts = new ();

            var currentSet = responsePage.ToHashSet();
            var badTranscripts = new List<Transcript>();
            while (currentSet.Count > 0)
            {
                try
                {
                    // Yuck .ToList() below
                    allTranscripts = api.GetTranscripts(currentSet.ToList(), continue_after_error: true); // Returns an ugly (Dictionary<string, IEnumerable<YoutubeTranscriptApi.TranscriptItem>>, IReadOnlyList<string>) 
                    break;
                }
                catch (YoutubeTranscriptApi.NoTranscriptFound ex)
                {
                    var videoId = videoIdsByVideoId.First(x => x.Value==ex.VideoId).Key;
                    var badTranscrit = Transcript.Create(videoId, "??", new[] { $"No transcript found for video {ex.VideoId}." });
                    
                    badTranscripts.Add(badTranscrit);

                    currentSet.Remove(ex.VideoId);                    
                }
                catch (YoutubeTranscriptApi.TranscriptsDisabled ex)
                {
                    var videoId = videoIdsByVideoId.First(x => x.Value == ex.VideoId).Key;
                    var badTranscrit = Transcript.Create(videoId, "??", new[] { $"Transcripts disabled for video {ex.VideoId}." });

                    badTranscripts.Add(badTranscrit);

                    currentSet.Remove(ex.VideoId);                    
                }
            }

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
}