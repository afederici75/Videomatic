namespace Company.Videomatic.Application.Features.Transcripts.Queries;

public record GetTranscriptsQuery(
    string? SearchText = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null,
    IEnumerable<long>? VideoIds = null) : IRequest<PageResult<TranscriptDTO>>;

internal class GetTranscriptsQueryValidator : AbstractValidator<GetTranscriptsQuery>
{
    public GetTranscriptsQueryValidator()
    {
        When(x => x.SearchText is not null, () =>
        {
            RuleFor(x => x.SearchText).NotEmpty();
        });

        When(x => x.VideoIds is not null, () =>
        {
            RuleFor(x => x.VideoIds).NotEmpty();
        });

        When(x => x.OrderBy is not null, () =>
        {
            RuleFor(x => x.OrderBy).NotEmpty();
        });

        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}

