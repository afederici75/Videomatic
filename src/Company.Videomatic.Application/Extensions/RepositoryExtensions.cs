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

        await repository.SaveChangesAsync(cancellationToken);

        return newLinks;
    }

    public static async Task<IReadOnlyDictionary<string, VideoId>> GetVideoProviderIds(this IRepository<Video> repository, IEnumerable<VideoId> videoIds, CancellationToken cancellationToken = default)
    {
        var videos = await repository.ListAsync(new VideosByIdsSpec(videoIds.ToArray()), cancellationToken);

        return videos.Where(v => v.Origin?.ProviderItemId != null)
                     .ToDictionary(v => v.Origin!.ProviderItemId , v => v.Id);
    }   
}

// TODO: move somewhere else
public class VideosByIdsSpec : Specification<Video>
{
    public VideosByIdsSpec(params VideoId[] ids)
    {
        Query.Where(v => ids.Contains(v.Id));
    }
}

// TODO: move somewhere else
public class PlaylistWithVideosSpec : Specification<Playlist>
{
    public PlaylistWithVideosSpec(params PlaylistId[] playlistIds)
    {
        Query.Where(pl => playlistIds.Contains(pl.Id))
             .Include(pl => pl.Videos);
    }    
}