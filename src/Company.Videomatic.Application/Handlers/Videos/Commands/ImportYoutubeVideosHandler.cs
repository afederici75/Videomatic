using Hangfire;
using MediatR;

namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class ImportYoutubeVideosHandler : IRequestHandler<ImportYoutubeVideosCommand, ImportYoutubeVideosResponse>
{
    readonly IRepository<Video> Repository;
    readonly IYouTubeHelper YouTubeHelper;
    readonly IMapper Mapper;
    readonly IPlaylistService PlaylistService;
    readonly IRepository<Transcript> TranscriptRepository;
    readonly IBackgroundJobClient JobClient;

    public ImportYoutubeVideosHandler(
        IBackgroundJobClient jobClient, 
        IRepository<Video> repository, 
        IYouTubeHelper youTubeHelper, 
        IMapper mapper, 
        IPlaylistService playlistService,
        IRepository<Transcript> transcriptRepository)
    {
        JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        YouTubeHelper = youTubeHelper ?? throw new ArgumentNullException(nameof(youTubeHelper));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        PlaylistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        TranscriptRepository = transcriptRepository ?? throw new ArgumentNullException(nameof(transcriptRepository));
    }

    public async Task<ImportYoutubeVideosResponse> Handle(ImportYoutubeVideosCommand request, CancellationToken cancellationToken = default)
    {
        var jobIds = new List<string>();
        foreach (var url in request.Urls)
        {
            var jobId = JobClient.Enqueue<ImportYoutubeVideosHandler>(x => x.ImportVideoJob(url, request.DestinationPlaylistId));
            jobIds.Add(jobId);
        }

        return new ImportYoutubeVideosResponse(true, jobIds, request.DestinationPlaylistId);        
    }

    public async Task ImportVideoJob(string url, int? playlistId)
    {
        await foreach (var v in YouTubeHelper.ImportVideosByUrl(new[] { url }))
        { 
            var savedVideo = await Repository.AddAsync(v);

            JobClient.Enqueue<ImportYoutubeVideosHandler>(x => x.ImportTranscriptionsOfVideo(savedVideo.Id));

            if (!playlistId.HasValue)
                continue;
            
            var linkRes = await PlaylistService.LinkToPlaylists(playlistId.Value, new[] { savedVideo.Id });            
        }
    }

    public async Task ImportTranscriptionsOfVideo(VideoId videoId)
    {
        await foreach (var transcript in YouTubeHelper.ImportTranscriptions(new[] { videoId }))
        {
            await TranscriptRepository.AddAsync(transcript);
        }
    }
}