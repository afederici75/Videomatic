namespace Company.Videomatic.Application.Abstractions;

public interface IVideoStorage
{
    Task<int> UpdateVideoAsync(Video video);
    Task<bool> DeleteVideoAsync(int id);
    Task<Video?> GetVideoByIdAsync(int id, VideoQueryOptions? options = null);
}