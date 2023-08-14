using SharedKernel.CQRS.Commands;

namespace Application.Handlers.Playlists.Commands;

public sealed class UpdatePlaylistHandler : UpdateEntityHandler<UpdatePlaylistCommand, Playlist, PlaylistId>
{
    public UpdatePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
