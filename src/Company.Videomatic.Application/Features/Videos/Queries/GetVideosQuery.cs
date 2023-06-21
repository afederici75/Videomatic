using Company.Videomatic.Application.Query;

namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideosQuery(
    GetVideosFilter? Filter = null,
    OrderBy? OrderBy = null,
    Paging? Paging = null,
    bool IncludeCounts = false,
    ThumbnailResolution? IncludeThumbnail = null) : IRequest<PageResult<VideoDTO>>;

public record GetVideosFilter(
    long? PlaylistId  = null,
    string? SearchText = null,
    long[]? Ids = null,
    params FilterItem[] Items) : Filter(SearchText, Ids, Items);

public class GetVideosQueryValidator : AbstractValidator<GetVideosQuery>
{
    public GetVideosQueryValidator()
    {
        //RuleFor(x => x.PlaylistId).GreaterThan(0);        
    }
}