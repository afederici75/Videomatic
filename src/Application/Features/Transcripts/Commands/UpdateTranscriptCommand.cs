namespace Application.Features.Transcripts.Commands;

public class UpdateTranscriptCommand(TranscriptId transcriptId,
                                     string language,
                                     IEnumerable<string> lines) : IRequest<Result<Transcript>>
{ 
    public TranscriptId TranscriptId { get; } = transcriptId;
    public string Language { get; } = language;
    public IEnumerable<string> Lines { get; } = lines;

    internal class Validator : AbstractValidator<UpdateTranscriptCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.TranscriptId).GreaterThan(0);
            RuleFor(x => x.Language).NotEmpty();
            RuleFor(x => x.Lines).NotEmpty();
        }
    }

    internal class Handler(IMyRepository<Transcript> repository, IMapper mapper) : UpdateEntityHandler<UpdateTranscriptCommand, Transcript, TranscriptId>(repository, mapper)
    {
    }
}