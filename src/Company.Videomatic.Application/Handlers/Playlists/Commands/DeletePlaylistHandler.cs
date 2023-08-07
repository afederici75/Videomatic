using Company.SharedKernel.Common.CQRS;

namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class DeletePlaylistHandler : DeleteEntityHandler<DeletePlaylistCommand, Playlist, PlaylistId>
{
    public DeletePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
