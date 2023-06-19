using Company.Videomatic.Application.Features.Model;

namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideosByIdQuery(long[] Ids) : IRequest<GetVideosByIdResponse>
{
    public GetVideosByIdQuery(long Id) : this(new[] { Id })
    { }
};

public record GetVideosByIdResponse(IEnumerable<VideoDTO> Items);

public class GetVideosByIdQueryValidator : AbstractValidator<GetVideosByIdQuery>
{
    public GetVideosByIdQueryValidator()
    {
        RuleFor(x => x.Ids).NotEmpty();
    }
}