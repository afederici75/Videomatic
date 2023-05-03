using Ardalis.Specification;
using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Features.Videos.DeleteVideo;

/// <summary>
/// The handler for DeleteVideoCommand.    
/// Triggers <see cref="VideoDeletedEvent"/>.
/// </summary>
public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, DeleteVideoResponse>
{
    readonly IRepository<Video> _repository;
    readonly IPublisher _publisher;

    public DeleteVideoCommandHandler(IRepository<Video> repository, IPublisher publisher)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
    {
        var target = await _repository.GetByIdAsync(request.VideoId, null, cancellationToken);
        if (target == null)
            return new DeleteVideoResponse(null, false);

        await _repository.DeleteRangeAsync(new[] { target }, cancellationToken);

        await _publisher.Publish(new VideoDeletedEvent(target), cancellationToken);

        return new DeleteVideoResponse(target, true);
    }
}