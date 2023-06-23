using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Queries;

public class GetVideosHandler : BaseRequestHandler<GetVideosQuery, PageResult<VideoDTO>>
{
    public GetVideosHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {        

    }
      
    public override async Task<PageResult<VideoDTO>> Handle(GetVideosQuery request, CancellationToken cancellationToken = default)
    {
        // Creates the projection
        var query = from pv in DbContext.PlaylistVideos
                         select new 
            {
                Id = pv.Video.Id, 
                Title = pv.Video.Title, 
                Description = pv.Video.Description, 
                Location = pv.Video.Location,
                PlaylistId = pv.PlaylistId,
                PlaylistCount = (int?)(request.IncludeCounts ? pv.Video.Playlists.Count : null),
                ArtifactCount = (int?)(request.IncludeCounts ? pv.Video.Artifacts.Count : null),
                ThumbnailCount = (int?)(request.IncludeCounts ? pv.Video.Thumbnails.Count : null),
                TranscriptCount = (int?)(request.IncludeCounts ? pv.Video.Transcripts.Count : null),
                TagCount = (int?)(request.IncludeCounts ? pv.Video.VideoTags.Count : null),
                Thumbnail = (request.IncludeThumbnail != null) ? pv.Video.Thumbnails.FirstOrDefault(t => t.Resolution==request.IncludeThumbnail) : null
            };

        // Applies the custom filter options
        if (request.PlaylistIds?.Length > 0)
        {
            query = query.Where(pv => request.PlaylistIds.Contains(pv.PlaylistId));
        }

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            query = query.Where(request.Filter);
        }

        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            query = query.OrderBy(request.OrderBy);
        }

        // Fetches the page
        var queriable = query
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
                Mapper.Map<Thumbnail, ThumbnailDTO>(v.Thumbnail)));


        var page = await queriable
           .ToPageAsync(request.Page ?? 1, request.PageSize ?? 10, cancellationToken);

        return page;
    }
}