using Company.Videomatic.Application.Features.Playlists.Commands;
using Company.Videomatic.Domain.Abstractions;
using Company.Videomatic.Domain.Specifications.Playlists;
using Company.Videomatic.Domain.Specifications.Videos;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public sealed class DeletePlaylistHandler : IRequestHandler<DeletePlaylistCommand, DeletePlaylistResponse>
{
    public DeletePlaylistHandler(IRepository<Playlist> repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IRepository<Playlist> Repository { get; }

    public async Task<DeletePlaylistResponse> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        var spec = new PlaylistsByIdSpecification(new PlaylistId[] { request.Id });

        var Playlist = await Repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (Playlist == null)
            return new DeletePlaylistResponse(request.Id, false);

        await Repository.DeleteAsync(Playlist);

        return new DeletePlaylistResponse(request.Id, true);
    }
}
