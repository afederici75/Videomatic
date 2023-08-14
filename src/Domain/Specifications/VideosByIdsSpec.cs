using Ardalis.Specification;
using Domain.Videos;

namespace Application.Specifications;

// TODO: move somewhere else
public class VideosByIdsSpec : Specification<Video>
{
    public VideosByIdsSpec(params VideoId[] ids)
    {
        Query.Where(v => ids.Contains(v.Id));
    }
}
