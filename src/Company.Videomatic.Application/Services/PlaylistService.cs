namespace Company.Videomatic.Application.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IRepository<Playlist> _repository;

    public PlaylistService(IRepository<Playlist> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<int>> LinkToPlaylists(PlaylistId playlistId, IEnumerable<VideoId> videoIds, CancellationToken cancellationToken = default)
    {
        var pl = await _repository.GetByIdAsync(playlistId, cancellationToken);
        if (pl is null)
        { 
            return Result<int>.NotFound();
        }

        var newLinks = pl.LinkToVideos(videoIds);
        
        await _repository.SaveChangesAsync();

        return newLinks;
    }
}