using Ardalis.Specification;
using System.Threading;

namespace Company.Videomatic.Domain.Specifications.Videos;

public class VideoWithPlaylistsSpecification : VideosByIdSpecification, ISingleResultSpecification<Video>
{
    public VideoWithPlaylistsSpecification(params VideoId[] ids)
        : base(ids)
    {
        Query.Include(v => v.Playlists);
    }
}