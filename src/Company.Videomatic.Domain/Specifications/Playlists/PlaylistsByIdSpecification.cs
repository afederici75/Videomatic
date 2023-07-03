using Ardalis.Specification;
using Company.Videomatic.Domain.Aggregates.Playlist;

namespace Company.Videomatic.Domain.Specifications.Playlists;

public class PlaylistsByIdSpecification : Specification<Playlist>
{
    public PlaylistsByIdSpecification(IEnumerable<PlaylistId> playlistIds)
    {
        Query.Where(p => playlistIds.Contains(p.Id));
    }
}
