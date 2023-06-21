using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Query;
using Company.Videomatic.Infrastructure.Data.Handlers;

namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideosQuery(
    Filter Filter = null,
    OrderBy OrderBy = null,
    Paging Paging = null,
    bool IncludeCounts = false,
    ThumbnailResolution? IncludeThumbnail = null) : IRequest<GetVideosResponse>
{ 
    //public GetVideosQuery() : this(new Filter(), new OrderBy(), new Paging(), false, null) { }
}

public record GetVideosResponse(PageResult<VideoDTO> Page);

public class GetVideosQueryValidator : AbstractValidator<GetVideosQuery>
{
    public GetVideosQueryValidator()
    {
        //RuleFor(x => x.PlaylistId).GreaterThan(0);        
    }
}