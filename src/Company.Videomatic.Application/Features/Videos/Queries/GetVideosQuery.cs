using Company.Videomatic.Application.Abstractions;

namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideosQuery(
    long? PlaylistId = null,
    string? SearchText = null,
    long[]? Ids = null,
    string? OrderBy = null,
    int? Page = 1,
    int? PageSize = 10,
    bool IncludeCounts = false,
    ThumbnailResolution? IncludeThumbnail = null) : IRequest<GetVideosResponse>, IQueryOptions;

public record GetVideosResponse(IPagedList<VideoDTO> Page);

public class GetVideosQueryValidator : AbstractValidator<GetVideosQuery>
{
    public GetVideosQueryValidator()
    {
        RuleFor(x => x.PlaylistId).GreaterThan(0);        
    }
}