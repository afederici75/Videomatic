using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Company.Videomatic.Application.Abstractions;

public interface IVideoStorage
{
    Task<int> UpdateVideoAsync(Video video);
    Task<bool> DeleteVideoAsync(int id);
    Task<Video?> GetVideoByIdAsync(int id, VideoQueryOptions? options = null);
    Task<Video[]> GetVideosAsync(
        Func<IQueryable<Video>, IQueryable<Video>>? filter = default,
        Func<IQueryable<Video>, IQueryable<Video>>? sort = default,
        Func<IQueryable<Video>, IQueryable<Video>>? paging = default,
        CancellationToken cancellationToken = default);
}