namespace Company.Videomatic.Domain.Services;

public interface IPlaylistService
{
    Task AddVideosToPlaylist(PlaylistId playlistId, params VideoId[] videoIds);
}

public class PlaylistService : IPlaylistService
{
    private readonly IRepository<Playlist> _repository;

    public PlaylistService(IRepository<Playlist> repository)
    {
        _repository = repository;
    }

    public async Task AddVideosToPlaylist(PlaylistId playlistId, params VideoId[] videoIds)
    {
        var playlist = await _repository.GetByIdAsync(playlistId);   

        // get duplicated ids

        foreach (var id in videoIds)
        { 
            playlist.AddVideo(id);
        }
    }
}