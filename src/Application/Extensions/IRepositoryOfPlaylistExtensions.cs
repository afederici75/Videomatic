using Application.Specifications;

namespace Application.Abstractions;

public static class IRepositoryOfPlaylistExtensions
{
    public static async Task<Result<int>> LinkPlaylistToVideos(this IMyRepository<Playlist> repository,
        PlaylistId playlistId,
        IEnumerable<VideoId> videoIds,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(repository, nameof(repository));
        Guard.Against.Null(playlistId, nameof(playlistId));

        try
        {
            Playlist? pl = await repository.SingleOrDefaultAsync(
                new Playlists.ById(playlistId, Playlists.Include.Videos), 
                cancellationToken);

            if (pl is null)
            {
                return Result<int>.NotFound();
            }

            int newLinksCount = pl.LinkToVideos(videoIds);

            await repository.SaveChangesAsync( cancellationToken);

            return Result<int>.Success(newLinksCount);
        }
        catch (Exception ex)
        {
            return Result<int>.Error(ex.Message);
        }
    }
}