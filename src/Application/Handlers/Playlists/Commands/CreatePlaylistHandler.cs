using SharedKernel.CQRS.Commands;

namespace Application.Handlers.Playlists.Commands;

public sealed class CreatePlaylistHandler : CreateEntitytHandler<CreatePlaylistCommand, Playlist>
{
    public CreatePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
