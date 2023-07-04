namespace Company.Videomatic.Domain.Specifications.Playlists;

public class PlaylistsByIdSpecification : Specification<Playlist>
{
    public PlaylistsByIdSpecification(IEnumerable<PlaylistId> playlistIds)
    {
        Query.Where(p => playlistIds.Contains(p.Id));
    }

    public PlaylistsByIdSpecification(PlaylistId playlistId)
    {
        Query.Where(p => playlistId == p.Id);
    }
}
