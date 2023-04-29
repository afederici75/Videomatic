namespace Company.Videomatic.Application.Features.Videos.ImportVideo;

/// <summary>
/// The handler for ImportVideoCommand.
/// </summary>
public class ImportVideoCommandHandler : IRequestHandler<ImportVideoCommand, ImportVideoResponse>
{
    readonly IVideoImporter _importer;
    readonly IRepositoryBase<Video> _repository;
    readonly IPublisher _publisher;

    public ImportVideoCommandHandler(
        IVideoImporter importer,
        IRepositoryBase<Video> repository,
        IPublisher publisher)
    {
        _importer = importer ?? throw new ArgumentNullException(nameof(importer));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _publisher = publisher;
    }

    public async Task<ImportVideoResponse> Handle(ImportVideoCommand request, CancellationToken cancellationToken)
    {
        // Imports the video from the provider url
        Video newVideo = await _importer.ImportAsync(new Uri(request.VideoUrl));

        // Generates artifacts for the video
        // TODO: these should be queued and processed asynchronously
        //Task<Artifact> summaryTask = _analyzer.SummarizeVideoAsync(newVideo);
        //Task<Artifact> reviewTask = _analyzer.ReviewVideoAsync(newVideo);

        //Artifact[] artifacts = await Task.WhenAll(summaryTask, reviewTask);

        //newVideo.AddArtifacts(artifacts);

        // Creates the video 
        var savedVideo = await _repository.AddAsync(newVideo);

        await _publisher.Publish(new VideoImportedEvent(
            VideoId: savedVideo.Id,
            ThumbNailCount: savedVideo.Thumbnails.Count(),
            TranscriptCount: savedVideo.Transcripts.Count(),
            ArtifactsCount: savedVideo.Artifacts.Count()));

        return new ImportVideoResponse(VideoId: savedVideo.Id);
    }
}
