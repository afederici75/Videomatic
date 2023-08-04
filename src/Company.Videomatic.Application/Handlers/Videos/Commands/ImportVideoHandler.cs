using Hangfire;

namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class ImportVideoHandler : IRequestHandler<ImportYoutubeVideosCommand, ImportYoutubeVideosResponse>
{
    readonly IRepository<Video> Repository;
    readonly IYouTubeHelper YouTubeHelper;
    readonly IMapper Mapper;
    readonly IPlaylistService PlaylistService;
    readonly IBackgroundJobClient JobClient;

    public ImportVideoHandler(IBackgroundJobClient jobClient, IRepository<Video> repository, IYouTubeHelper youTubeHelper, IMapper mapper, IPlaylistService playlistService)
    {
        JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        YouTubeHelper = youTubeHelper ?? throw new ArgumentNullException(nameof(youTubeHelper));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        PlaylistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
    }

    public async Task<ImportYoutubeVideosResponse> Handle(ImportYoutubeVideosCommand request, CancellationToken cancellationToken = default)
    {
        var jobIds = new List<string>();
        foreach (var url in request.Urls)
        {
            var jobId = JobClient.Enqueue<ImportVideoHandler>(x => x.ImportVideoJob(url, request.DestinationPlaylistId));
            jobIds.Add(jobId);
        }

        return new ImportYoutubeVideosResponse(true, jobIds, request.DestinationPlaylistId);        
    }

    public async Task ImportVideoJob(string url, int? playlistId)
    {
        await foreach (var v in YouTubeHelper.ImportVideosByUrl(new[] { url }))
        { 
            var savedVideo = await Repository.AddAsync(v);

            if (!playlistId.HasValue)
                continue;
            
            var linkRes = await PlaylistService.LinkToPlaylists(playlistId.Value, new[] { savedVideo.Id });
        }
    }
}