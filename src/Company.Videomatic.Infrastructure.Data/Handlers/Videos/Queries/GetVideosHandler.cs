using Company.Videomatic.Domain.Aggregates.Video;
using System.Linq.Expressions;
using System.Security.Cryptography;

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
        //{ nameof(VideoDTO.ArtifactCount), _ => _.Artifacts.Count },
        //{ nameof(VideoDTO.TranscriptCount), _ => _.Artifacts.Count },
        //{ nameof(VideoDTO.TagCount), _ => _.Artifacts.Count },
        //{ nameof(VideoDTO.ThumbnailCount), _ => _.Artifacts.Count },
        //{ nameof(VideoDTO.PlaylistCount), _ => _.PlaylistVideos.Count },
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
            throw new NotImplementedException();
            //var usedVideoIds = await (
            //    from pl in DbContext.Playlists
            //    where request.PlaylistIds.Contains(pl.Id)
            //    from v in pl.PlaylistVideos
            //    select v.VideoId.Value).ToArrayAsync(cancellationToken);
            //
            //query = query.Where(v => usedVideoIds.Contains(v.Id));
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
                (int?)(request.IncludeCounts ? video.VideoTags.Count : null),
                thumb.Location ?? ""
            );;
        
        var page = await dtoQuery
           .ToPageAsync(request.Page ?? 1, request.PageSize ?? 10, cancellationToken);
        
        return page;
    }
}