namespace Application.Handlers.Playlists.Commands;

public sealed class CreatePlaylistHandler : CreateEntityHandler<CreatePlaylistCommand, Playlist>
{
    public CreatePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
