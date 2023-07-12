using Company.Videomatic.Domain.Specifications.Videos;

namespace Company.Videomatic.Application.Services;

public class VideoService : IVideoService
{
    private readonly IRepository<Video> _repository;

    public VideoService(IRepository<Video> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> LinkToPlaylists(VideoId videoId, PlaylistId[] playlistIds, CancellationToken cancellationToken = default)
    {
        var spec = new VideoWithPlaylistsSpecification(videoId);

        var video = await _repository.SingleOrDefaultAsync(spec, cancellationToken); 
        if (video == null)
            return 0;

        video.LinkToPlaylists(playlistIds);
        
        var cnt = await _repository.SaveChangesAsync();
        return cnt;
    }
}