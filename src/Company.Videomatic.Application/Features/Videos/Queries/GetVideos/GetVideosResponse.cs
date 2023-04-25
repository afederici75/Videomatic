using Company.Videomatic.Domain;

namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;

public class GetVideosResponse
{
    public GetVideosResponse(IEnumerable<GetVideosDTO> videos)
    {
        Videos = videos ?? throw new ArgumentNullException(nameof(videos));
    }

    public IEnumerable<GetVideosDTO> Videos { get; }
    public int Count => Videos.Count();
}
