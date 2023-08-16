namespace Application.Features.Transcripts.Queries;

public class GetTranscriptsQuery(
    string? searchText = null,
    string? orderBy = null,
    int? skip = null,
    int? take = null,
    TextSearchType? searchType = null,
    // 
    IEnumerable<int>? videoIds = null,
    IEnumerable<int>? transcriptIds = null,
    string? searchOn = null) : IRequest<Page<TranscriptDTO>>, IBasicQuery
{ 
    public string? SearchText { get; } = searchText;
    public string? OrderBy { get; } = orderBy;
    public int? Skip { get; } = skip;
    public int? Take { get; } = take;
    public TextSearchType? SearchType { get; } = searchType;
    public IEnumerable<int>? VideoIds { get; } = videoIds;
    public IEnumerable<int>? TranscriptIds { get; } = transcriptIds;
    public string? SearchOn { get; } = searchOn;

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
}

