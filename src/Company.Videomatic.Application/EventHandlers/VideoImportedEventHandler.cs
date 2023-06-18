//namespace Company.Videomatic.Application.EventHandlers;

//public class VideoImportedEventHandler : INotificationHandler<VideoImportedEvent>
//{
//    readonly IVideoRepository _repository;

//    public VideoImportedEventHandler(
//        IVideoRepository repository)
//    {
//        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
//    }
//    public async Task Handle(VideoImportedEvent request, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//        //var newVideo = await _repository.GetByIdAsync(
//        //    request.VideoId,
//        //    includes: new[] { nameof(Video.Transcripts), nameof(Video.Transcripts) + '.' + nameof(Transcript.Lines) },
//        //    cancellationToken);
//        //Guard.Against.Null(newVideo, nameof(newVideo), $"Video with id {request.VideoId} not found.");
//        //
//        //// Generates artifacts for the video
//        //// TODO: Use Polly to retry
//        //var summaryTask = _analyzer.SummarizeVideoAsync(newVideo);
//        //var reviewTask = _analyzer.ReviewVideoAsync(newVideo);
//        //
//        //Artifact[] artifacts = await Task.WhenAll(summaryTask, reviewTask);
//        //
//        //newVideo.AddArtifacts(artifacts);
//        //
//        //// Saves
//        //await _repository.UpdateRangeAsync(new[] { newVideo }, cancellationToken);
//    }
//}