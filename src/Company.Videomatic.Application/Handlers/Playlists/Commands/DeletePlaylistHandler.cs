namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public sealed class DeletePlaylistHandler : DeleteEntityHandlerBase<DeletePlaylistCommand, DeletePlaylistResponse, Playlist, PlaylistId>
{
    public DeletePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override DeletePlaylistResponse CreateResponseFor(PlaylistId entityId, bool wasDeleted) => new(entityId, wasDeleted);

    protected override PlaylistId GetIdOfRequest(DeletePlaylistCommand request) => new(request.Id);
}
