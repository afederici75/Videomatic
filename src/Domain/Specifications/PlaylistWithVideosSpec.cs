namespace Domain.Playlists;

// TODO: move somewhere else
public class PlaylistWithVideosSpec : Specification<Playlist>
{
    public PlaylistWithVideosSpec(params PlaylistId[] playlistIds)
    {
        Query.Where(pl => playlistIds.Contains(pl.Id))
             .Include(pl => pl.Videos);
    }
}