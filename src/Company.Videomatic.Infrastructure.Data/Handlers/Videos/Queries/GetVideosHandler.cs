using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Domain.Extensions;
using Company.Videomatic.Domain.Specifications;

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

        PageResult<Video> page = await _repository.PageAsync(spec, spec.Page, spec.PageSize, cancellationToken);

        // Mapping
        var result = _mapper.Map<PageResult<VideoDTO>>(page);

        return result;
    }
    
    public async Task<IEnumerable<VideoDTO>> Handle(GetVideosByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new VideoByIdsSpecification(request.VideoIds.Select(x => new VideoId(x)));

        var videos = await _repository.ListAsync(spec, cancellationToken);

        // Mapping
        IEnumerable<VideoDTO> result = MapVideos(
            request.Resolution?.ToThumbnailResolution() ?? ThumbnailResolution.Default, 
            videos);

        return result;
    }

    private static IEnumerable<VideoDTO> MapVideos(ThumbnailResolution expectedRes, IEnumerable<Video> videos)
    {
        var dtoQuery = from video in videos
                       from thumb in video.Thumbnails
                       where thumb.Resolution == expectedRes
                       select new VideoDTO(
                video.Id,
                video.Location,
                video.Name,
                video.Description,
                video.Tags.Select(x => x.Name).ToArray(),
                video.GetThumbnail(expectedRes).Location);                

        var result = dtoQuery.ToArray();
        return result;
    }

   //private static PageResult<VideoDTO> MapVideos(
   //    GetVideosByIdQuery request,
   //    IEnumerable<Video> videos,         
   //    ThumbnailResolutionDTO? includeThumbnail = null)
   //{
   //    ThumbnailResolution expectedRes = includeThumbnail?.ToThumbnailResolution() ?? ThumbnailResolution.Default;
   //    var dtoQuery = from video in videos
   //                   from thumb in video.Thumbnails
   //                   where thumb.Resolution == expectedRes
   //                   select new VideoDTO(
   //            video.Id,
   //            video.Location,
   //            video.Name,
   //            video.Description,
   //            -1, //(int?)(request.IncludeCounts ? video.PlaylistVideos.Count : null),
   //            -1,//(int?)(request.IncludeCounts ? video.Artifacts.Count : null),
   //            (int?)(request.IncludeCounts ? video.Thumbnails.Count : null),
   //            -1,//(int?)(request.IncludeCounts ? video.Transcripts.Count : null),
   //            (int?)(request.IncludeCounts ? video.Tags.Count : null),
   //            thumb.Location ?? ""
   //        ); ;
   //
   //    var result = new PageResult<VideoDTO>(dtoQuery, spec.Page, spec.PageSize, page.TotalCount);
   //    return result;
   //}
}