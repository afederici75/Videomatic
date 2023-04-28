namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;


public partial class GetVideosDTOQuery : QueryBase<GetVideosDTOSpecification, VideoDTO>
{
    public GetVideosDTOQuery(GetVideosDTOSpecification specification) : base(specification)
    {
    }
}
