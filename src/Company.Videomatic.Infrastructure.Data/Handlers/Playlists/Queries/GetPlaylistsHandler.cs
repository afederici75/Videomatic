using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : BaseRequestHandler<GetPlaylistsQuery, GetPlaylistsResponse>
{
    public GetPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<GetPlaylistsResponse> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken = default)
    {
        var query = from pl in DbContext.Playlists.AsNoTracking()
                    select new PlaylistDTO(pl.Id, pl.Name, pl.Description, pl.PlaylistVideos.Count());

        //request.OrderBy
        //request.Filter

        var playlists = await query
            .Skip(request.Skip ?? 0)
            .Take(request.Take ?? 50)
            .ToListAsync();

        var response = new GetPlaylistsResponse(playlists);
        return response;
    }
}
