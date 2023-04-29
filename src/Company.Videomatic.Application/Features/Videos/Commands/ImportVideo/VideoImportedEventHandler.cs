using Ardalis.GuardClauses;
using Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;

namespace Company.Videomatic.Application.Features.Videos.Commands.ImportVideo;

public class VideoImportedEventHandler : INotificationHandler<VideoImportedEvent>
{
    readonly IVideoAnalyzer _analyzer;
    readonly IRepositoryBase<Video> _repository;
    
    public VideoImportedEventHandler(
        IVideoAnalyzer importer,
        IRepositoryBase<Video> repository)
    {
        _analyzer = importer ?? throw new ArgumentNullException(nameof(importer));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));        
    }
    public async Task Handle(VideoImportedEvent request, CancellationToken cancellationToken)
    {
        var newVideo = await _repository.GetByIdAsync(request.VideoId, cancellationToken);
        Guard.Against.Null(newVideo, nameof(newVideo), $"Video with id {request.VideoId} not found.");

        // Generates artifacts for the video
        // TODO: Use Polly to retry
        Task<Artifact> summaryTask = _analyzer.SummarizeVideoAsync(newVideo);
        Task<Artifact> reviewTask = _analyzer.ReviewVideoAsync(newVideo);

        Artifact[] artifacts = await Task.WhenAll(summaryTask, reviewTask);

        newVideo.AddArtifacts(artifacts);

        // Saves
        await _repository.UpdateAsync(newVideo, cancellationToken);
    }
}