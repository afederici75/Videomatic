using Company.Videomatic.Domain.Videos;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Queries;

public class GetVideosHandler : BaseRequestHandler<GetVideosQuery, PageResult<VideoDTO>>
{
    public GetVideosHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {        

    }

    Dictionary<string, Expression<Func<Video, object?>>> SupportedOrderBys = new(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(VideoDTO.Id), _ => _.Id },
        { nameof(VideoDTO.Title), _ => _.Name },
        { nameof(VideoDTO.Description), _ => _.Description },
        { nameof(VideoDTO.ArtifactCount), _ => _.Artifacts.Count },
        { nameof(VideoDTO.TranscriptCount), _ => _.Artifacts.Count },
        { nameof(VideoDTO.TagCount), _ => _.Artifacts.Count },
        { nameof(VideoDTO.ThumbnailCount), _ => _.Artifacts.Count },
        { nameof(VideoDTO.PlaylistCount), _ => _.Playlists.Count },
    };

    public override async Task<PageResult<VideoDTO>> Handle(GetVideosQuery request, CancellationToken cancellationToken = default)
    {
        // Creates the projection
        IQueryable<Video> query = DbContext.Videos;

        // Query setup        
        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            // Search text fields must be specified directly
            query = query.Where(v =>
                v.Name.Contains(request.SearchText) ||
                v.Description!.Contains(request.SearchText));
        }

        if (request.PlaylistIds != null)
        {
            var videosOfPlaylist = DbContext.PlaylistVideos
                .Where(pv => request.PlaylistIds.Contains(pv.PlaylistId))
                .Select(pv => pv.VideoId);

            query = query.Where(v => videosOfPlaylist.Contains(v.Id));
        }

        if (request.VideoIds != null)
        {            
            query = query.Where(v => request.VideoIds.Contains(v.Id));
        }

        // Custom OrderBy which takes in account what we allow to sort by
        query = query.OrderBy(request.OrderBy, SupportedOrderBys);

        // Mapping                
        ThumbnailResolution expectedRes = request.IncludeThumbnail?.ToThumbnailResolution() ?? ThumbnailResolution.Default;

        var dtoQuery = from video in query
                       join thumb in DbContext.Thumbnails 
                       on video.Id equals thumb.VideoId
                       into thumbGroup 
                       from resThumb in thumbGroup.DefaultIfEmpty()
                       where resThumb.Resolution == expectedRes
                       select new VideoDTO(
                video.Id,
                video.Location,
                video.Name,
                video.Description,
                (int?)(request.IncludeCounts ? video.Playlists.Count : null),
                (int?)(request.IncludeCounts ? video.Artifacts.Count : null),
                (int?)(request.IncludeCounts ? video.Thumbnails.Count : null),
                (int?)(request.IncludeCounts ? video.Transcripts.Count : null),
                (int?)(request.IncludeCounts ? video.VideoTags.Count : null),
                resThumb.Location ?? "Unknown"
            );

        var page = await dtoQuery
           .ToPageAsync(request.Page ?? 1, request.PageSize ?? 10, cancellationToken);

        return page;
    }
}