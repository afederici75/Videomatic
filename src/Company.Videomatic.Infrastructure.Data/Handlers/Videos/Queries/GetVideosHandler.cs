using System.Linq.Dynamic.Core;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Queries;

public class GetVideosHandler : BaseRequestHandler<GetVideosQuery, PageResult<VideoDTO>>
{
    public GetVideosHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {        

    }
      
    public override async Task<PageResult<VideoDTO>> Handle(GetVideosQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<PlaylistVideo> query = DbContext.PlaylistVideos;

        // Applies the custom filter options
        if (request.Filter?.PlaylistIds is not null)
        {
            query = query.Where(pv => request.Filter.PlaylistIds.Contains(pv.PlaylistId));
        }

        // Creates the projection
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

        // Fetches the page
        var page = await projection
            .ApplyFilters(request.Filter, new [] { nameof(VideoDTO.Title), nameof(VideoDTO.Description) })
            .ApplyOrderBy(request.OrderBy)
            .ToPageAsync(request.Paging, 
                v => new VideoDTO(
                v.Id,
                v.Location,
                v.Title,
                v.Description,
                v.PlaylistCount,
                v.ArtifactCount,
                v.ThumbnailCount,
                v.TranscriptCount,
                v.TagCount,
                Mapper.Map<Thumbnail, ThumbnailDTO>(v.Thumbnail)), 
                cancellationToken);

        return page;
    }
}