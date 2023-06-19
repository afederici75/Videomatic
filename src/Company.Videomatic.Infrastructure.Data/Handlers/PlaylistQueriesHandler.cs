using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Application.Features.Playlists.Queries;
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
        IQueryable<Playlist> source = _dbContext.Playlists.AsNoTracking();

        //request.OrderBy
        //request.Filter        

        var playlists = await source
            .Select(p => _mapper.Map<Playlist, PlaylistDTO>(p))
            .Skip(request.Skip ?? 0)
            .Take(request.Take ?? 50)
            .ToListAsync();
        
        var response = new GetPlaylistsResponse(playlists);
        return response;
    }

    public async Task<GetPlaylistByIdResponse> Handle(GetPlaylistByIdQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<Playlist> source = _dbContext.Playlists.AsNoTracking();
        source = source.Where(source => request.Ids.Contains(source.Id));

        var playlists = await source
            .Select(p => _mapper.Map<Playlist, PlaylistDTO>(p))
            .ToListAsync(); 
        
        return new GetPlaylistByIdResponse(Items: playlists);
    }    
}