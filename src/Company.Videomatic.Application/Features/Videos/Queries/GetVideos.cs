using Company.Videomatic.Application.Features.DataAccess;

namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideosQuery(
    long[]? PlaylistIds = null,
    string? Filter = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null,
    bool IncludeCounts = false,
    ThumbnailResolutionDTO? IncludeThumbnail = null) : IRequest<PageResult<VideoDTO>>;


internal class GetVideosQueryValidator : AbstractValidator<GetVideosQuery>
{
    public GetVideosQueryValidator()
    {
        When(x => x.PlaylistIds is not null, () =>
        {
            RuleFor(x => x.PlaylistIds).NotEmpty();
        });

        When(x => x.Filter is not null, () =>
        {
            RuleFor(x => x.Filter).NotEmpty();
        });

        When(x => x.OrderBy is not null, () =>
        { 
           RuleFor(x => x.OrderBy).NotEmpty();
        });
        
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}