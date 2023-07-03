using Company.Videomatic.Application.Features.Transcripts;
using Company.Videomatic.Domain.Abstractions;

namespace Company.Videomatic.Application.Features.Artifacts.Queries;

public record GetTranscriptsByIdQuery(
    long[] TranscriptIds,
    string? SearchText = null,
    string? OrderBy = null,
    int? Page = null,
    int? PageSize = null) : IRequest<IEnumerable<TranscriptDTO>>;

internal class GetTranscriptByIdQueryValidator : AbstractValidator<GetTranscriptsByIdQuery>
{
    public GetTranscriptByIdQueryValidator()
    {
        RuleFor(x => x.TranscriptIds).NotEmpty();

        When(x => x.SearchText is not null, () =>
        {
            RuleFor(x => x.SearchText).NotEmpty();
        });

        When(x => x.OrderBy is not null, () =>
        {
            RuleFor(x => x.OrderBy).NotEmpty();
        });

        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}

