using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public class GetPlaylistByIdHandler : BaseRequestHandler<GetPlaylistByIdQuery, GetPlaylistByIdResponse>
{
    public GetPlaylistByIdHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
    
    public override async Task<GetPlaylistByIdResponse> Handle(GetPlaylistByIdQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<Playlist> source = DbContext.Playlists.AsNoTracking();
        source = source.Where(source => request.Ids.Contains(source.Id));

        var playlists = await source
            .Select(p => Mapper.Map<Playlist, PlaylistDTO>(p))
            .ToListAsync();

        return new GetPlaylistByIdResponse(Items: playlists);
    }
}