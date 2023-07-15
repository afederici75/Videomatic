using System.Linq;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : IRequestHandler<GetPlaylistsQuery, PageResult<PlaylistDTO>>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Playlist, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Playlist, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Playlist.Id), _ => _.Id },
        { nameof(Playlist.Name), _ => _.Name },
        { nameof(Playlist.Description), _ => _.Description },
        { "VideoCount", _ => _.Videos.Count()},
    };

    public GetPlaylistsHandler(VideomaticDbContext dbContext)
    {
        _dbContext = dbContext;        
    }

    readonly VideomaticDbContext _dbContext;

    public async Task<PageResult<PlaylistDTO>> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken)
    {
        var pageIdx = request.Page ?? 1;
        var pageSize = request.PageSize ?? 10;

        // Playlists
        IQueryable<Playlist> q = _dbContext.Playlists;

        // Where
        if (request.PlaylistIds != null)
        {
            q = q.Where(p => request.PlaylistIds.Contains(p.Id));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            q = q.Where(p => p.Name.Contains(request.SearchText) || ((p.Description != null) && p.Description.Contains(request.SearchText)));
        }

        // OrderBy
        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            q = q.OrderBy(request.OrderBy, SupportedOrderBys);
        }        

        // Projection
        var final = q.Select(p => new PlaylistDTO(
            p.Id,
            p.Name,
            p.Description,
            p.Videos.Count()));

        // Counts
        var totalCount = await final.CountAsync();
        var res = await final.ToListAsync();

        return new PageResult<PlaylistDTO>(res, pageIdx, pageSize, totalCount);
    }
}