using Company.Videomatic.Application.Features.Videos.UpdateVideo;

namespace Company.Videomatic.Application.Features.Videos.UpdateVideo;

/// <summary>
/// The handler for UpdateVideoCommand.
/// </summary>
public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, UpdateVideoResponse>
{
    private readonly IRepository<Video> _storage;
    private readonly IPublisher _publisher;

    public UpdateVideoCommandHandler(IRepository<Video> videoStorage, IPublisher publisher)
    {
        _storage = videoStorage ?? throw new ArgumentNullException(nameof(videoStorage));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task<UpdateVideoResponse> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
    {
        // Looks up the video by id.
        var video = await _storage.GetByIdAsync(request.VideoId, null, cancellationToken);
        if (video is null)
            return new(Video: null, Updated: false);

        // Updates the video.
        // TODO: should use a mapper.
        video.Title = request.Title;
        video.Description = request.Description;

        await _storage.UpdateRangeAsync(new[] { video }, cancellationToken);

        //  Publishes the event and returns the response.
        await _publisher.Publish(new VideoUpdatedEvent(video.Id), cancellationToken);

        return new(Video: video, Updated: true);
    }
}