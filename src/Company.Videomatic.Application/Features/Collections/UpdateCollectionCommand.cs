namespace Company.Videomatic.Application.Features.Collections;

public record UpdateCollectionCommand(int Id, string Name, string Description) : IRequest<UpdateCollectionResponse>;

public record UpdateCollectionResponse();

public class UpdateCollectionHandler : IRequestHandler<UpdateCollectionCommand, UpdateCollectionResponse>
{
    private readonly IRepository<Collection> _collectionRepository;
    private readonly IPublisher _publisher;
    public UpdateCollectionHandler(IRepository<Collection> collectionRepository, IPublisher publisher)
    {
        _collectionRepository = collectionRepository ?? throw new ArgumentNullException(nameof(collectionRepository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public async Task<UpdateCollectionResponse> Handle(UpdateCollectionCommand request, CancellationToken cancellationToken)
    {
        //var collection = await _collectionRepository.GetByIdAsync(request.Id, null, cancellationToken);
        //if (collection is null)
        //    return new();
        //collection.Name = request.Name;
        //collection.Description = request.Description;
        //await _collectionRepository.UpdateRangeAsync(new[] { collection }, cancellationToken);
        //await _publisher.Publish(new CollectionUpdatedEvent(collection.Id), cancellationToken);
        //return new();

        throw new NotImplementedException();
    }
}