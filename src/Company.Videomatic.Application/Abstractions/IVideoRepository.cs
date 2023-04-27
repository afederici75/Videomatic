using Ardalis.Specification;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Domain.Specifications;

namespace Company.Videomatic.Application.Abstractions;

public interface IVideoRepository
{
    Ardalis.Specification.IReadRepositoryBase<object> readRepositoryBase();

    Task<int> UpdateVideoAsync(Video video);
    Task<bool> DeleteVideoAsync(int id);
    Task<Video> GetVideoByIdAsync(GetVideoSpecification spec);
    Task<IEnumerable<Video>> GetVideosAsync(GetVideosSpecification spec);
}

public interface IRepository<T> : IRepositoryBase<T> 
    where T : class
{ 
}