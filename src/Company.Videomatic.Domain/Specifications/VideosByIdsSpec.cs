using Ardalis.Specification;
using Company.Videomatic.Domain.Video;

namespace Company.Videomatic.Application.Specifications;

// TODO: move somewhere else
public class VideosByIdsSpec : Specification<Video>
{
    public VideosByIdsSpec(params VideoId[] ids)
    {
        Query.Where(v => ids.Contains(v.Id));
    }
}
