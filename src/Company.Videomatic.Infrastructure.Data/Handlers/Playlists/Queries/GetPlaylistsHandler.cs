namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : BaseRequestHandler<GetPlaylistsQuery, PageResult<PlaylistDTO>>
{
    public GetPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<PageResult<PlaylistDTO>> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
        //var dtos = from pl in DbContext.Playlists
        //           select new PlaylistDTO(pl.Id, pl.Name, pl.Description, pl.PlaylistVideos.Count());
        //
        //var page = await dtos
        //    .ApplyFilters(request.Filter, new[] { nameof(PlaylistDTO.Name), nameof(PlaylistDTO.Description) })
        //    .ApplyOrderBy(request.OrderBy)
        //    .ToPageAsync(request.Paging, cancellationToken);            
        //
        //return page;
    }
}
