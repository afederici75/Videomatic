namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class DeletePlaylistHandler : DeleteAggregateRootHandler<DeletePlaylistCommand, Playlist, PlaylistId>
{
    public DeletePlaylistHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }
    
    protected override PlaylistId ConvertIdOfRequest(DeletePlaylistCommand request) => request.Id;
}
