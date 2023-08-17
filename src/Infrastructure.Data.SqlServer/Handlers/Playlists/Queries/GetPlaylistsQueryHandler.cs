
using LinqKit;
using System.Linq.Expressions;

namespace Infrastructure.Data.SqlServer.Handlers.Playlists.Queries;

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
            Expression<Func<Playlist, bool>> fullTextPredicate = PredicateBuilder.New<Playlist>(false);
            fullTextPredicate = fullTextPredicate.Or(x => EF.Functions.Contains(x.Name, $"\"{request.SearchText}\""));
            fullTextPredicate = fullTextPredicate.Or(x => EF.Functions.Contains(x.Description!, $"\"{request.SearchText}\""));
            fullTextPredicate = fullTextPredicate.Or(x => EF.Functions.Contains(x.Tags!, $"\"{request.SearchText}\""));

            // TODO: create a replacement of EF.Functions.FreeText so I can use this
            // in the .Data assembly and pass multiple columns. I don't have time now, but this shows how:
            // https://www.thinktecture.com/en/entity-framework-core/custom-functions-using-imethodcalltranslator-in-2-1/
            // https://www.thinktecture.com/entity-framework-core/custom-functions-using-hasdbfunction-in-2-1/
            q = q.Where(fullTextPredicate);            
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
            p.Thumbnail ?? p.Origin.Thumbnail,
            p.Picture ?? p.Origin.Picture,
            p.Description,
            p.Videos.Count()));

        // Counts
        var res = await final.AsNoTracking().ToListAsync(cancellationToken);

        return new Page<PlaylistDTO>(res, skip, take, totalCount);
    }
}