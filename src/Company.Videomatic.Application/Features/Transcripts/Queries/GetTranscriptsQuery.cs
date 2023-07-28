namespace Company.Videomatic.Application.Features.Transcripts.Queries;

public record GetTranscriptsQuery(
    // IBasicQuery
    string? SearchText = null,
    string? OrderBy = null,
    int? Skip = null,
    int? Take = null,
    // Additional
    IEnumerable<long>? VideoIds = null,
    IEnumerable<long>? TranscriptIds = null) : IRequest<Page<TranscriptDTO>>, IBasicQuery;

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

        When(x => x.TranscriptIds is not null, () =>
        {
            RuleFor(x => x.TranscriptIds).NotEmpty();
        });

        When(x => x.OrderBy is not null, () =>
        {
            RuleFor(x => x.OrderBy).NotEmpty();
        });

        RuleFor(x => x.Take).GreaterThan(0);
        RuleFor(x => x.Skip).GreaterThan(0);
    }
}

