using Company.Videomatic.Application.Features.Videos;

namespace Company.Videomatic.Application.Features.Collections;

public record UpdateCollectionCommand(int Id, string Name, string Description) : IRequest<UpdateCollectionResponse>;

public record UpdateCollectionResponse(int Id, bool Updated);

/// <summary>
/// This event is published when a video is updated.
/// </summary>
/// <param name="VideoId"></param>
public record CollectionUpdatedEvent(int VideoId);

public class UpdateCollectionHandler : IRequestHandler<UpdateCollectionCommand, UpdateCollectionResponse>
{
    private readonly IRepository<Playlist> _repository;
    private readonly IPublisher _publisher;
    public UpdateCollectionHandler(IRepository<Playlist> collectionRepository, IPublisher publisher)
    {
        _repository = collectionRepository ?? throw new ArgumentNullException(nameof(collectionRepository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public async Task<UpdateCollectionResponse> Handle(UpdateCollectionCommand request, CancellationToken cancellationToken)
    {
        //// Looks up the video by id.
        //Collection? collection = await _repository.GetByIdAsync(request.Id, null, cancellationToken);
        //if (collection is null)
        //    return new(request.Id, false);
        //
        //// Updates the video.
        //// TODO: should use a mapper.
        //collection.Name = request.Name;
        //collection.Description = request.Description;
        //
        //await _repository.UpdateRangeAsync(new[] { collection }, cancellationToken);
        //
        ////  Publishes the event and returns the response.
        //await _publisher.Publish(new VideoUpdatedEvent(collection.Id), cancellationToken);
        //
        //return new(collection.Id, Updated: true);

        throw new NotImplementedException();
    }
}