using Company.SharedKernel.Queries;

namespace Company.Videomatic.Application.Features.Videos.Commands.UpdateVideo;

public partial class UpdateVideoCommand
{
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
            var query = new GetEntityQuery<Video>(request.VideoId);
            var video = await _storage.FirstOrDefaultAsync(query, cancellationToken);
            if (video is null)
                return new UpdateVideoResponse(null, false);
            
            // Updates the video.
            video.Title = request.Title;
            video.Description = request.Description;
            
            await _storage.UpdateAsync(video, cancellationToken);

            //  Publishes the event and returns the response.
            await _publisher.Publish(new VideoUpdatedEvent(video.Id));
            
            return new UpdateVideoResponse(video, true);
        }
    }
}