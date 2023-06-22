using Company.Videomatic.Application.Features.DataAccess;

namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideosQuery(
    VideosFilter? Filter = null,
    OrderBy? OrderBy = null,
    Paging? Paging = null,
    bool IncludeCounts = false,
    ThumbnailResolution? IncludeThumbnail = null) : IRequest<PageResult<VideoDTO>>;


public class GetVideosQueryValidator : AbstractValidator<GetVideosQuery>
{
    public GetVideosQueryValidator()
    {
        RuleFor(x => x.Filter).SetValidator(new VideosFilterValidator());
        RuleFor(x => x.OrderBy).SetValidator(new OrderByValidator());
        RuleFor(x => x.Paging).SetValidator(new PagingValidator());
    }
}