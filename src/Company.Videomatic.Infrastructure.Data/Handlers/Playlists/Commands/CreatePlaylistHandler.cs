namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public sealed class CreatePlaylistHandler : CreateEntityHandlerBase<CreatePlaylistCommand, CreatePlaylistResponse, Playlist>
{
    public CreatePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override CreatePlaylistResponse CreateResponseFor(Playlist entity)
        => new CreatePlaylistResponse(Id: entity.Id);
    
}
