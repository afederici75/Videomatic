namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class UpdatePlaylistHandler : UpdateAggregateRootHandler<UpdatePlaylistCommand, Playlist>
{
    public UpdatePlaylistHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }
}
