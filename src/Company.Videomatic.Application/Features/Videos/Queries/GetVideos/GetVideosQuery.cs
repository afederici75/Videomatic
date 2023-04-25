using System.Diagnostics;

namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;


public partial class GetVideosQuery : IRequest<GetVideosResponse>
{
    public VideosFilter? Filter { get; set; }
    public PagingOptions? Paging { get; set; }
    public SortOptions? Sort { get; set; }  
    

}
