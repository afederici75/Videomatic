using Ardalis.Specification;
using System.Threading;

namespace Company.Videomatic.Domain.Specifications;

public class VideoByIdSpecification : Specification<Video>, ISingleResultSpecification<Video>
{
    public VideoByIdSpecification(VideoId id)
    {
        Query.Where(v => v.Id == id);
    }
}

public class VideoWithPlaylistsSpecification : VideoByIdSpecification, ISingleResultSpecification<Video>
{
    public VideoWithPlaylistsSpecification(VideoId id)
        : base(id)
    {
        Query.Include(v => v.Playlists);
    }
}
