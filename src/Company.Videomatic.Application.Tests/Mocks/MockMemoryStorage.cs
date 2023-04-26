using Company.Videomatic.Application.Features.Videos.Queries.GetVideos;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Domain.Specifications;

namespace Company.Videomatic.Application.Tests.Mocks;

/// <summary>
/// This class implements the IVideoStorage interface and stores the videos in memory.
/// </summary>
internal class MockVideoStorage : IVideoRepository
{
    private readonly Dictionary<int, Video> _videos = new();
    public Task<int> UpdateVideoAsync(Video video)
    {
        if (video.Id <= 0)
        {
            video.SetId(_videos.Count + 1);
        }
        _videos[video.Id] = video;
        return Task.FromResult(video.Id);
    }


    public Task<bool> DeleteVideoAsync(int id)
    {
        return Task.FromResult(_videos.Remove(id));
    }

    public Task<Video?> GetVideoByIdAsync(int id)
    {        
        var rec = _videos.GetValueOrDefault(id);
        return Task.FromResult(rec);
    }

    public Task<Video> GetVideoByIdAsync(GetVideoSpecification spec)
    {
        Video video = spec.Evaluate(_videos.Values).First();
        return Task.FromResult(video);
    }

    public Task<IEnumerable<Video>> GetVideosAsync(GetVideosSpecification spec)
    {
        var res = spec.Evaluate(_videos.Values);
        return Task.FromResult(res);
    }
}
