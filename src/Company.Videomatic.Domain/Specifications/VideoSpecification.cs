using Ardalis.Specification;
using System.Threading;

namespace Company.Videomatic.Domain.Specifications;

public class VideoSpecification : Specification<Video>, ISingleResultSpecification<Video>
{
    public VideoSpecification(VideoId videoId)
    {
        Query.Where(vid => vid.Id == videoId);
    }
}
