namespace Company.Videomatic.Application.Features.Videos.Commands.UpdateVideo;

public partial class UpdateVideoCommand
{
    /// <summary>
    /// The handler for UpdateVideoCommand.
    /// </summary>
    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, UpdateVideoResponse>
    {
        private readonly IVideoStorage _storage;
        private readonly IPublisher _publisher;

        public UpdateVideoCommandHandler(IVideoStorage videoStorage, IPublisher publisher)
        {
            _storage = videoStorage ?? throw new ArgumentNullException(nameof(videoStorage));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<UpdateVideoResponse> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            // Looks up the video by id.
            var video = await _storage.GetVideoByIdAsync(request.VideoId);
            if (video is null)
                return new UpdateVideoResponse { Updated = false };
            
            // Updates the video.
            video.Title = request.Title;
            video.Description = request.Description;
            
            var count = await _storage.UpdateVideoAsync(video);

            //  Publishes the event and returns the response.
            if (count > 0)
            {
                await _publisher.Publish(new VideoUpdatedEvent(video.Id));
            }            

            return new UpdateVideoResponse { Updated = count > 0 };
        }
    }
}