namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class DeletePlaylistHandler : DeleteAggregateRootHandler<DeletePlaylistCommand, Playlist, PlaylistId>
{
    public DeletePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }
    
    protected override PlaylistId ConvertIdOfRequest(DeletePlaylistCommand request) => request.Id;
}
