using SharedKernel.CQRS.Commands;

namespace Application.Handlers.Playlists.Commands;

public sealed class DeletePlaylistHandler : DeleteEntityHandler<DeletePlaylistCommand, Playlist, PlaylistId>
{
    public DeletePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
