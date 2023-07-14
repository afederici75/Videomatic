namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public sealed class UpdatePlaylistHandler : UpdateEntityHandlerBase<UpdatePlaylistCommand, UpdatePlaylistResponse, Playlist, PlaylistId>
{
    public UpdatePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override UpdatePlaylistResponse CreateResponseFor(PlaylistId updatedEntityId, bool wasUpdated)
        => new(updatedEntityId, wasUpdated);

    protected override PlaylistId GetIdOfRequest(UpdatePlaylistCommand request) 
        => request.Id;
}
