using Company.Videomatic.Application.Features.Playlists;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Company.Videomatic.Infrastructure.Data.Handlers;

public class PlaylistQueriesHandler : 
    IRequestHandler<GetPlaylistsQuery, IReadOnlyCollection<Playlist>>,
    IRequestHandler<GetPlaylistByIdQuery, Playlist?>
{
    private readonly VideomaticDbContext _dbContext;
    private readonly IMapper _mapper;

    public PlaylistQueriesHandler(VideomaticDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<Playlist>> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<PlaylistDb> source = _dbContext.Playlists.AsNoTracking();

        if (request.Includes != null)
        {
            foreach (var include in request.Includes)
            {
                source = source.Include(include);
            }
        }

        if (request.Ids?.Length > 0)
        {
            source = source.Where(p => request.Ids.Contains(p.Id));
        }

        var playlists = await source
            .Select(p => _mapper.Map<PlaylistDb, Playlist>(p))
            .ToListAsync();

        return playlists;
    }

    public Task<Playlist?> Handle(GetPlaylistByIdQuery request, CancellationToken cancellationToken = default)
    {
        var qry = new GetPlaylistsQuery(new long[] { request.Id }, request.Includes, Take: 1, Skip: null); 
        
        return Handle(qry, cancellationToken)
            .ContinueWith(t => t.Result.FirstOrDefault(), cancellationToken);
    }    
}