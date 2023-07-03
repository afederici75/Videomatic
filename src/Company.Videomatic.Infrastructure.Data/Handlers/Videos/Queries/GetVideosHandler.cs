using Company.Videomatic.Domain.Extensions;
using Company.Videomatic.Domain.Specifications;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Queries;

//public class GetVideosHandler : BaseRequestHandler<GetVideosQuery, PageResult<VideoDTO>>
public class GetVideosHandler : IRequestHandler<GetVideosQuery, PageResult<VideoDTO>>
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

        var page = await _repository.PageAsync(spec, cancellationToken);

        // Mapping
        ThumbnailResolution expectedRes = request.IncludeThumbnail?.ToThumbnailResolution() ?? ThumbnailResolution.Default;
        var dtoQuery = from video in page.Items
                       from thumb in video.Thumbnails
                       where thumb.Resolution == expectedRes
                       select new VideoDTO(
                video.Id,
                video.Location,
                video.Name,
                video.Description,
                -1, //(int?)(request.IncludeCounts ? video.PlaylistVideos.Count : null),
                -1,//(int?)(request.IncludeCounts ? video.Artifacts.Count : null),
                (int?)(request.IncludeCounts ? video.Thumbnails.Count : null),
                -1,//(int?)(request.IncludeCounts ? video.Transcripts.Count : null),
                (int?)(request.IncludeCounts ? video.Tags.Count : null),
                thumb.Location ?? ""
            );;
        
        var result = new PageResult<VideoDTO>(dtoQuery, spec.Page, spec.PageSize, page.TotalCount);
        return result;
    }
}