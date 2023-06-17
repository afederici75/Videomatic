namespace Company.Videomatic.Application.Features.Videos;

public record AddCollectionsToVideo(int VideoId, int[] CollectionIds) : IRequest<AddCollectionsToVideoResponse>;

public record AddCollectionsToVideoResponse(int addedToCount);

public class AddCollectionsToVideoHandler : IRequestHandler<AddCollectionsToVideo, AddCollectionsToVideoResponse>
{
    private readonly IRepository<Video> _videoRepository;
    private readonly IRepository<Playlist> _collectionRepository;
    private readonly IPublisher _publisher;
    public AddCollectionsToVideoHandler(IRepository<Video> videoRepository, IRepository<Playlist> collectionRepository, IPublisher publisher)
    {
        _videoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        _collectionRepository = collectionRepository ?? throw new ArgumentNullException(nameof(collectionRepository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public async Task<AddCollectionsToVideoResponse> Handle(AddCollectionsToVideo request, CancellationToken cancellationToken)
    {
        //var video = await _videoRepository.GetByIdAsync(request.VideoId, null, cancellationToken);
        //if (video is null)
        //    return new(addedToCount: 0);
        //var collections = await _collectionRepository.GetByIdsAsync(request.CollectionIds, null, cancellationToken);
        //video.AddCollections(collections);
        //await _videoRepository.UpdateRangeAsync(new[] { video }, cancellationToken);
        //await _publisher.Publish(new VideoUpdatedEvent(video.Id), cancellationToken);
        //return new(addedToCount: collections.Count());

        throw new NotImplementedException();
    }
}