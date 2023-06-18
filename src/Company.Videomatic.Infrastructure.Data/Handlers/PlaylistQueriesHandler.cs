using Company.Videomatic.Application.Features.Playlists;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Company.Videomatic.Infrastructure.Data.Handlers;

public class PlaylistQueriesHandler : 
    IRequestHandler<GetPlaylistsQuery, GetPlaylistsResponse>,
    IRequestHandler<GetPlaylistByIdQuery, GetPlaylistByIdResponse>
{
    private readonly VideomaticDbContext _dbContext;
    private readonly IMapper _mapper;

    public PlaylistQueriesHandler(VideomaticDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetPlaylistsResponse> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken = default)
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
            .Select(p => _mapper.Map<PlaylistDb, PlaylistDTO>(p))
            .ToListAsync();
        
        var response = new GetPlaylistsResponse(playlists);
        return response;
    }

    public async Task<GetPlaylistByIdResponse> Handle(GetPlaylistByIdQuery request, CancellationToken cancellationToken = default)
    {
        var qry = new GetPlaylistsQuery(new long[] { request.Id }, request.Includes, Take: 1, Skip: null);
        var resp = await Handle(qry, cancellationToken);

        return new GetPlaylistByIdResponse(resp.playlists.SingleOrDefault());        
    }    
}