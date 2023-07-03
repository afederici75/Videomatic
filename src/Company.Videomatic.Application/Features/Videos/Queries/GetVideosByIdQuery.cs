using Company.Videomatic.Domain.Abstractions;

namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideosByIdQuery(
    long[] VideoIds,
    bool IncludeCounts = false,
    ThumbnailResolutionDTO? IncludeThumbnail = null) : IRequest<IEnumerable<VideoDTO>>;

internal class GetVideosByIdQueryValidator : AbstractValidator<GetVideosByIdQuery>
{
    public GetVideosByIdQueryValidator()
    {
        RuleFor(x => x.VideoIds).NotEmpty();        
    }
}