using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Domain.Extensions;
using Company.Videomatic.Domain.Specifications;
using Company.Videomatic.Domain.Specifications.Videos;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Queries;

//public class GetVideosHandler : BaseRequestHandler<GetVideosQuery, PageResult<VideoDTO>>
public class GetVideosHandler : 
    IRequestHandler<GetVideosQuery, PageResult<VideoDTO>>,
    IRequestHandler<GetVideosByIdQuery, IEnumerable<VideoDTO>>
{
    public GetVideosHandler(IReadRepository<Video> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    readonly IReadRepository<Video> _repository;
    readonly IMapper _mapper;

    public async Task<PageResult<VideoDTO>> Handle(GetVideosQuery request, CancellationToken cancellationToken = default)
    {
        var spec = new VideosFilteredAndPaginated(
            request.SearchText,
            request.PlaylistIds,
            request.Page,
            request.PageSize,
            request.OrderBy);

        var res = await _repository.PageAsync<Video, VideoDTO>(
            spec,
            spec.Page,
            spec.PageSize,
            vid => MapVideo(vid, request.Resolution?.ToThumbnailResolution()),
            cancellationToken);

        return res;
    }
    
    public async Task<IEnumerable<VideoDTO>> Handle(GetVideosByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new VideosByIdSpecification(request.VideoIds.Select(x => new VideoId(x)));

        var videos = await _repository.ListAsync(spec, cancellationToken);        

        return videos.Select(vid => MapVideo(vid, request.Resolution?.ToThumbnailResolution()));
    }

    private VideoDTO MapVideo(Video video, ThumbnailResolution? expectedRes)
    {
        var dto = new VideoDTO(
            video.Id,
            video.Location,
            video.Name,
            video.Description,
            video.Tags.Select(x => x.Name).ToArray(),
            video.GetThumbnail(expectedRes ?? ThumbnailResolution.Default).Location);
        
        return dto;        
    }
}