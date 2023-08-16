namespace Application.Features.Transcripts.Commands;

public class DeleteTranscriptCommand(TranscriptId id) : IRequest<Result>
{ 
    public TranscriptId Id { get; } = id;

    internal class Validator : AbstractValidator<DeleteTranscriptCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
        }
    }
}