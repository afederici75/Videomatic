namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class CreatePlaylistHandler : CreateAggregateRootHandler<CreatePlaylistCommand, Playlist>
{
    public CreatePlaylistHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }
}
