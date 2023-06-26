using System.Linq.Dynamic.Core;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : BaseRequestHandler<GetPlaylistsQuery, PageResult<PlaylistDTO>>
{
    public GetPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<PageResult<PlaylistDTO>> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken = default)
    {
        var query = from pl in DbContext.Playlists
                    select new
                    {
                        pl.Id,
                        pl.Name,
                        pl.Description,
                        VideoCount = pl.PlaylistVideos.Count
                    };

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            query = query.Where(request.Filter);
        }
        
        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            query = query.OrderBy(request.OrderBy);
        }

        var test = await query.ToListAsync();

        IQueryable<PlaylistDTO> queriable = query.Select(row => new PlaylistDTO(
              row.Id,
              row.Name,
              row.Description,
              row.VideoCount));

        var page = await queriable
            .ToPageAsync(request.Page ?? 1, request.PageSize ?? 10, cancellationToken);
            
        return page;
    }
}
