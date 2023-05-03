using Ardalis.GuardClauses;

namespace Company.Videomatic.Application.Features.Videos.GetTranscript;

public class GetTranscriptDTOQueryHandler : IRequestHandler<GetTranscriptDTOQuery, TranscriptDTO>
{
    readonly IReadOnlyRepository<Transcript> _repository;

    public GetTranscriptDTOQueryHandler(IReadOnlyRepository<Transcript> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<TranscriptDTO> Handle(GetTranscriptDTOQuery request, CancellationToken cancellationToken)
    {        
        var transcript = await _repository.GetByIdAsync(
            request.TranscriptId, 
            new[] { nameof(Transcript.Lines) }, 
            cancellationToken);

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