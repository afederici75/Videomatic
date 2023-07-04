namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideosByIdQuery(
    long[] VideoIds,
    ThumbnailResolutionDTO? Resolution = null) : IRequest<IEnumerable<VideoDTO>>;

internal class GetVideosByIdQueryValidator : AbstractValidator<GetVideosByIdQuery>
{
    public GetVideosByIdQueryValidator()
    {
        RuleFor(x => x.VideoIds).NotEmpty();        
    }
}