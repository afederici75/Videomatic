namespace Company.Videomatic.Application.Features.Videos;

/// <summary>
/// 
/// </summary>
/// <param name="TranscriptId"></param>
public record GetTranscriptQuery(long TranscriptId) : IRequest<GetTranscriptResult>;

/// <summary>
/// 
/// </summary>
/// <param name="VideoId"></param>
/// <param name="TranscriptId"></param>
/// <param name="Language"></param>
/// <param name="Lines"></param>
/// <param name="LineCount"></param>
public record GetTranscriptResult(long VideoId, long TranscriptId, string Language, string[] Lines, int LineCount);

/// <summary>
/// 
/// </summary>
public class GetTranscriptQueryValidator : AbstractValidator<GetTranscriptQuery>
{
    public GetTranscriptQueryValidator()
    {
        RuleFor(x => x.TranscriptId).GreaterThan(0);
    }
}

/// <summary>
/// 
/// </summary>
public class GetTranscriptQueryHandler : IRequestHandler<GetTranscriptQuery, GetTranscriptResult>
{
    //readonly IReadOnlyRepository<Transcript> _repository;

    public GetTranscriptQueryHandler()//(IReadOnlyRepository<Transcript> repository)
    {
        //_repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetTranscriptResult> Handle(GetTranscriptQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var transcript = await _repository.GetByIdAsync(
        //    request.TranscriptId,
        //    new[] { nameof(Transcript.Lines) },
        //    cancellationToken);

        //Guard.Against.Null(transcript, nameof(transcript));

        //var lines = transcript.Lines.Select(l => l.Text)
        //                            .ToArray();

        //var response = new GetTranscriptResult(
        //        VideoId: transcript.Id,
        //        TranscriptId: transcript.Id,
        //        Language: transcript.Language ?? string.Empty,
        //        Lines: lines,
        //        LineCount: lines.Length);

        //return response;

    }
}