namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : 
    IRequestHandler<GetPlaylistsQuery, PageResult<PlaylistDTO>>,
    IRequestHandler<GetPlaylistsByIdQuery, IEnumerable<PlaylistDTO>>
{
    public GetPlaylistsHandler(VideomaticDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    readonly VideomaticDbContext _dbContext;
    readonly IMapper _mapper;
    
    public async Task<PageResult<PlaylistDTO>> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken)
    {        
        //var spec = new PlaylistsFilteredAndPaginated(
        //    request.SearchText,
        //    request.Page,
        //    request.PageSize,
        //    request.OrderBy);
        //
        //var res = await _repository.PageAsync<Playlist, PlaylistDTO>(
        //    spec,
        //    vid => _mapper.Map<PlaylistDTO>(vid),
        //    spec.Page,
        //    spec.PageSize,
        //    cancellationToken);
        //
        //return res;
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PlaylistDTO>> Handle(GetPlaylistsByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var spec = new PlaylistsByIdSpecification(request.PlaylistIds.Select(x => new PlaylistId(x)));
        //
        //var videos = await _repository.ListAsync(spec, cancellationToken);
        //
        //return videos.Select(vid => _mapper.Map<PlaylistDTO>(vid));

    }
}