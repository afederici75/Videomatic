using Ardalis.GuardClauses;
using Company.SharedKernel.Abstractions;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Video;
using System.Threading;

namespace Company.Videomatic.Application.Abstractions;

public static class RepositoryExtensions
{
    public static async Task<Result<int>> LinkPlaylistToVideos(this IRepository<Playlist> repository, PlaylistId playlistId, IEnumerable<VideoId> videoIds, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(repository, nameof(repository));
        Guard.Against.Null(playlistId, nameof(playlistId));

        var pl = await repository.GetByIdAsync(playlistId, cancellationToken);
        if (pl is null)
        { 
            return Result<int>.NotFound();
        }

        var newLinks = pl.LinkToVideos(videoIds);

        await repository.SaveChangesAsync();

        return newLinks;
    }
}
