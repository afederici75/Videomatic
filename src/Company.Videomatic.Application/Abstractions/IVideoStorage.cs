using Company.Videomatic.Application.Features.Videos.Queries.GetVideos;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Company.Videomatic.Application.Abstractions;

public interface IVideoStorage
{
    Task<int> UpdateVideoAsync(Video video);
    Task<bool> DeleteVideoAsync(int id);
    Task<Video?> GetVideoByIdAsync(int id);    
}