namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public sealed class DeletePlaylistHandler : BaseRequestHandler<DeletePlaylistCommand, DeletePlaylistResponse>
{
    public DeletePlaylistHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<DeletePlaylistResponse> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        int cnt = await DbContext
            .Playlists
            .Where(x => x.Id == request.Id).
            ExecuteDeleteAsync(cancellationToken);

        return new DeletePlaylistResponse(request.Id, cnt > 0);
    }
}
