namespace Company.Videomatic.Application.Features.Videos;

public record AddTagsToVideoCommand(int VideoId, string[] Tags) : IRequest<AddTagsToVideoResponse>;

public record AddTagsToVideoResponse(int videoId);

public class AddTagsToVideoHandler : IRequestHandler<AddTagsToVideoCommand, AddTagsToVideoResponse>
{
    private readonly IRepository<Video> _videoRepository;
    
    private readonly IPublisher _publisher;
    public AddTagsToVideoHandler(IRepository<Video> videoRepository, IPublisher publisher)
    {
        _videoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public async Task<AddTagsToVideoResponse> Handle(AddTagsToVideoCommand request, CancellationToken cancellationToken)
    {
        //var video = await _videoRepository.GetByIdAsync(request.VideoId, null, cancellationToken);
        //if (video is null)
        //    return new(videoId: 0);
        //var tags = await _tagRepository.GetTagsByNameAsync(request.Tags, cancellationToken);
        //video.AddTags(tags);
        //await _videoRepository.UpdateRangeAsync(new[] { video }, cancellationToken);
        //await _publisher.Publish(new VideoUpdatedEvent(video.Id), cancellationToken);
        //return new(videoId: video.Id);

        throw new NotImplementedException();
    }
}