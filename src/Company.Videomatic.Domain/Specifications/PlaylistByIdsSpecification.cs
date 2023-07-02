using Ardalis.Specification;
using Company.Videomatic.Domain.Aggregates.Playlist;

namespace Company.Videomatic.Domain.Specifications;

public class PlaylistByIdsSpecification : Specification<Playlist>
{
    public PlaylistByIdsSpecification(params PlaylistId[] playlistIds)
    {
        Query.Where(p => playlistIds.Contains(p.Id));
    }
}