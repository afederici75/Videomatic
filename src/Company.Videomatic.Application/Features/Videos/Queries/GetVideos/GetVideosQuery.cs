using Company.Videomatic.Domain.Specifications;

namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;


public partial class GetVideosQuery : IRequest<IEnumerable<VideoDTO>>
{
    public GetVideosQuery(GetVideosSpec specification)
    {
        Specification = specification;
    }

    public GetVideosSpec Specification { get; }
}
