namespace Company.Videomatic.Application.Features.Collections;

public record ImportCollectionCommand(string collectionUrl) : IRequest<ImportCollectionResponse>;

public record ImportCollectionResponse(bool found);

public class ImportCollectionHandler : IRequestHandler<ImportCollectionCommand, ImportCollectionResponse>
{
    private readonly IRepository<Collection> _collectionRepository;
    private readonly IRepository<Video> _videoRepository;
    private readonly IPublisher _publisher;
    public ImportCollectionHandler(IRepository<Collection> collectionRepository, IRepository<Video> videoRepository, IPublisher publisher)
    {
        _collectionRepository = collectionRepository ?? throw new ArgumentNullException(nameof(collectionRepository));
        _videoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public async Task<ImportCollectionResponse> Handle(ImportCollectionCommand request, CancellationToken cancellationToken)
    {
        //var collection = await _collectionRepository.GetByUrlAsync(request.collectionUrl, cancellationToken);
        //if (collection is null)
        //    return new(false);
        //var videos = await _videoRepository.GetVideosByUrlAsync(collection.VideoUrls, cancellationToken);
        //collection.AddVideos(videos);
        //await _collectionRepository.UpdateRangeAsync(new[] { collection }, cancellationToken);
        //await _publisher.Publish(new CollectionUpdatedEvent(collection.Id), cancellationToken);
        //return new(true);

        throw new NotImplementedException();
    }
}