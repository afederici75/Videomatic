using System.Linq;
using System.Linq.Expressions;

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : IRequestHandler<GetPlaylistsQuery, Page<PlaylistDTO>>
{
    //public static readonly IReadOnlyDictionary<string, Expression<Func<Playlist, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Playlist, object?>>>(StringComparer.OrdinalIgnoreCase)
    //{
    //    { nameof(Playlist.Id), _ => _.Id },
    //    { nameof(Playlist.Name), _ => _.Name },
    //    { nameof(Playlist.Description), _ => _.Description },
    //    { "VideoCount", _ => _.Videos.Count()},
    //};

    public GetPlaylistsHandler(IDbContextFactory<VideomaticDbContext> dbContextFactory)
    {
        DbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    public IDbContextFactory<VideomaticDbContext> DbContextFactory { get; }

    public async Task<Page<PlaylistDTO>> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = DbContextFactory.CreateDbContext();

        var skip = request.Skip ?? 0;
        var take = request.Take ?? 10;

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
        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            q = q.OrderBy(request.OrderBy);
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

        return new Page<PlaylistDTO>(res, skip, take, totalCount);
    }
}