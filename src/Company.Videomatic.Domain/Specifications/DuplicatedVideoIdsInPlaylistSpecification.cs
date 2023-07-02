using Ardalis.Specification;

namespace Company.Videomatic.Domain.Specifications;

public class DuplicatedVideoIdsInPlaylistSpecification : Specification<Playlist>
{
    public DuplicatedVideoIdsInPlaylistSpecification(PlaylistId playlistId, VideoId[] videoIds)
    {
        //Query.Where(pv => pv.PlaylistId==playlistId && videoIds.Contains(pv.VideoId));
    }
}