namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public sealed class UpdatePlaylistHandler : UpdateAggregateRootHandler<UpdatePlaylistCommand, Playlist, PlaylistId>
{
    public UpdatePlaylistHandler(IRepository<Playlist> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override PlaylistId ConvertIdOfRequest(UpdatePlaylistCommand request) => new PlaylistId(request.Id);
}
