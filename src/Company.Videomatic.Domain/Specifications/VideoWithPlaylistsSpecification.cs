using Ardalis.Specification;
using System.Threading;

namespace Company.Videomatic.Domain.Specifications;

public class VideoWithPlaylistsSpecification : VideoByIdsSpecification, ISingleResultSpecification<Video>
{
    public VideoWithPlaylistsSpecification(params VideoId[] ids)
        : base(ids)
    {
        Query.Include(v => v.Playlists);
    }
}