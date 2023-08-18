using Application.Specifications;

namespace Application.Abstractions;

public static class IRepositoryOfVideoExtensions
{
    public static async Task<IReadOnlyDictionary<string, VideoId>> GetVideoProviderIds(this IRepository<Video> repository,
        IEnumerable<VideoId> videoIds,
        CancellationToken cancellationToken = default)
    {        
        List<Video> videos = await repository.ListAsync(new Videos.ByIds(videoIds.ToArray()), cancellationToken);

        return videos.Where(v => v.Origin?.ProviderItemId != null)
                     .ToDictionary(v => v.Origin!.ProviderItemId , v => v.Id);
    }
}
