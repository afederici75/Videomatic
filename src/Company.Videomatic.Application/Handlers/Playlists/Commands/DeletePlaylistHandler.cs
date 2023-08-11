using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class DeletePlaylistHandler : DeleteEntityHandler<DeletePlaylistCommand, Playlist, PlaylistId>
{
    public DeletePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
