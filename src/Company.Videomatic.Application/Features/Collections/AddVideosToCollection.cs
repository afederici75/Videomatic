namespace Company.Videomatic.Application.Features.Collections;

public record AddVideosToCollectionCommand(string[] VideoUrls) : IRequest<AddVideosToCollectionResponse>;

public record AddVideosToCollectionResponse(Guid CollectionId, int VideoCount);

public class AddVideosToCollectionHandler : IRequestHandler<AddVideosToCollectionCommand, AddVideosToCollectionResponse>
{
    private readonly IRepository<Collection> _collectionRepository;

    public AddVideosToCollectionHandler(IRepository<Collection> collectionRepository)
    {
        _collectionRepository = collectionRepository ?? throw new ArgumentNullException(nameof(collectionRepository));
    }

    public async Task<AddVideosToCollectionResponse> Handle(AddVideosToCollectionCommand request, CancellationToken cancellationToken)
    {

        //var videos = await _videoRepository.GetVideosByUrlAsync(request.VideoUrls, cancellationToken);
        //var collection = new Collection(videos);
        //await _collectionRepository.AddAsync(collection, cancellationToken);
        //return new AddVideosToCollectionResponse(collection.Id);
        throw new NotImplementedException();
    }
}