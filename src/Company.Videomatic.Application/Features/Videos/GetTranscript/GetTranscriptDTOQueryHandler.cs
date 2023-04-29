using Ardalis.GuardClauses;

namespace Company.Videomatic.Application.Features.Videos.GetTranscript;

public class GetTranscriptDTOQueryHandler : IRequestHandler<GetTranscriptDTOQuery, TranscriptDTO>
{
    readonly IReadRepositoryBase<Transcript> _repository;

    public GetTranscriptDTOQueryHandler(IReadRepositoryBase<Transcript> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<TranscriptDTO> Handle(GetTranscriptDTOQuery request, CancellationToken cancellationToken)
    {
        var query = new GetOneSpecification<Transcript>(request.TranscriptId, new[] { nameof(Transcript.Lines) });
        var transcript = await _repository.FirstOrDefaultAsync(query, cancellationToken);

        Guard.Against.Null(transcript, nameof(transcript));

        var lines = transcript.Lines.Select(l => l.Text)
                                    .ToArray();

        var response = new TranscriptDTO(
                VideoId: transcript.Id,
                TranscriptId: transcript.Id,
                Language: transcript.Language ?? string.Empty,
                Lines: lines,
                LineCount: lines.Length);

        return response;

    }
}