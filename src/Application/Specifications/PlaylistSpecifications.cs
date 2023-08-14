namespace Application.Specifications;

public class PlaylistWithVideos : Specification<Playlist>
{
    public PlaylistWithVideos(params PlaylistId[] playlistIds)
    {
        Query.Where(pl => playlistIds.Contains(pl.Id))
             .Include(pl => pl.Videos);
    }
}

public class PlaylistByIds : Specification<Playlist>
{
    public PlaylistByIds(params PlaylistId[] playlistIds)
    {
        Query.Where(v => playlistIds.Contains(v.Id));
    }
}