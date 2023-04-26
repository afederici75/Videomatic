using Ardalis.Specification;
using Company.Videomatic.Application.Features.Videos.Queries.GetVideos;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Domain.Specifications;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Company.Videomatic.Application.Abstractions;

public interface IVideoRepository
{
    Task<int> UpdateVideoAsync(Video video);
    Task<bool> DeleteVideoAsync(int id);
    Task<Video> GetVideoByIdAsync(GetVideoByIdSpec spec);
    Task<IEnumerable<Video>> GetVideosAsync(GetVideosSpec spec);
}