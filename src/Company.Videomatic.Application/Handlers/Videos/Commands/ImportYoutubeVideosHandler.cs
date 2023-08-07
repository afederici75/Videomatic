using Hangfire;
using MediatR;

namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class ImportYoutubeVideosHandler : IRequestHandler<ImportYoutubeVideosCommand, ImportYoutubeVideosResponse>
{
    readonly IRepository<Video> Repository;
    readonly IYouTubeImporter YouTubeImporter;
    readonly IPlaylistService PlaylistService;
    readonly IRepository<Transcript> TranscriptRepository;
    readonly IBackgroundJobClient JobClient;

    public ImportYoutubeVideosHandler(
        IBackgroundJobClient jobClient, 
        IRepository<Video> repository, 
        IYouTubeImporter youTubeImporter, 
        IPlaylistService playlistService,
        IRepository<Transcript> transcriptRepository)
    {
        JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        YouTubeImporter = youTubeImporter ?? throw new ArgumentNullException(nameof(youTubeImporter));
        PlaylistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        TranscriptRepository = transcriptRepository ?? throw new ArgumentNullException(nameof(transcriptRepository));
    }

    public Task<ImportYoutubeVideosResponse> Handle(ImportYoutubeVideosCommand request, CancellationToken cancellationToken = default)
    {
        var jobIds = new List<string>();
        foreach (var url in request.Urls)
        {
            var jobId = JobClient.Enqueue<ImportYoutubeVideosHandler>(x => x.ImportVideoJob(url, request.DestinationPlaylistId));
            jobIds.Add(jobId);
        }

        return Task.FromResult(new ImportYoutubeVideosResponse(true, jobIds, request.DestinationPlaylistId));
    }

    public async Task ImportVideoJob(string url, int? playlistId)
    {
        await foreach (var v in YouTubeImporter.ImportVideos(new[] { url }))
        { 
            var savedVideo = await Repository.AddAsync(v);

            JobClient.Enqueue<ImportYoutubeVideosHandler>(x => x.ImportTranscriptionsOfVideo(savedVideo.Id));

            if (!playlistId.HasValue)
                continue;
            
            var linkRes = await PlaylistService.LinkPlaylistToVideos(playlistId.Value, new[] { savedVideo.Id });            
        }
    }

    public async Task ImportTranscriptionsOfVideo(VideoId videoId)
    {
        await foreach (var transcript in YouTubeImporter.ImportTranscriptions(new[] { videoId }))
        {
            await TranscriptRepository.AddAsync(transcript);
        }
    }
}