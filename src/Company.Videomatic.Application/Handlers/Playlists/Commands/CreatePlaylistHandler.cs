using Company.SharedKernel.Common.CQRS;

namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class CreatePlaylistHandler : CreateEntitytHandler<CreatePlaylistCommand, Playlist>
{
    public CreatePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
