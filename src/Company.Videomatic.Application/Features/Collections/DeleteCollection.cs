namespace Company.Videomatic.Application.Features.Collections;

public record DeleteCollectionCommand(int CollectionId) : IRequest<DeleteCollectionResponse>;

public record DeleteCollectionResponse(Video? Video, bool Deleted);

public class DeleteCollectionHandler : IRequestHandler<DeleteCollectionCommand, DeleteCollectionResponse>
{
    private readonly IRepository<Collection> _collectionRepository;
    private readonly IPublisher _publisher;
    public DeleteCollectionHandler(IRepository<Collection> collectionRepository, IPublisher publisher)
    {
        _collectionRepository = collectionRepository ?? throw new ArgumentNullException(nameof(collectionRepository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public async Task<DeleteCollectionResponse> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
    {
        //var collection = await _collectionRepository.GetByIdAsync(request.CollectionId, null, cancellationToken);
        //if (collection is null)
        //    return new(Video: null, Deleted: false);
        //await _collectionRepository.DeleteAsync(collection, cancellationToken);
        //await _publisher.Publish(new CollectionDeletedEvent(collection.Id), cancellationToken);
        //return new(Video: null, Deleted: true);

        throw new NotImplementedException();
    }
}