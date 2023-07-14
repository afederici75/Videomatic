namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class DeletePlaylistHandler : DeleteAggregateRootHandler<DeletePlaylistCommand, Playlist>
{
    public DeletePlaylistHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }
}
