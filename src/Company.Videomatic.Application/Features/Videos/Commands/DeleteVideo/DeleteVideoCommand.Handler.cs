using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;

public partial class DeleteVideoCommand
{
    /// <summary>
    /// The handler for DeleteVideoCommand.    
    /// Triggers <see cref="VideoDeletedEvent"/>.
    /// </summary>
    public class DeleteVideoLinkHandler : IRequestHandler<DeleteVideoCommand, DeleteVideoResponse>
    {
        readonly IRepository<Video> _repository;
        readonly IPublisher _publisher;

        public DeleteVideoLinkHandler(IRepository<Video> repository, IPublisher publisher)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var target = await _repository.GetByIdAsync(request.VideoId, cancellationToken);
            if (target == null)
                return new DeleteVideoResponse(null, false);

            await _repository.DeleteAsync(target, cancellationToken);

            await _publisher.Publish(new VideoDeletedEvent(target));
            
            return new DeleteVideoResponse(target, true);
        }
    }
}
