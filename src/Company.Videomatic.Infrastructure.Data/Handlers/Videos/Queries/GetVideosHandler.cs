using Company.Videomatic.Application.Features.Model;
using Company.Videomatic.Application.Query;
using Company.Videomatic.Infrastructure.Data.Extensions;
using System.Linq.Dynamic.Core;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Queries;

public class GetVideosHandler : BaseRequestHandler<GetVideosQuery, GetVideosResponse>
    {

    static GetVideosHandler()
    {
        
    }

    public GetVideosHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {        

    }
      
    public override async Task<GetVideosResponse> Handle(GetVideosQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<PlaylistVideo> query = DbContext.PlaylistVideos;                
        
        var projection = from pv in query
            select new 
            {
                Id = pv.Video.Id, 
                Title = pv.Video.Title, 
                Description = pv.Video.Description, 
                Location = pv.Video.Location,
                PlaylistCount = (int?)(request.IncludeCounts ? pv.Video.Playlists.Count : null),
                ArtifactCount = (int?)(request.IncludeCounts ? pv.Video.Artifacts.Count : null),
                ThumbnailCount = (int?)(request.IncludeCounts ? pv.Video.Thumbnails.Count : null),
                TranscriptCount = (int?)(request.IncludeCounts ? pv.Video.Transcripts.Count : null),
                TagCount = (int?)(request.IncludeCounts ? pv.Video.VideoTags.Count : null),
                Thumbnail = (request.IncludeThumbnail != null) ? pv.Video.Thumbnails.FirstOrDefault(t => t.Resolution==request.IncludeThumbnail) : null
            };

        projection = projection.ApplyOrderBy(request.OrderBy);



        IQueryable<VideoDTO> dtosQuery = projection
            .Select(v => new VideoDTO(
                v.Id,
                v.Location,
                v.Title,
                v.Description,
                v.PlaylistCount,
                v.ArtifactCount,
                v.ThumbnailCount,
                v.TranscriptCount,
                v.TagCount,
                Mapper.Map<Thumbnail, ThumbnailDTO>(v.Thumbnail)
                ));

        throw new Exception();

        //IPagedResults<VideoDTO> items = await PagedList<VideoDTO>.CreateAsync(dtosQuery, request.Page ?? 1, request.PageSize ?? 10);

        //return new GetVideosResponse(Page: items);
    }
}