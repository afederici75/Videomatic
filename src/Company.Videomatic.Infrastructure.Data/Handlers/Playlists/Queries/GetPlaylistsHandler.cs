namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : BaseRequestHandler<GetPlaylistsQuery, GetPlaylistsResponse>
{
    public GetPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<GetPlaylistsResponse> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<Playlist> source = DbContext.Playlists.AsNoTracking();

        //request.OrderBy
        //request.Filter        

        var playlists = await source
            .Select(p => Mapper.Map<Playlist, PlaylistDTO>(p))
            .Skip(request.Skip ?? 0)
            .Take(request.Take ?? 50)
            .ToListAsync();

        var response = new GetPlaylistsResponse(playlists);
        return response;
    }
}
