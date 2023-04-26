namespace Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;

public partial class DeleteVideoCommand
{
    /// <summary>
    /// The handler for DeleteVideoCommand.    
    /// Triggers <see cref="VideoDeletedEvent"/>.
    /// </summary>
    public class DeleteVideoLinkHandler : IRequestHandler<DeleteVideoCommand, DeleteVideoResponse>
    {
        readonly IVideoRepository _repository;
        readonly IPublisher _publisher;

        public DeleteVideoLinkHandler(IVideoRepository repository, IPublisher publisher)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var wasDeleted = await _repository.DeleteVideoAsync(request.VideoId);

            if (wasDeleted)
            {
                await _publisher.Publish(new VideoDeletedEvent(request.VideoId));
            }

            return new DeleteVideoResponse(request.VideoId, wasDeleted);
        }
    }
}
