using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class UpdatePlaylistHandler : UpdateEntityHandler<UpdatePlaylistCommand, Playlist, PlaylistId>
{
    public UpdatePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
