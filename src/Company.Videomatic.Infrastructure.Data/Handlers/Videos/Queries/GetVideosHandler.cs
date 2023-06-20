using Company.Videomatic.Application.Features.Model;
using System.Linq;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Queries;

public class GetVideosHandler : BaseRequestHandler<GetVideosQuery, GetVideosResponse>
{
    public GetVideosHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
        SortOptions = new ();
        SortOptions.Add(nameof(Video.Description), v => v.Description!);
        SortOptions.Add(nameof(Video.Title), v => v.Title);

    }

    Dictionary<string, Expression<Func<Video, object>>> SortOptions;

    IQueryable<T> ApplyFilter<T>(IQueryOptions filter, IQueryable<T> queriable)
        where T : IEntity
    {
        if (filter.Ids is not null)
        {
            queriable = queriable.Where(e => filter.Ids.Contains(e.Id));
        }        

        return queriable;
    }

    public override async Task<GetVideosResponse> Handle(GetVideosQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<PlaylistVideo> playlistVideosQuery = DbContext.PlaylistVideos;
        
        if (request.Ids is not null)
        {
            playlistVideosQuery = playlistVideosQuery.Where(e => request.Ids.Contains(e.VideoId));
        }

        if (request.SearchText is not null)
        {
            playlistVideosQuery = playlistVideosQuery.Where(pv =>             
                pv.Video.Title.Contains(request.SearchText)
                ||
                ((pv.Video.Description != null) && (pv.Video.Description!.Contains(request.SearchText)))
            );
        }

        if (request.PlaylistId is not null)
        {
            playlistVideosQuery = playlistVideosQuery.Where(pv=> pv.PlaylistId==request.PlaylistId);
        }

        var projection = from pv in playlistVideosQuery
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

        IPagedList<VideoDTO> items = await PagedList<VideoDTO>.CreateAsync(dtosQuery, request.Page ?? 1, request.PageSize ?? 10);

        return new GetVideosResponse(Page: items);
    }
}