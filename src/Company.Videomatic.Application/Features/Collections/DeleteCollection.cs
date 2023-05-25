using Company.SharedKernel.Abstractions;
using Company.Videomatic.Application.Features.Videos;

namespace Company.Videomatic.Application.Features.Collections;

/// <summary>
/// Deletes an existing collection.
/// </summary>
/// <param name="id"></param>
public record DeleteCollectionCommand(int Id) : IRequest<DeleteCollectionResponse>;

/// <summary>
/// This response is returned by DeleteCollectionCommand.
/// </summary>
/// <param name="Item"></param>
/// <param name="Deleted"></param>
public record DeleteCollectionResponse(Collection? Item, bool Deleted);

/// <summary>
/// This event is published when a video is deleted.
/// </summary>
public record CollectionDeletedEvent(Collection Item) : INotification;

/// <summary>
/// Handles the DeleteCollectionCommand.
/// </summary>
public class DeleteCollectionHandler : IRequestHandler<DeleteCollectionCommand, DeleteCollectionResponse>
{
    private readonly IRepository<Collection> _repository;
    private readonly IPublisher _publisher;
    public DeleteCollectionHandler(IRepository<Collection> collectionRepository, IPublisher publisher)
    {
        _repository = collectionRepository ?? throw new ArgumentNullException(nameof(collectionRepository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public async Task<DeleteCollectionResponse> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
    {
        var target = await _repository.GetByIdAsync(request.Id, null, cancellationToken);
        if (target == null)
            return new DeleteCollectionResponse(null, false);

        await _repository.DeleteRangeAsync(new[] { target }, cancellationToken);

        await _publisher.Publish(new CollectionDeletedEvent(target!), cancellationToken);

        return new DeleteCollectionResponse(target, true);               
    }
}