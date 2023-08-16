namespace Application.Abstractions;

public static class IRepositoryOfPlaylistExtensions
{
    public static async Task<Result<int>> LinkPlaylistToVideos(this IRepository<Playlist> repository,
        PlaylistId playlistId,
        IEnumerable<VideoId> videoIds,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(playlistId, nameof(playlistId));

            Playlist? pl = await repository.GetByIdAsync(playlistId, cancellationToken);
            if (pl is null)
            {
                return Result<int>.NotFound();
            }

            int newLinksCount = pl.LinkToVideos(videoIds);

            await repository.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(newLinksCount);
        }
        catch (Exception ex)
        {
            return Result<int>.Error(ex.Message);
        }
    }
}