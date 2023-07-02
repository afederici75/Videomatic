using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain.Abstractions;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Application.Services;

public class VideoService : IVideoService
{
    private readonly IRepository<Video> _repository;

    public VideoService(IRepository<Video> repository)
    {
        _repository = repository;
    }

    public async Task<int> LinkToPlaylists(VideoId videoId, params PlaylistId[] playlistIds)
    {
        var video = await _repository.GetByIdAsync(videoId);
        if (video == null)
            return 0;

        // get duplicated ids
        foreach (var id in playlistIds)
        { 
            video.AddPlaylist(id);
        }
        
        var cnt = await _repository.SaveChangesAsync();
        return cnt;
    }
}