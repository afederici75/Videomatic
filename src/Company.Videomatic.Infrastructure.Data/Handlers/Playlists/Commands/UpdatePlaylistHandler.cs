using Company.Videomatic.Domain.Abstractions;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Specifications.Playlists;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public sealed class UpdatePlaylistHandler : IRequestHandler<UpdatePlaylistCommand, UpdatePlaylistResponse>
{
    private readonly IRepository<Playlist> _repository;
    private readonly IMapper _mapper;

    public UpdatePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdatePlaylistResponse> Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        var spec = new PlaylistsByIdSpecification(new PlaylistId(request.Id));
        Playlist? Playlist = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (Playlist == null)
        {
            return new UpdatePlaylistResponse(request.Id);
        }

        _mapper.Map<UpdatePlaylistCommand, Playlist>(request, Playlist);
        var cnt = await _repository.SaveChangesAsync();

        return new UpdatePlaylistResponse(request.Id);
    }
}
