namespace Company.Videomatic.Application.Features.Videos;

/// <summary>
/// This command is used to update a video in the repository.
/// </summary>
public record UpdateVideoCommand(int VideoId, string? Title = default, string? Description = default) : IRequest<UpdateVideoResponse>;

/// <summary>
/// The response from the UpdateVideoCommand.
/// </summary>
/// <param name="Video"></param>
/// <param name="Updated"></param>
public record UpdateVideoResponse(Video? Video, bool Updated);

/// <summary>
/// This event is published when a video is updated.
/// </summary>
/// <param name="VideoId"></param>
public record VideoUpdatedEvent(int VideoId);

/// <summary>
/// The handler for UpdateVideoCommand.
/// </summary>
public class UpdateVideoHandler : IRequestHandler<UpdateVideoCommand, UpdateVideoResponse>
{
    private readonly IVideoRepository _storage;
    private readonly IPublisher _publisher;

    public UpdateVideoHandler(IVideoRepository videoStorage, IPublisher publisher)
    {
        _storage = videoStorage ?? throw new ArgumentNullException(nameof(videoStorage));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task<UpdateVideoResponse> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // Looks up the video by id.
        //var video = await _storage.GetByIdAsync(request.VideoId, null, cancellationToken);
        //if (video is null)
        //    return new(Video: null, Updated: false);
        //
        //// Updates the video.
        //// TODO: should use a mapper.
        //video.Title = request.Title;
        //video.Description = request.Description;
        //
        //await _storage.UpdateRangeAsync(new[] { video }, cancellationToken);
        //
        ////  Publishes the event and returns the response.
        //await _publisher.Publish(new VideoUpdatedEvent(video.Id), cancellationToken);
        //
        //return new(Video: video, Updated: true);
    }
}