using Company.Videomatic.Domain.Aggregates.Playlist;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public sealed class UpdatePlaylistHandler : BaseRequestHandler<UpdatePlaylistCommand, UpdatePlaylistResponse>
{
    public UpdatePlaylistHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<UpdatePlaylistResponse> Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        var newValue = Mapper.Map<UpdatePlaylistCommand, Playlist>(request);

        var playlistDb = await DbContext.Playlists
            .SingleAsync(x => x.Id == request.Id, cancellationToken);

        Mapper.Map(request, playlistDb);

        var cnt = await DbContext.CommitChangesAsync(cancellationToken);

        return new UpdatePlaylistResponse(request.Id);
    }
}
