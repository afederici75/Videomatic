namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class UpdatePlaylistHandler : UpdateAggregateRootHandler<UpdatePlaylistCommand, Playlist, PlaylistId>
{
    public UpdatePlaylistHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }

    protected override PlaylistId ConvertIdOfRequest(UpdatePlaylistCommand request) => new PlaylistId(request.Id);
}
