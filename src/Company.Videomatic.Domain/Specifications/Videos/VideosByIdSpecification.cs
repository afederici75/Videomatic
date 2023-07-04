using Ardalis.Specification;

namespace Company.Videomatic.Domain.Specifications.Videos;

public class VideosByIdSpecification : Specification<Video>
{
    public VideosByIdSpecification(
        IEnumerable<VideoId> ids)
    {
        Query.Where(v => ids.Contains(v.Id));
    }

    public VideosByIdSpecification(VideoId id)
        : this(new[] { id })
    {
            
    }
}