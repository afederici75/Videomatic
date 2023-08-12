using System.Linq;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Handlers.Playlists.Queries;

public sealed class GetPlaylistsQueryHandler : IRequestHandler<GetPlaylistsQuery, Page<PlaylistDTO>>
{    
    public GetPlaylistsQueryHandler(IDbContextFactory<VideomaticDbContext> dbContextFactory)
    {
        DbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    public IDbContextFactory<VideomaticDbContext> DbContextFactory { get; }

    public async Task<Page<PlaylistDTO>> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = DbContextFactory.CreateDbContext();
        
        // Playlists
        IQueryable<Playlist> q = dbContext.Playlists;

        
        // Where
        if (request.PlaylistIds != null)
        {
            q = q.Where(p => request.PlaylistIds.Contains(p.Id));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            q = q.Where(p => EF.Functions.FreeText(p.Name, request.SearchText) ||
                ((p.Description != null) && EF.Functions.FreeText(p.Description, request.SearchText)));
        }

        // OrderBy
        q = !string.IsNullOrWhiteSpace(request.OrderBy) ? q.OrderBy(request.OrderBy) : q;

        var totalCount = await q.CountAsync(cancellationToken);

        // Pagination
        var skip = request.Skip ?? 0;
        var take = request.Take ?? 10;

        q = q.Skip(skip).Take(take);

        // Projection
        var final = q.Select(p => new PlaylistDTO(
            p.Id,
            p.Name,
            p.Thumbnail,
            p.Picture,
            p.Description,
            p.Videos.Count()));

        // Counts
        var res = await final.AsNoTracking().ToListAsync(cancellationToken);

        return new Page<PlaylistDTO>(res, skip, take, totalCount);
    }
}