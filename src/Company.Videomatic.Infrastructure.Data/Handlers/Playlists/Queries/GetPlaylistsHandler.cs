namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Queries;

public sealed class GetPlaylistsHandler : 
    IRequestHandler<GetPlaylistsQuery, PageResult<PlaylistDTO>>,
    IRequestHandler<GetPlaylistsByIdQuery, IEnumerable<PlaylistDTO>>
{
    public GetPlaylistsHandler(IReadRepository<Playlist> repository, IMapper mapper, VideomaticDbContext dbContext) 
    {
        _repository = repository;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    readonly IReadRepository<Playlist> _repository;
    readonly IMapper _mapper;
    readonly VideomaticDbContext _dbContext;

    public async Task<PageResult<PlaylistDTO>> Handle(GetPlaylistsQuery request, CancellationToken cancellationToken)
    {
        var spec = new PlaylistsFilteredAndPaginated(
            request.SearchText,
            request.Page,
            request.PageSize,
            request.OrderBy);

        var res = await _repository.PageAsync<Playlist, PlaylistDTO>(
            spec,
            vid => _mapper.Map<PlaylistDTO>(vid),
            spec.Page,
            spec.PageSize,
            cancellationToken);

        if (request.IncludeCounts)
        {
            var ids = res!.Items.Select(x => x.Id);

            var res2 = from vid in _dbContext.Videos
                       from plv in vid.Playlists
                       where ids.Contains(plv.PlaylistId)
                       group plv by plv.PlaylistId into g
                       select new 
                       {  
                            PlaylistId = g.Key,
                            VideoCount = g.Count()
                       };
            
            //res.TotalCount = await _repository.CountAsync(spec, cancellationToken);
        }

        return res;
    }

    public async Task<IEnumerable<PlaylistDTO>> Handle(GetPlaylistsByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new PlaylistsByIdSpecification(request.PlaylistIds.Select(x => new PlaylistId(x)));

        var videos = await _repository.ListAsync(spec, cancellationToken);

        return videos.Select(vid => _mapper.Map<PlaylistDTO>(vid));

    }
}