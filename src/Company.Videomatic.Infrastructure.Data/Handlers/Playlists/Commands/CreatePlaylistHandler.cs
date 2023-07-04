namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public sealed class CreatePlaylistHandler : IRequestHandler<CreatePlaylistCommand, CreatePlaylistResponse>
{
    private readonly IRepository<Playlist> _repository;
    private readonly IMapper _mapper;

    public CreatePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) 
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CreatePlaylistResponse> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken = default)
    {        
        Playlist newPlaylist = _mapper.Map<CreatePlaylistCommand, Playlist>(request);

        var entry = await _repository.AddAsync(newPlaylist);
        
        return new CreatePlaylistResponse(Id: entry.Id);
    }
}
